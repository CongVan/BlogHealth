var lastScrollTop = 0;
$(window).scroll(function (event) {
    var st = $(this).scrollTop();
    if (st > lastScrollTop ) {
        // downscroll code
        var t = $('#SideBar').innerHeight();
       // $("#SideBar").css('height',)
        $('#nav-fix-top').removeClass('fixed-top');
        $('#nav-fix-top').removeClass(' animated fadeInDown');
    } else if (lastScrollTop!=0) {
     
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
$('.reviewRate').click(function () {
    $('.reviewRate').addClass('fa-star-o');
    $('.reviewRate').removeClass('yellow-text fa-star');

    var pos = parseInt($(this).data("pos"));
    if (pos < 3) {
        if (pos == 1) {
            $('#titleRate').text("Dở tệ");
        }
        if (pos == 2) {
            $('#titleRate').text("Tạm được");
        }
        $('#titleRate').removeClass('text-success');
        $('#titleRate').addClass('text-danger');
        //$('#iconRate').removeClass('fa-smile-o text-success');
        //$('#iconRate').addClass('fa-frown-o text-danger');
    } else {
        if (pos == 3) {
            $('#titleRate').text("Hay");
        }
        if (pos == 4) {
            $('#titleRate').text("Rất hay");
        }
        if (pos == 5) {
            $('#titleRate').text("Tuyệt vời");
        }
        $('#titleRate').removeClass('text-danger');
        $('#titleRate').addClass('text-success');
        //$('#iconRate').removeClass('fa-frown-o text-danger');
        //$('#iconRate').addClass('fa-smile-o text-success');
    }
    for (var i = 1; i <= pos; i++) {
        $('#star' + i).removeClass('fa-star-o');
        $('#star' + i).addClass("yellow-text fa-star")
    }
    //if ($(this).hasClass("yellow-text fa-star")) {
        
    //    $(this).addClass('fa-star-o');
    //    $(this).removeClass('yellow-text fa-star');
    //} else {
    //    $(this).removeClass('fa-star-o');
    //    $(this).addClass('yellow-text fa-star');
    //}
    
});

//function moveScroller() {
//    var $anchor = $("#scroller-anchor");
//    var $scroller = $('#scroller');

//    var move = function () {
//        var st = $(window).scrollTop();
//        var ot = $anchor.offset().top;
//        if (st > ot) {
//            $scroller.css({
//                position: "fixed",
//                top: "0px"
//            });
//        } else {
//            $scroller.css({
//                position: "relative",
//                top: ""
//            });
//        }
//    };
//    $(window).scroll(move);
//    move();
//}