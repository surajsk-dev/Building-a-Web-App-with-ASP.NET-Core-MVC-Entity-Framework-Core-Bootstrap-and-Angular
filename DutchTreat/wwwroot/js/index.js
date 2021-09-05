//jquery - alias is - $ symbol

console.log("hello");

//javascript way
/* var theForm = document.getElementById("theForm");
theForm.hidden = true;

var Button = document.getElementById("buyButton");
Button.addEventListener("click", () => {
    console.log("buying item");
}); */


//jquery way

var theForm = $("#theForm");
theForm.hide();

var button = $("#buyButton");
button.on("click", function () {
    console.log("buying item");
});

//getting list of items
var productList = $(".product-props li");
productList.on("click", function () {
    console.log("you clicked on" + $(this).text());
});

//toggel form show-hide
var $loginToggle = $("#loginToggle");
var $popupForm = $(".popup-form");

$loginToggle.on("click", function () {
    //adding time in ()
    //$popupForm.toggle(1000);

    //adding animation
    //$popupForm.fadeToggle(1000);
    $popupForm.slideToggle(1000);
})
