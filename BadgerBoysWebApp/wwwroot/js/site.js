jQuery(function ($) {

    $(".badger-boy").hover(function () {
            $(this).animate({
                opacity: 0.8
            });
        },
        function () {
            $(this).stop();
            $(this).css({
                width: '70%',
                opacity: 1
            });
        }
    );

    $("#matt-badger").click(function () {
        showBio("#matt-bio");
    });

    $("#alex-badger").click(function () {
        showBio("#alex-bio");
    });

    $("#tommie-badger").click(function () {
        showBio("#tommie-bio");
    });

    function showBio(selector) {
        $("#matt-bio").hide();
        $("#alex-bio").hide();
        $("#tommie-bio").hide();
        $(selector).slideDown();
        $('html, body').animate({
            scrollTop: ($("#bio-holder").offset().top)
        }, 500);
    }
});
