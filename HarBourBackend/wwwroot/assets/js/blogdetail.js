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

    //$(document).on('click', '.add-comment', function (e) {
    //    e.preventDefault();
    //    let blogId = parseInt($(this).attr("data-blogId"));
    //    let userId = $(this).attr("data-userId");
    //    let comment = $(".comment-text").val();
    //    if (comment.trim() == "") {
    //        toastr["error"]("Comment can`t leave empty")
    //        toastr.options = {
    //            "closeButton": false,
    //            "debug": false,
    //            "newestOnTop": false,
    //            "progressBar": false,
    //            "positionClass": "toast-top-center",
    //            "preventDuplicates": false,
    //            "onclick": null,
    //            "showDuration": "300",
    //            "hideDuration": "1000",
    //            "timeOut": "5000",
    //            "extendedTimeOut": "1000",
    //            "showEasing": "swing",
    //            "hideEasing": "linear",
    //            "showMethod": "fadeIn",
    //            "hideMethod": "fadeOut"
    //        }
    //        return;
    //    }

    //    $.ajax({
    //        url: `../AddComment`,
    //        data: { userId, blogId, comment },
    //        type: 'POST',
    //        success: function (response) {
    //            toastr["success"]("Comment posted")
    //            toastr.options = {
    //                "closeButton": false,
    //                "debug": false,
    //                "newestOnTop": false,
    //                "progressBar": false,
    //                "positionClass": "toast-top-right",
    //                "preventDuplicates": false,
    //                "onclick": null,
    //                "showDuration": "300",
    //                "hideDuration": "1000",
    //                "timeOut": "5000",
    //                "extendedTimeOut": "1000",
    //                "showEasing": "swing",
    //                "hideEasing": "linear",
    //                "showMethod": "fadeIn",
    //                "hideMethod": "fadeOut"
    //            }
    //            $(".comment-text").val("");
    //        },
    //        error: function (response) {
    //            toastr["error"]("Login to add comment")
    //            toastr.options = {
    //                "closeButton": false,
    //                "debug": false,
    //                "newestOnTop": false,
    //                "progressBar": false,
    //                "positionClass": "toast-top-right",
    //                "preventDuplicates": false,
    //                "onclick": null,
    //                "showDuration": "300",
    //                "hideDuration": "1000",
    //                "timeOut": "5000",
    //                "extendedTimeOut": "1000",
    //                "showEasing": "swing",
    //                "hideEasing": "linear",
    //                "showMethod": "fadeIn",
    //                "hideMethod": "fadeOut"
    //            }
    //        }

    //    });

    //})


    $(document).on('click', '.add-comment', function (e) {
        e.preventDefault();
        let blogId = parseInt($(this).attr("data-blogId"));
        let userId = $(this).attr("data-userId");
        let comment = $(".comment-text").val();
        if (comment.trim() === "") {
            toastr["error"]("Comment can't be empty");
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
            url: `../AddComment`,
            data: { userId, blogId, comment },
            type: 'POST',
            success: function (response) {
                toastr["success"]("Comment posted");
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
                };

                console.log(response)
                $(".comment-text").val("");


                let newCommentHtml = `
                    <ol>
                        <li>
                                            <div class="comment-body">
                                                <div class="author">
                                                    <img src="/assets/img/team/3.png" alt="" />
                                                    <h3>${response.userFullName}</h3>
                                                </div>
                                                <div class="date">
                                                    <p>${response.createDate}</p>
                                                </div>
                                                <p>
                                                    ${response.textComment}
                                                </p>
                                            </div>
                                        </li>
                                    </ol>`;

                $('.comments-area').append(newCommentHtml);
            },
            error: function (response) {
                toastr["error"]("Login to add comment");
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
                };
            }
        });
    });






})