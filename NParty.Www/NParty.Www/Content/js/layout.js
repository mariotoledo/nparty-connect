(function ($) {
    "use strict";

    $(function () {

        // ie9 placeholder

        if ($('html').hasClass('ie9')) {
            $('input[placeholder]').each(function () {
                $(this).val($(this).attr('placeholder'));
                var v = $(this).val();
                $(this).on('focus', function () {
                    if ($(this).val() === v) {
                        $(this).val("");
                    }
                }).on("blur", function () {
                    if ($(this).val() == "") {
                        $(this).val(v);
                    }
                });
            });

        }

        // remove products from shopping cart

        $('[role="banner"]').on('click', '.close_product', function () {
            $(this).closest('li').animate({ 'opacity': '0' }, function () {
                $(this).slideUp(500);
            });
        });


        // responsive menu

        window.rmenu = function () {

            var nav = $('nav[role="navigation"]'),
				header = $('[role="banner"]');

            var rMenu = new Object();

            rMenu.init = function () {
                rMenu.checkWindowSize();
                $(window).on('resize', rMenu.checkWindowSize);
            }

            rMenu.checkWindowSize = function () {

                if ($(window).width() < 992) {
                    rMenu.Activate();
                }
                else {
                    rMenu.Deactivate();
                }

            }
            // add click events
            rMenu.Activate = function () {
                if ($('html').hasClass('md_touch')) header.off('.touch');
                header.off('click').on('click.responsivemenu', '#menu_button', rMenu.openClose);
                header.on('click.responsivemenu', '.main_menu li a', rMenu.openCloseSubMenu);
                nav.find('.touch_open_sub').removeClass('touch_open_sub').children('a').removeClass('prevented');
            }
            // remove click events
            rMenu.Deactivate = function () {
                header.off('.responsivemenu');
                nav.removeAttr('style').find('li').removeClass('current_click')
				.end().find('.sub_menu_wrap').removeAttr('style').end().find('.prevented').removeClass('prevented').end()
                if ($('html').hasClass('md_touch')) header.off('click').on('click.touch', '.main_menu li a', rMenu.touchOpenSubMenu);
            }

            rMenu.openClose = function () {

                $(this).toggleClass('active');
                nav.stop().slideToggle();

            }

            rMenu.openCloseSubMenu = function (e) {

                var self = $(this);

                if (self.next('.sub_menu_wrap').length) {
                    self.parent()
						.addClass('current_click')
						.siblings()
						.removeClass('current_click')
						.children(':not(span,a)')
						.slideUp();
                    self.next().stop().slideToggle();
                    self.parent().siblings().children('a').removeClass('prevented');

                    if (!(self.hasClass('prevented'))) {
                        e.preventDefault();
                        self.addClass('prevented');
                    } else {
                        self.removeClass('prevented');
                    }
                }

            }

            rMenu.touchOpenSubMenu = function (event) {
                var self = $(this);

                if (self.next('.sub_menu_wrap').length) {

                    if (!(self.hasClass('prevented'))) {
                        event.preventDefault();
                        self.addClass('prevented');
                    } else {
                        self.removeClass('prevented');
                    }

                }

            }

            rMenu.init();
        }

        rmenu();

        /*Search_holder*/

        window.search_holder = function () {

            var searchHolder = $('.search-holder');

            if (searchHolder.length) {
                searchHolder.searchClick();
            }

        }

        search_holder();

        // twitter feed

        window.tweet_slider = function () {

            if ($(".tweets").length) {

                var follow = $(".tweets").next('.tw_follow'),
					config = {
					    username: 'fanfbmltemplate',
					    modpath: 'plugins/twitter/',
					    loading_text: '<img src="images/loader2.gif" alt="">',
					    template: '<p>{text}</p><p>{time}</p>'
					}

                $(".tweets").each(function () {

                    var self = $(this);

                    if (self.hasClass('single')) {
                        config.template = '<p>{text}</p><p>{time}</p>';
                    }
                    self.tweet(config);
                    if (self.hasClass('single')) {
                        var owl = self.data('owlCarousel');

                        $('.twc_prev').on('click', function () { owl.trigger('owl.prev'); });
                        $('.twc_next').on('click', function () { owl.trigger('owl.next'); });
                    }

                    var owlConfig = {
                        navSpeed: 800,
                        nav: true,
                        loop: true,
                        autoplay: true,
                        autoplaySpeed: 800,
                        navText: false,
                        items: 1,
                        onInitialized: function () {
                            self.find('.tweet_odd').parent().remove();
                        }
                    }

                    self.children('.tweet_list').owlCarousel(owlConfig);

                });
                follow.attr('href', 'https://twitter.com/' + config.username);
            }

            // twitter  

            jQuery('#twitter').tweet({
                username: 'fanfbmltemplate',
                modpath: 'plugins/twitter/',
                loading_text: '<img src="images/loader2.gif" alt="">',
                template: '<li><p>{text}</p><p>{time}</p></li>'
            })

        }

        tweet_slider();

        // News carousel

        $("#owl-demo").owlCarousel({
            nav: true,
            navText: false,
            navSpeed: 800,
            loop: true,
            autoplay: true,
            autoplaySpeed: 800,
            items: 1
        });

        // Gallery carousel

        window.footer_slider = function () {

            $(".owl-demo-2").owlCarousel({
                items: 1,
                navigation: true, // Show next and prev buttons
                nav: true,
                loop: true,
                navSpeed: 800,
                navText: false
            });

        }

        footer_slider();

        // Gallery carousel-2

        $("#owl-demo-3").owlCarousel({
            nav: true,
            navText: false,
            navSpeed: 800,
            loop: true,
            items: 1
        });

        // Gallery carousel-3

        $("#owl-demo-4").owlCarousel({
            items: 3,
            navSpeed: 800,
            nav: true,
            loop: true,
            navText: false,
            responsive: {
                0: {
                    items: 1
                },
                481: {
                    items: 2
                },
                980: {
                    items: 3
                }
            }
        });

        // Gallery carousel-4

        $("#owl-demo-5").owlCarousel({
            items: 5,
            navSpeed: 800,
            nav: true,
            navText: false,
            loop: true,
            autoplay: true,
            autoplaySpeed: 800,
            responsive: {
                0: {
                    items: 1
                },
                481: {
                    items: 3
                },
                992: {
                    items: 5
                }
            }
        });

        // Gallery carousel-5

        $("#owl-demo-6").owlCarousel({
            items: 3,
            navSpeed: 800,
            nav: true,
            loop: true,
            navText: false,
            responsive: {
                0: {
                    items: 2
                },
                481: {
                    items: 3
                },
                980: {
                    items: 3
                }
            }
        });

        // Gallery carousel-6

        $("#owl-demo-7").owlCarousel({
            items: 3,
            navSpeed: 800,
            nav: true,
            loop: true,
            navText: false,
            responsive: {
                0: {
                    items: 2
                },
                481: {
                    items: 3
                },
                980: {
                    items: 3
                }
            }
        });

        // Gallery carousel-7

        $("#owl-demo-9").owlCarousel({
            items: 1,
            navSpeed: 800,
            nav: true,
            navText: false,
            loop: true
        });

        // Gallery carousel-8

        $("#owl-demo-10").owlCarousel({
            items: 4,
            navSpeed: 800,
            nav: true,
            loop: true,
            navText: false,
            responsive: {
                0: {
                    items: 2
                },
                481: {
                    items: 3
                },
                980: {
                    items: 4
                }
            }
        });

        //===================fixed header================

        // ie9 placeholder

        if ($('html').hasClass('ie9')) {
            $('input[placeholder]').each(function () {
                $(this).val($(this).attr('placeholder'));
                var v = $(this).val();
                $(this).on('focus', function () {
                    if ($(this).val() === v) {
                        $(this).val("");
                    }
                }).on("blur", function () {
                    if ($(this).val() == "") {
                        $(this).val(v);
                    }
                });
            });

        }

        // tabs

        var tabs = $('.tabs');
        if (tabs.length) {
            tabs.tabs({
                beforeActivate: function (event, ui) {
                    var hash = ui.newTab.children("li a").attr("href");
                },
                hide: {
                    effect: "fadeOut",
                    duration: 450
                },
                show: {
                    effect: "fadeIn",
                    duration: 450
                },
                updateHash: false
            });
        }

        // newsletter

        var subscribe = $('[id^="newsletter"]');
        subscribe.append('<div class="message_container_subscribe"></div>');
        var message = $('.message_container_subscribe'), text;

        subscribe.on('submit', function (e) {
            var self = $(this);

            if (self.find('input[type="email"]').val() == '') {
                text = "Please enter your e-mail!";
                message.html('<div class="alert_box warning"><p>' + text + '</p></div>')
                  .slideDown()
                  .delay(4000)
                  .slideUp(function () {
                      $(this).html("");
                  });

            } else {
                self.find('span.error').hide();
                $.ajax({
                    type: "POST",
                    url: "bat/newsletter.php",
                    data: self.serialize(),
                    success: function (data) {
                        if (data == '1') {
                            text = "Your email has been sent successfully!";
                            message.html('<div class="alert_box success"><p>' + text + '</p></div>')
                              .slideDown()
                              .delay(4000)
                              .slideUp(function () {
                                  $(this).html("");
                              })
                              .prevAll('input[type="email"]').val("");
                        } else {
                            text = "Invalid email address!";
                            message.html('<div class="alert_box error"></i><p>' + text + '</p></div>')
                              .slideDown()
                              .delay(4000)
                              .slideUp(function () {
                                  $(this).html("");
                              });
                        }
                    }
                });
            }
            e.preventDefault();
        });

        // open dropdown

        $('#sort_button').css3Animate($('#sort_button').next('.sort_list'));


        // load more

        $("#load_more").click(function () {
            $(".load_more_block").slideToggle("slow");
            $(this).toggleClass("active");
            return false;
        });
        $(".more_news_button").click(function () {
            $(".more_news").slideToggle("slow");
            $(this).toggleClass("active");
            return false;
        });

        // flexslider

        if ($("#flexslider").length) {
            $("#flexslider").flexslider({
                controlNav: false,
                smoothHeight: true,
                animationSpeed: 1000,
                slideshow: false,
                prevText: '',
                keyboard: false,
                nextText: '',
                start: function (el) {
                    var slshow = el,
						thumbnails = $('.thumbnails_container').children('ul');
                    slshow.find('.flex-direction-nav a').addClass('flex_nav_buttons');
                    var currIndex = slshow.data('flexslider').currentSlide;
                    thumbnails.children('li').eq(currIndex).addClass('active');
                    thumbnails.children('li').on('click', function () {
                        var self = $(this),
							index = self.index();
                        self.addClass('active').siblings().removeClass('active');
                        slshow.data('flexslider').flexAnimate(index);
                    });
                    slshow.find('.flex-prev,.flex-next').on('click', function () {
                        var ci = slshow.children('.slides').children('.flex-active-slide').index();
                        thumbnails.children('li').eq(ci).addClass('active').siblings().removeClass('active');
                    });
                }
            });
        }
        
        $(window).load(function () {
            if ($('.flexslider').length) {
                $(window).trigger('resize');
            }

        });

        // Sticky and Go-top

        (function ($, window) {

            function Temp(el, options) {
                this.el = $(el);
                this.init(options);
            }

            Temp.DEFAULTS = {
                sticky: true
            }

            Temp.prototype = {
                init: function (options) {
                    var base = this;
                    base.window = $(window);
                    base.options = $.extend({}, Temp.DEFAULTS, options);
                    base.menuWrap = $('.menu_wrap');
                    base.goTop = $('<button class="go-to-top" id="go-to-top"></button>').appendTo(base.el);

                    // Sticky
                    if (base.options.sticky) {
                        base.sticky.stickySet.call(base, base.window);
                    }

                    // Scroll Event
                    base.window.on('scroll', function (e) {
                        if (base.options.sticky) {
                            base.sticky.stickyInit.call(base, e.currentTarget);
                        }
                        base.gotoTop.scrollHandler.call(base, e.currentTarget);
                    });

                    // Click Handler Button GotoTop
                    base.gotoTop.clickHandler(base);
                },
                sticky: {
                    stickySet: function () {
                        var menuWrap = this.menuWrap, offset;
                        if (menuWrap.length) {
                            offset = menuWrap.offset().top;
                            $.data(menuWrap, 'data', {
                                offset: offset,
                                height: menuWrap.outerHeight(true)
                            });
                            this.spacer = $('<div/>', { 'class': 'spacer' }).insertBefore(menuWrap);
                        }
                    },
                    stickyInit: function (win) {
                        var base = this, data;
                        if (base.menuWrap.length) {
                            data = $.data(base.menuWrap, 'data');
                            base.sticky.stickyAction(data, win, base);
                        }
                    },
                    stickyAction: function (data, win, base) {
                        var scrollTop = $(win).scrollTop();
                        if (scrollTop > data.offset) {
                            base.spacer.css({ height: data.height });
                            if (!base.menuWrap.hasClass('sticky')) {
                                base.menuWrap.addClass('sticky');
                            }
                        } else {
                            base.spacer.css({ height: 'auto' });
                            if (base.menuWrap.hasClass('sticky')) {
                                base.menuWrap.removeClass('sticky');
                            }
                        }
                    }
                },
                gotoTop: {
                    scrollHandler: function (win) {
                        $(win).scrollTop() > 200 ?
							this.goTop.addClass('go-top-visible') :
							this.goTop.removeClass('go-top-visible');
                    },
                    clickHandler: function (self) {
                        self.goTop.on('click', function (e) {
                            e.preventDefault();
                            $('html, body').animate({ scrollTop: 0 }, 800);
                        });
                    }
                }
            }

            /* Temp Plugin
			 * ================================== */

            $.fn.Temp = function (option) {
                return this.each(function () {
                    var $this = $(this), data = $this.data('Temp'),
						options = typeof option == 'object' && option;
                    if (!data) {
                        $this.data('Temp', new Temp(this, options));
                    }
                });
            }

            $('body').Temp({
                sticky: true
            });

        })(jQuery, window);

        /* ---------------------------------------------------- */
        /*	SmoothScroll										*/
        /* ---------------------------------------------------- */

        // custom select

        $('.custom_select').each(function () {
            var list = $(this).children('ul'),
				select = $(this).find('select'),
				title = $(this).find('.select_title');


            // select items to list items

            if ($(this).find('[data-filter]').length) {
                for (var i = 0, len = select.children('option').length; i < len; i++) {
                    list.append('<li data-filter="' + select.children('option').eq(i).data('filter') + '">' + select.children('option').eq(i).text() + '</li>')
                }
            }
            else {
                for (var i = 0, len = select.children('option').length; i < len; i++) {
                    list.append('<li>' + select.children('option').eq(i).text() + '</li>')
                }
            }
            select.hide();

            // open list

            title.on('click', function () {
                list.slideToggle(400);
                $(this).toggleClass('active');
            });

            // selected option

            list.on('click', 'li', function () {
                var val = $(this).text();
                title.text(val);
                list.slideUp(400);
                select.val(val);
                title.toggleClass('active');
            });

        });


        // accordion & toggle

        var aItem = $('.accordion:not(.toggle) .accordion_item'),
			link = aItem.find('.a_title'),
			$label = aItem.find('label'),
			aToggleItem = $('.accordion.toggle .accordion_item'),
			tLink = aToggleItem.find('.a_title');

        aItem.add(aToggleItem).children('.a_title').not('.active').next().hide();

        function triggerAccordeon($item) {
            $item
				.addClass('active')
				.next().stop().slideDown()
				.parent().siblings()
				.children('.a_title')
				.removeClass('active')
				.next().stop().slideUp();
        }


        if ($label.length) {
            $label.on('click', function () {
                triggerAccordeon($(this).closest('.a_title'))
            });
        } else {
            link.on('click', function () {
                triggerAccordeon($(this))
            });
        }


        tLink.on('click', function () {
            $(this).toggleClass('active')
					.next().stop().slideToggle();

        })

        // jackbox

        if ($(".jackbox[data-group]").length) {
            jQuery(".jackbox[data-group]").jackBox("init", {
                showInfoByDefault: false,
                preloadGraphics: false,
                fullscreenScalesContent: true,
                autoPlayVideo: false,
                flashVideoFirst: false,
                defaultVideoWidth: 960,
                defaultVideoHeight: 540,
                baseName: ".jackbox",
                className: ".jackbox",
                useThumbs: true,
                thumbsStartHidden: false,
                thumbnailWidth: 75,
                thumbnailHeight: 50,
                useThumbTooltips: false,
                showPageScrollbar: false,
                useKeyboardControls: true
            });
        }

        // appear animation

        function animate() {

            $("[data-appear-animation]").each(function () {

                var self = $(this);

                self.addClass("appear-animation");

                if ($(window).width() > 800) {
                    self.appear(function () {

                        var delay = (self.attr("data-appear-animation-delay") ? self.attr("data-appear-animation-delay") : 1);

                        if (delay > 1) self.css("animation-delay", delay + "ms");
                        self.addClass(self.attr("data-appear-animation"));

                        setTimeout(function () {
                            self.addClass("appear-animation-visible");
                        }, delay);

                    }, { accX: 0, accY: -150 });
                } else {
                    self.addClass("appear-animation-visible");
                }
            });
        }

        $(window).on('resize', animate);

        //elevate zoom

        (function () {

            if ($('#thumbnails').length) {
                $('#thumbnails').owlCarousel({
                    items: 3,
                    stagePadding: 40,
                    margin: 10,
                    URLhashListener: false,
                    navSpeed: 800,
                    nav: true,
                    navText: false,
                    responsive: {
                        "0": {
                            "items": 4
                        },
                        "481": {
                            "items": 3
                        },
                        "769": {
                            "items": 3
                        },
                        "992": {
                            "items": 3
                        }
                    }
                });
            }

            if ($('[data-zoom-image]').length) {

                var button = $('.qv_preview [class*="button"]');

                $("#zoom_image").elevateZoom({
                    gallery: 'thumbnails',
                    galleryActiveClass: 'active',
                    zoomType: "inner",
                    cursor: "crosshair",
                    responsive: true,
                    zoomWindowFadeIn: 500,
                    zoomWindowFadeOut: 500,
                    easing: true,
                    lensFadeIn: 500,
                    lensFadeOut: 500
                });

                button.on("click", function (e) {
                    var ez = $('#zoom_image').data('elevateZoom');
                    $.fancybox(ez.getGalleryList());
                    e.preventDefault();
                });

            }

        })();

    });

})(jQuery);