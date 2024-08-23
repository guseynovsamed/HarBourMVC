(function ($) {

    // Category filter
    $('.category-filter').on('click', function () {
        const categoryId = $(this).attr('category-id');

        $(".paginate").css("display", "none");

        $(".products .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: `Shop/CategoryFilter?id=${categoryId}`,
            success: function (res) {
                $('.products').append(res);
            },
        });
    });


    //Price filter
    $('.form-range').on('change', function () {
        const value = $(this).val().trim();

        $(".paginate").css("display", "none");

        $(".products .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: `Shop/PriceFilter?price=${value}`,
            success: function (res) {
                $('.products').append(res);
            },
        });
    });



    // Search
    $(document).on("keyup", ".search", function () {
        const value = $(this).val().trim();

        if (value !== "") {
            $(".paginate").css("display", "none");
        } else {
            $(".paginate").css("display", "block");
        }

        $(".products .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: `Shop/Search?searchText=${value}`,
            success: function (res) {
                $(".products").append(res);
            }
        });
    });



    //Sorting
    $('#fruits').on('change', function () {
        const value = $(this).val().trim();

        $(".paginate").css("display", "none");

        $(".products .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: "Shop/Sorting",
            data: { sort: value },
            success: function (res) {
                $('.products').append(res);
            },
        });
    });



}
