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
    let startDate = null;
    let endDate = null;

    Date.prototype.addDays = function (days) {
        var date = new Date(this.valueOf());
        date.setDate(date.getDate() + days);
        return date;
    }

    function getDates(startDate, stopDate) {
        var dateArray = new Array();
        var currentDate = startDate;
        while (currentDate <= stopDate) {
            dateArray.push(moment(new Date(currentDate)).format('YYYY-MM-DD'));
            currentDate = currentDate.addDays(1);
        }
        return dateArray;
    }

    //if yazilmalidiki tesdiqden sonra girsin



    let dateRangeInput = $('input[name="daterange"]');
    dateRangeInput.daterangepicker({
        opens: 'left',
        autoUpdateInput: false,
        minDate: new Date(),
        isInvalidDate: function (ele) {
            var currDate = moment(ele._d).format('YYYY-MM-DD');
            return reservations.reduce((prev, val) => [...prev, ...getDates(val.startDate, val.endDate)], []).indexOf(currDate) != -1;
        }
    });

    dateRangeInput.on('apply.daterangepicker', function (ev, picker) {
        startDate = picker.startDate.format('YYYY-MM-DD');
        endDate = picker.endDate.format('YYYY-MM-DD');
    });

    $(document).on('click', '.rezev-form .rezev-info form button', function (e) {
        e.preventDefault();
        let yachtId = parseInt($(this).attr("id"));
        let username = $(".username").val();
        let userEmail = $(".user-email").val();
        let guest = $(".guest").val();
        let enquiry = $(".enquiry").val();

        console.log(yachtId, startDate, endDate, userEmail, username);
        if (userEmail.trim() == "" || username.trim() == "" || startDate == null || endDate == null || enquiry.trim() == "") {
            toastr["error"]("Plese fill all inputs");
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-center",
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
            };
            return;
        }
        $.ajax({
            url: `../AddReservation`,
            data: { yachtId, username, userEmail, startDate, endDate, guest },
            type: 'POST',
            success: function (response) {
                toastr["success"]("Thanks for your reservation");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-center",
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
                };
            },
            error: function (response) {
                toastr["error"](response.responseJSON.detail);
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-center",
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
                };
            }
        });
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

    if ($(".interior-carousel").length) {
        $(".interior-carousel").owlCarousel({
            loop: true,
            autoplay: true,
            margin: 30,
            autoHeight: false,
            autoplayTimeout: 2000,
            dots: true,
            nav: true,
            navText: [
                '<i class="fa-solid fa-chevron-left" aria-hidden="true" ></i>',
                '<i class="fa-solid fa-chevron-right" aria-hidden="true"></i>',
            ],
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
                },
            },
        });
    }


    $('#luxury-yacht .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        mouseDrag: true,
        autoplay: false,
        dots: true,
        nav: false,
        navText: ["<span class='lnr ti-angle-left'></span>", "<span class='lnr ti-angle-right'></span>"],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1
            },
            1000: {
                items: 2
            }
        }
    });








});
