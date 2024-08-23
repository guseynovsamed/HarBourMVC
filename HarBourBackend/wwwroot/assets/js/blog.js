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



    $(document).on("keyup", ".search", function () {
        const value = $(this).val().trim();
        const showMoreButton = $("#blog .show-more");

        // Clear current results
        $(".blog-item .item").slice(0).remove();
        if (value != "") {
            $.ajax({
                type: "Get",
                url: `Blog/Search?searchText=${value}`,
                success: function (res) {
                    // Populate the results
                    $(".blog-item").html(res);

                    // Check if there are more results available
                    if (res.hasMoreResults) {
                        showMoreButton.removeClass("d-none");
                    } else {
                        showMoreButton.addClass("d-none");
                    }
                },
            });
        } else {
            $.ajax({
                type: "Get",
                url: `Blog/Search?searchText=${value}`,
                success: function (res) {
                    // Populate the results
                    $(".blog-item").html(res);

                    // Check if there are more results available
                    showMoreButton.removeClass("d-none")
                },
            });
        }
        // Make the AJAX call
       
    });





    $(document).on("click", ".show-more a", function () {
        let skip = parseInt($(".blog-item").children().length);
        let blogsCount = parseInt($(".blog-item").attr("data-count"))
        let parentElem = $(".blog-item");
        let parentElemContent = $(".blog-item").html();
        $.ajax({
            url: `Blog/ShowMore`,
            data: {skip},
            type: "GET",
            success: function (response) {

                parentElemContent += response;
                $(parentElem).html(parentElemContent)

                let skip = parseInt($(".blog-item").children().length);

                if (skip >= blogsCount) {
                    $("#blog .show-more").addClass("d-none")
                }

            }
        })

    })






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
