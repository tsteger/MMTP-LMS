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

    $('.crtactivity_btn').click(function () {

        if (this.innerText === "Show Activity form") {
            this.innerText = "Hide Activity form";
        }
        else {
            this.innerText = "Show Activity form"
        }
    });

    $('.crtstudent_btn').click(function () {

        if (this.innerText === "Show Student form") {
            this.innerText = "Hide Student form";
        }
        else {
            this.innerText = "Show Student form"
        }
    });


    $('.crtmodule_btn').click(function () {

        if (this.innerText === "Show Module form") {
            this.innerText = "Hide Module form";
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

    $('.courseView_btn').click(function () {

        if (this.innerText === "Courses") {
            this.innerText = "Hide Courses";
        }
        else {
            this.innerText = "Courses"
        }
    });

    $('.moduleView_btn').click(function () {

        if (this.innerText === "Modules") {
            this.innerText = "Hide Modules";
        }
        else {
            this.innerText = "Modules"
        }
    });

    $('.activitiesView_btn').click(function () {

        if (this.innerText === "Activities") {
            this.innerText = "Hide Activities";
        }
        else {
            this.innerText = "Activities"
        }
    });

    $('.documentsView_btn').click(function () {

        if (this.innerText === "Documents") {
            this.innerText = "Hide Documents";
        }
        else {
            this.innerText = "Documents"
        }
    });

    $('.studentsView_btn').click(function () {

        if (this.innerText === "Students") {
            this.innerText = "Hide Students";
        }
        else {
            this.innerText = "Students"
        }
    });



    $('#forgot-password').hide()
   
    $("p").filter(function () {
        return $(this).find("a[href^='/Identity/Account/Register']").length > 0;
    }).hide();

    $('.upload').click(function () {

      //  $('.doc_counter').html.('My content here :-)');

        var el = $('.doc_counter');
        el.addClass('green');
       // el.removeClass('theClassThatsThereNow');
      //  alert("Hi");
    });
});