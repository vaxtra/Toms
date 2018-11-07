(function ($) {
 "use strict";

/*===================================
Wow Js 
=====================================*/
     new WOW().init();
/*===================================
 jQuery MeanMenu
=====================================*/
    $('#dropdown').meanmenu();
/*===================================
 Owl Carousel Featured Items
=====================================*/	
  $(".feature_item_lists").owlCarousel({
      autoPlay: false, //Set AutoPlay to 3 seconds
      items : 4,
      itemsDesktop : [1199,3],
      itemsDesktopSmall : [979,3],
	  navigation:true,
	  navigationText: ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
	  stopOnHover: true 
  });	 
/*===================================
 Owl Carousel Company Lists
=====================================*/
    $(".company_lists").owlCarousel({
         autoPlay: false, 
         items : 6,
         itemsDesktop:[1199,4],
         stopOnHover : true,
  		 navigation:true,
  		 navigationText: ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"]
    });  
/*===========================
Sticky Menu 
=============================*/
     $(".home-one .header_area").sticky({topSpacing: 0});
     $(".home-two .header_area").sticky({topSpacing: 0});
     $(".home-three .mainmenu").sticky({topSpacing: 0});
     $(".home-four .mainmenu").sticky({topSpacing: 0});
     $(".home-five .mainmenu").sticky({topSpacing: 0});
     $(".blog .header_area").sticky({topSpacing: 0});
     $(".list .header_area").sticky({topSpacing: 0});
     $(".grid .header_area").sticky({topSpacing: 0});
/*===========================
Scroll to top
=============================*/
    $('body').materialScrollTop();
/*=====================================
Price Filter 
=======================================*/
    $( "#slider-range" ).slider({
        range: true,
        min: 0,
        max: 1500,
        values: [ 450, 969 ],
        slide: function( event, ui ) {
            $( "#amount" ).val( "" + ui.values[ 0 ] + " - " + ui.values[ 1 ] );
        }
    });
    $( "#amount" ).val( "" + $( "#slider-range" ).slider( "values", 0 ) +
        " - " + $( "#slider-range" ).slider( "values", 1 ) );	
/*=======================
  Category menu
 ========================*/
     $('#cate-toggle li.has-sub>a').on('click', function(){
      $(this).removeAttr('href');
      var element = $(this).parent('li');
      if (element.hasClass('open')) {
       element.removeClass('open');
       element.find('li').removeClass('open');
       element.find('ul').slideUp();
      }
      else {
       element.addClass('open');
       element.children('ul').slideDown();
       element.siblings('li').children('ul').slideUp();
       element.siblings('li').removeClass('open');
       element.siblings('li').find('li').removeClass('open');
       element.siblings('li').find('ul').slideUp();
      }
     });
     $('#cate-toggle>ul>li.has-sub>a').append('<span class="holder"></span>');
     $('#cate-toggle ul li.has-sub ul.category-sub > li.has-sub > a').append('<span class="holder"></span>');
/*=================================
cart-plus-minus-button 
===================================*/
    $(".cart-plus-minus").append('<div class="dec qtybutton">-</i></div><div class="inc qtybutton">+</div>');
    $(".qtybutton").on("click", function () {
        var $button = $(this);
        var oldValue = $button.parent().find("input").val();
        if ($button.text() == "+") {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        $button.parent().find("input").val(newVal);
    });
/*-===============================
 countdown
==================================*/
	$('[data-countdown]').each(function() {
	  var $this = $(this), finalDate = $(this).data('countdown');
	  $this.countdown(finalDate, function(event) {
		$this.html(event.strftime('<span class="cdown days"><span class="time-count">%-D</span> <p>Days</p></span> <span class="cdown hour"><span class="time-count">%-H</span> <p>Hour</p></span> <span class="cdown minutes"><span class="time-count">%M</span> <p>Min</p></span> <span class="cdown second"> <span><span class="time-count">%S</span> <p>Sec</p></span>'));
	  });
	});	

})(jQuery);    