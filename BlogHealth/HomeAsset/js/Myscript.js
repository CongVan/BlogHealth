var lastScrollTop = 0;
$(window).scroll(function (event) {
    var st = $(this).scrollTop();
    if (st > lastScrollTop) {
        // downscroll code
       
        $('#nav-fix-top').removeClass('fixed-top');
        $('#nav-fix-top').removeClass(' animated fadeInDown');
    } else {
     
        $('#nav-fix-top').addClass('fixed-top');
        $('#nav-fix-top').addClass('animated fadeInDown');
        if (st < 100) {
            $('#nav-fix-top').removeClass('fixed-top');
            $('#nav-fix-top').removeClass('animated fadeInDown');
           
        }
        // upscroll code
    }
    lastScrollTop = st;
});

//$(window).scroll(function () {
//    if ($(this).scrollTop() > 400) {
//        $('#nav-fix-top').addClass('fixed-top');
//        //$('#UtiPost').show();

//        //$('.fx').fadeIn();
//        //$('.aria-contact').fadeIn();
//    } else {
//        $('#nav-fix-top').removeClass('fixed-top');
//       // $('#UtiPost').hide();
//    }
//    //if ($("#fotterStop").isVisible()) {
//    //    $("#UtiPost").hide();
//    //} else {
//    //    $("#UtiPost").show();
//    //}
//    //if ($('body').height() <= ($(window).height() + $(window).scrollTop()+200)) {
//    //    $('#UtiPost').hide();
//    //}

//});

$('.navbar .dropdown').hover(function () {
    $(this).find('.dropdown-menu').first().stop(true, true).slideDown(100);
}, function () {
    $(this).find('.dropdown-menu').first().stop(true, true).slideUp(95)
});