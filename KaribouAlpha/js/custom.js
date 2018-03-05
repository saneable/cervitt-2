// --------------------------------------------------------------
    // Author       : Crevitt
    // Template Name: Crevitt Template
    // Version      : 1.1 
// --------------------------------------------------------------
// ==============================================================
    // 01. START SWIPER SLIDER JS
// ==============================================================

/*---------Swiper-Slider----------*/

// 01. START SWIPER SLIDER JS
    var swiper = new Swiper('.swiper-crevitt', {
      /*slidesPerView: 3,
      spaceBetween: 15,*/
      loop: true,
      navigation: {
        nextEl: '.swiper-button-next-crevitt',
        prevEl: '.swiper-button-prev-crevitt',
        clickable: true,
      },
       /*breakpoints:{
           1023: {
                slidesPerView: 3,
                spaceBetween: 15
            },
            768: {
                slidesPerView: 2,
                spaceBetween: 15
            },
            640: {
                slidesPerView: 1,
                spaceBetween: 15
            },
            320: {
                slidesPerView: 1,
                spaceBetween: 0
            }
        }*/
    });
    swiper.on('slideChange', function () {
        console.log('slide changed');
    });

     /*var swiper = new Swiper('.swiper-crevitt', {
        nextButton: '.swiper-button-next-crevitt',
        prevButton: '.swiper-button-prev-crevitt',
        parallax: true,
        speed: 600,
    });
*/

  
/*---------Swiper-Slider----------*/



/*---------Login-Animation----------*/

// 02. Login Animation JS

    $(document).ready(function () {
        $('.login-fix').click(function () {

        if ($("#email").val() == "" && $("#password").val() == "") {
            $('#pre-header').toggleClass('login_active');
        } else {

            var toogler = true;
            if ($("#email").val() != "") {
                toogler = false;
            }
            if ($("#password").val() != "") {
                toogler = false;
            }
            if (toogler) {
                $('#pre-header').toggleClass('login_active');
            }
            else
            {
                $('#pre-header').addClass('login_active');
            }
        }
        

        //$('#pre-header').addClass('login_active');
        //if ($("#pre-header").hasClass("login_active") && ( )) {	
        //    $('#pre-header').toggleClass('login_active');
        //}
        
    });

    $('.close_login').click(function(){
        $('#pre-header').toggleClass('login_active');
    });
});





/*---------Login-Animation----------*/