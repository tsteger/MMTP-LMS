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
        el.addClass('glow').delay(500).fadeOut(500, function () {
            el.removeClass('glow').delay(500).fadeIn(500);
        });
       
   
    });
   // $("main").addClass("login_main");
    if (window.location.href.endsWith('/Identity/Account/Login')) {
        $('h4').hide();
        $('p').hide();
        $('hr').hide();
        $("main").addClass("login_main");
        $("main").removeClass("pb-3");
        $('h1').addClass("login_h1");
        $("div").removeClass("col-md-4");
        $("div").addClass("col-md-12");
        $("div").removeClass("col-md-6 col-md-offset-2");
    }
});
