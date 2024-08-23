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


    $('.about-slider .owl-carousel').owlCarousel({
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        autoplayTimeout: 4000,
        items: 1,
        loop: true,
        mouseDrag: true,
        margin: 0,
        autoplay: true,
        dots: false,
        smartSpeed: 450
    });



    $("#preloader").fadeOut(700);
    $(".preloader-bg").delay(700).fadeOut(700);
    var wind = $(window);



    $(document).on("keyup", ".search", function () {
        const value = $(this).val().trim();


        $(".products .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: `Home/Search?searchText=${value}`,
            success: function (res) {
                $(".products").append(res);
            }
        });
    });






    if ($(".yachts-carousel").length) {
        $(".yachts-carousel").owlCarousel({
            loop: true,
            margin: 30,
            autoHeight: false,
            autoplayTimeout: 5000,
            dots: true,
            nav: true,
            navText: ['<i class="fa-solid fa-chevron-left" aria-hidden="true" ></i>', '<i class="fa-solid fa-chevron-right" aria-hidden="true"></i>'],
            responsiveClass: true,
            responsive: {
                0: {
                    dots: false,
                    nav: false,
                    items: 1,
                },
                600: {
                    dots: false,
                    nav: false,
                    items: 1,
                },
                1000: {
                    dots: false,
                    items: 1,
                }
            }
        });
    }

    var progressPath = document.querySelector('.progress-wrap path');
    var pathLength = progressPath.getTotalLength();
    progressPath.style.transition = progressPath.style.WebkitTransition = 'none';
    progressPath.style.strokeDasharray = pathLength + ' ' + pathLength;
    progressPath.style.strokeDashoffset = pathLength;
    progressPath.getBoundingClientRect();
    progressPath.style.transition = progressPath.style.WebkitTransition = 'stroke-dashoffset 10ms linear';
    var updateProgress = function () {
        var scroll = $(window).scrollTop();
        var height = $(document).height() - $(window).height();
        var progress = pathLength - (scroll * pathLength / height);
        progressPath.style.strokeDashoffset = progress;
    }
    updateProgress();
    $(window).scroll(updateProgress);
    var offset = 150;
    var duration = 550;
    $(window).on('scroll', function () {
        if ($(this).scrollTop() > offset) {
            $('.progress-wrap').addClass('active-progress');
        } else {
            $('.progress-wrap').removeClass('active-progress');
        }
    });
    $('.progress-wrap').on('click', function (event) {
        event.preventDefault();
        $('html, body').animate({
            scrollTop: 0
        }, duration);
        return false;
    })


    $('#watersports .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: false,
        dots: true,
        nav: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });



    $('#destinations .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: false,
        dots: true,
        autoplayHoverPause: true,
        nav: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2
            },
            1000: {
                items: 4
            }
        }
    });

    $('.testimonials .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: false,
        autoplayTimeout: 5000,
        dots: false,
        nav: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });




    $('#articles .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: false,
        dots: true,
        autoplayHoverPause: true,
        nav: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2
            },
            1000: {
                items: 3
            }
        }
    });


    $(document).on('click', '.subscriber', function (e) {
        e.preventDefault();
        let subscriberEmail = $(".subscribe-input").val();
        if (subscriberEmail.trim() == "") {
            toastr["error"]("You can`t subscribe without email")
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            return;
        }
        $.ajax({
            url: `Home/Subscribe`,
            type: 'POST',
            data: { subscriberEmail },
            success: function (response) {
                toastr["success"]("Thanks for your subscription")
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                $(".subscribe-input").val("");
            }
        });
    })













})



