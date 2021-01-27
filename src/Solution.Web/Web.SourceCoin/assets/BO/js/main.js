jQuery(document).ready(function($) {
    new WOW().init();

    function stickySide(idString, closest, offset) {
        if (!$(idString).length) return;
        if (!$(closest).length) return;
        if (!$(offset)) offset = 0;
        let winTop = $(window).scrollTop();
        let mainHeight = $(closest).height();
        let mainHeightOff = $(closest).offset().top;
        if (
            winTop + offset >= mainHeightOff &&
            winTop + offset + $(idString).height() <= mainHeightOff + mainHeight
        ) {
            $(idString).css({
                position: 'relative',
                top: offset + winTop - mainHeightOff + 'px',
            });
        } else {
            if (winTop + offset < mainHeightOff) {
                $(idString).attr('style', '');
            }
            if (winTop + offset + $(idString).height() > mainHeightOff + mainHeight) {
                $(idString).css({
                    top: mainHeight - $(idString).height() + 'px',
                });
            }
        }
    }

    $('.tab-wrapper').each(function() {
        let $tabWrapper, $tabID;
        $tabWrapper = $(this);
        $tabID = $tabWrapper.find('.tab-link.current').attr('data-tab');
        $tabWrapper.find($tabID).fadeIn().siblings().hide();
        $($tabWrapper).on('click', '.tab-link', function(e) {
            e.preventDefault();
            $tabID = $(this).attr('data-tab');
            $(this).addClass('current').siblings().removeClass('current');
            $tabWrapper.find($tabID).fadeIn().siblings().hide();
        });
    });

    $('.main-menu-btn').on('click', function() {
        $(this).addClass('active');
        $('.main-menu').addClass('active');
    });

    $('.main-menu-overlay').on('click', function() {
        $('.main-menu-btn').removeClass('active');
        $('.main-menu').removeClass('active');
    });

    $('.acc-info-btn').on('click', function(e) {
        e.preventDefault();
        $('.status-mobile').addClass('open');
        $('.overlay-status-mobile').show();
    });

    $('.overlay-status-mobile').on('click', function() {
        $('.status-mobile').removeClass('open');
        $(this).hide();
    });

    if ($('.scroll-top').length) {
        $(window).scroll(function() {
            $(this).scrollTop() > 100 ?
                $('.scroll-top').addClass('show') :
                $('.scroll-top').removeClass('show');
        });
        $('.scroll-top').on('click', function() {
            $('html, body').animate({ scrollTop: 0 }, 'slow');
        });
    }

    /*$('.open-popup-btn').magnificPopup({
          removalDelay: 500,
          callbacks: {
              beforeOpen: function() {
                  this.st.mainClass = "mfp-zoom-in";
              },
          },
          midClick: true
      });*/

    $('.open-video-popup-btn').magnificPopup({
        disableOn: 700,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false,
    });

    $('.main-menu-nav .dropdown > a').append(
        '<i class="fa fa-angle-down" aria-hidden="true"></i>'
    );
    $(window).on('load resize', function() {
        if (window.matchMedia('(min-width: 992px)').matches) {
            $('.main-menu-nav .dropdown').hover(
                function() {
                    $(this).find('> .sub-menu-wrap').stop().slideDown('fast');
                },
                function() {
                    $(this).find('> .sub-menu-wrap').stop().slideUp('fast');
                }
            );
        } else {
            $('.main-menu-nav .dropdown > a > .fa').on('click', function(e) {
                e.preventDefault();
                $(this)
                    .closest('.dropdown')
                    .find('> .sub-menu-wrap')
                    .stop()
                    .slideToggle();
                $(this).hasClass('fa-angle-down') ?
                    $(this).removeClass('fa-angle-down').addClass('fa-angle-up') :
                    $(this).removeClass('fa-angle-up').addClass('fa-angle-down');
            });
        }
    });

    if ($('.header').length && $('.main').length) {
        let $header = $('.header'),
            $main = $('.main');
        $main.css('margin-top', $header.outerHeight());
        $(window).scrollTop() > 50 ?
            $header.addClass('fixed') :
            $header.removeClass('fixed');
        $(window).on('scroll', function() {
            $(window).scrollTop() > 50 ?
                $header.addClass('fixed') :
                $header.removeClass('fixed');
        });
    }
});
var toggleHeight = $(window).outerHeight();
$(window).scroll(function() {
    if ($(window).scrollTop() > toggleHeight) {
        //Adds active class to make button visible
        $('.m-backtotop').addClass('active');

        //Just some cool text changes
        $('h1 span').text('TA-DA! Now hover it and hit dat!');
    } else {
        //Removes active class to make button visible
        $('.m-backtotop').removeClass('active');

        //Just some cool text changes
        $('h1 span').text('(start scrolling)');
    }
});

//Scrolls the user to the top of the page again
$('.m-backtotop').click(function() {
    $('html, body').animate({ scrollTop: 0 }, 'slow');
    return false;
});

$('#brand-source').msDropdown();

// slide

$('.slide-vertical-banner').slick({
    dots: false,
    autoplay: true,
    infinite: true,
    speed: 300,
    arrows: false,
    autoplaySpeed: 2000,
    slidesToShow: 1,
    slidesToScroll: 1,
    vertical: true,
    cssEase: 'ease-in-out',
    verticalSwiping: true,
});

var swiperas = new Swiper('.info', {
    slidesPerView: 'auto',

    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
});
$('.slide-available').slick({
    dots: false,
    autoplay: false,
    infinite: true,
    arrows: true,
    speed: 1000,
    autoplaySpeed: 6000,
    slidesToShow: 2,
    slidesToScroll: 1,
        responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 4,
        slidesToScroll: 3,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow:2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 500,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
});
$('.slide-function').slick({
    dots: false,
    autoplay: false,
    infinite: true,
    arrows: false,
    speed: 1000,
    autoplaySpeed: 6000,
    slidesToShow: 3,
    slidesToScroll: 1,
        responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow:2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 500,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
  ]
});
$(document).ready(function() {
    $('.btn-drodown').click(function() {
        $('.box-opacity').toggleClass('visible');
    });
});
