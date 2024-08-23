$(function () {
    $("#eye").click(function (e) {
        e.preventDefault();

        if ($(this).hasClass("fa-regular fa-eye")) {
            $(this).removeClass("fa-regular fa-eye");

            $(this).addClass("fa-regular fa-eye-slash");

            $("#password").attr("type", "text");
        } else {
            $(this).removeClass("fa-regular fa-eye-slash");

            $(this).addClass("fa-regular fa-eye");

            $("#password").attr("type", "password");
        }
    });






    $("#preloader").fadeOut(700);
    $(".preloader-bg").delay(700).fadeOut(700);
    var wind = $(window);


    $("#eyes").click(function (e) {
        e.preventDefault();

        if ($(this).hasClass("fa-regular fa-eye")) {
            $(this).removeClass("fa-regular fa-eye");

            $(this).addClass("fa-regular fa-eye-slash");

            $("#passwords").attr("type", "text");
        } else {
            $(this).removeClass("fa-regular fa-eye-slash");

            $(this).addClass("fa-regular fa-eye");

            $("#passwords").attr("type", "password");
        }
    });
});
