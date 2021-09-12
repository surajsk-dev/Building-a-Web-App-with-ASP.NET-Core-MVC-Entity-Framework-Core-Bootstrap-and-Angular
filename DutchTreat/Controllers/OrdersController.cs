using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok( _repository.GetAllOrders());

            }
            catch(Exception ex)
            {
                _logger.LogError($"failed to get orders: {ex}");
                return BadRequest("failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if(order != null)
                {
                    return Ok(order);

                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"failed to get orders: {ex}");
                return BadRequest("failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            //add to db
            try
            {
                if(ModelState.IsValid)
                {
                    //converting model to order
                    var newOrder = new Order()
                    {
                        OrderDate = model.OrderDate,
                        OrderNumber = model.OrderNumber,
                        Id= model.OrderId

                    };

                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);

                    if (_repository.SaveAll())
                    {
                        //converting back to viewmodel
                        var vm = new OrderViewModel()
                        {
                            OrderId = newOrder.Id,
                            OrderDate = newOrder.OrderDate,
                            OrderNumber = newOrder.OrderNumber
                        };
                        return Created($"/api/orders/{vm.OrderId}", vm);

                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }

            catch(Exception ex)
            {
                _logger.LogError($"failed to save new order : {ex}");
            }
            return BadRequest("failed to save new order");
        }

    }
}
