$(document).ready(function () {
    $('.activity_btn').click(function () {

        if (this.innerText === "Show Activities") {
            this.innerText = "Hide Activities";
        }
        else {
            this.innerText = "Show Activities"
        }
    });
});