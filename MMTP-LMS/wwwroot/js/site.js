$(document).ready(function () {
    $('.activity_btn').click(function () {

        if (this.innerText === "Show Activities") {
            this.innerText = "Hide Activities";
        }
        else {
            this.innerText = "Show Activities"
        }
    });
    $('#forgot-password').hide()
   
    $("p").filter(function () {
        return $(this).find("a[href^='/Identity/Account/Register']").length > 0;
    }).hide();
});