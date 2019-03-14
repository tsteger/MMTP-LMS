$(document).ready(function () {
    $('.activity_btn').click(function () {

        if (this.innerText === "Show Activities") {
            this.innerText = "Hide Activities";
        }
        else {
            this.innerText = "Show Activities"
        }
    });

    $('.crtcourse_btn').click(function () {

        if (this.innerText === "Show Course form") {
            this.innerText = "Hide Course form";
        }
        else {
            this.innerText = "Show Course form"
        }
    });

    $('.crtstudent_btn').click(function () {

        if (this.innerText === "Show Create Student form") {
            this.innerText = "Hide Create Student form";
        }
        else {
            this.innerText = "Show Create Student form"
        }
    });


    $('.crtmodule_btn').click(function () {

        if (this.innerText === "Show Create Module form") {
            this.innerText = "Hide Create Module form";
        }
        else {
            this.innerText = "Show Module form"
        }
    });

    $('.gotouser_btn').click(function () {

        if (this.innerText === "User") {
            this.innerText = "User";
        }
        else {
            this.innerText = "Show User form"
        }
    });

    $('#forgot-password').hide()
   
    $("p").filter(function () {
        return $(this).find("a[href^='/Identity/Account/Register']").length > 0;
    }).hide();
});