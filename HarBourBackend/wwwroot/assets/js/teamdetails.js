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

  $("#team .owl-carousel").owlCarousel({
    loop: true,
    margin: 30,
    dots: true,
    mouseDrag: true,
    autoplay: false,
    nav: false,
    navText: [
      "<span class='lnr ti-angle-left'></span>",
      "<span class='lnr ti-angle-right'></span>",
    ],
    responsiveClass: true,
    responsive: {
      0: {
        items: 1,
        dots: true,
      },
      600: {
        items: 2,
      },
      1000: {
        items: 3,
      },
    },
  });

  $(document).ready(function () {
    let biographybt = $(".biography-bt");
    let educationbt = $(".education-bt");
    let awardsbt = $(".awards-bt");
  
    let tbiography = $(".biography");
    let teducation = $(".education");
    let tawards = $(".awards");
  
    $(biographybt).click(function () {
      $(tbiography).removeClass("d-none");
      $(teducation).addClass("d-none");
      $(tawards).addClass("d-none");
      $(this).css({ color: "#1e90ff" });
      $(educationbt).css({ color: "black" });
      $(awardsbt).css({ color: "black" });
    });
  
    $(educationbt).click(function () {
      $(tbiography).addClass("d-none");
      $(teducation).removeClass("d-none");
      $(tawards).addClass("d-none");
      $(this).css({ color: "#1e90ff" });
      $(biographybt).css({ color: "black" });
      $(awardsbt).css({ color: "black" });
    });
  
    $(awardsbt).click(function () {
      $(tbiography).addClass("d-none");
      $(teducation).addClass("d-none");
      $(tawards).removeClass("d-none");
      $(this).css({ color: "#1e90ff" });
      $(biographybt).css({ color: "black" });
      $(educationbt).css({ color: "black" });
    });

  })

})
