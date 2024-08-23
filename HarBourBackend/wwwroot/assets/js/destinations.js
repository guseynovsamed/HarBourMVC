$(function () {
  $(window).on("scroll", function () {
    var bodyScroll = $(this).scrollTop(),
      navbar = $(".navbar");
    if (bodyScroll > 100) {
      navbar.addClass("nav-scroll");
    } else {
      navbar.removeClass("nav-scroll");
    }
  });



    $("#preloader").fadeOut(700);
    $(".preloader-bg").delay(700).fadeOut(700);
    var wind = $(window);




  var progressPath = document.querySelector(".progress-wrap path");
  var pathLength = progressPath.getTotalLength();
  progressPath.style.transition = progressPath.style.WebkitTransition = "none";
  progressPath.style.strokeDasharray = pathLength + " " + pathLength;
  progressPath.style.strokeDashoffset = pathLength;
  progressPath.getBoundingClientRect();
  progressPath.style.transition = progressPath.style.WebkitTransition =
    "stroke-dashoffset 10ms linear";
  var updateProgress = function () {
    var scroll = $(window).scrollTop();
    var height = $(document).height() - $(window).height();
    var progress = pathLength - (scroll * pathLength) / height;
    progressPath.style.strokeDashoffset = progress;
  };
  updateProgress();
  $(window).scroll(updateProgress);
  var offset = 150;
  var duration = 550;
  $(window).on("scroll", function () {
    if ($(this).scrollTop() > offset) {
      $(".progress-wrap").addClass("active-progress");
    } else {
      $(".progress-wrap").removeClass("active-progress");
    }
  });
  $(".progress-wrap").on("click", function (event) {
    event.preventDefault();
    $("html, body").animate(
      {
        scrollTop: 0,
      },
      duration
    );
    return false;
  });
});
