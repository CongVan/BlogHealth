$(window).scroll(function () {
    if ($(this).scrollTop() > 400) {
        $('#nav-fix-top').addClass('fixed-top');
        //$('#UtiPost').show();

        //$('.fx').fadeIn();
        //$('.aria-contact').fadeIn();
    } else {
        $('#nav-fix-top').removeClass('fixed-top');
       // $('#UtiPost').hide();
    }
    //if ($("#fotterStop").isVisible()) {
    //    $("#UtiPost").hide();
    //} else {
    //    $("#UtiPost").show();
    //}
    //if ($('body').height() <= ($(window).height() + $(window).scrollTop()+200)) {
    //    $('#UtiPost').hide();
    //}

});