$(document).ready(function () {



	$(window).on('scroll', function() {
		if($(window).scrollTop() > 90){
        $('.header').addClass('fix-header');
		} else{
			$('.header').removeClass('fix-header');
		}
	});



    $(window).on('scroll', function() {
		if($(window).scrollTop() >= $('#main').offset().top){
			$('a.fix-active').removeClass('fix-active');
			$('.main-nav').addClass('fix-active');
		}
		if ($(window).scrollTop() >= $('#teacher').offset().top) {
			$('a.fix-active').removeClass('fix-active');
			$('.teacher-nav').addClass('fix-active');
		}
		if($(window).scrollTop() >= $('#about').offset().top){
			$('a.fix-active').removeClass('fix-active');
			$('.about-nav').addClass('fix-active');
		}
		if($(window).scrollTop() >= $('#courses').offset().top){
			$('a.fix-active').removeClass('fix-active');
			$('.course-nav').addClass('fix-active');
		}
		if ($(window).scrollTop() >= $('.packages').offset().top - 100) {
			$('a.fix-active').removeClass('fix-active');
			$('.packages-nav').addClass('fix-active');
		}
	});




	$('.main-nav').click(function() {
		var offset = 0;
		$('a.fix-active').removeClass('fix-active');
		$('.main-nav').addClass('fix-active');
		$('html, body').animate({
			scrollTop: $('#main').offset().top
		}, 1000);
		return false;
	});
	$('.about-nav').click(function() {
		var offset = 0;
		$('a.fix-active').removeClass('fix-active');
		$('.about-nav').addClass('fix-active');
		$('html, body').animate({
			scrollTop: $('#about').offset().top
		}, 1000);
		return false;
	});
	$('.course-nav, .select-btn').click(function() {
		var offset = 0;
		$('a.fix-active').removeClass('fix-active');
		$('.course-nav').addClass('fix-active');
		$('html, body').animate({
			scrollTop: $('#courses').offset().top
		}, 1000);
		return false;
	});
	$('.teacher-nav').click(function () {
		var offset = 0;
		$('a.fix-active').removeClass('fix-active');
		$('.teacher-nav').addClass('fix-active');
		$('html, body').animate({
			scrollTop: $('#teacher').offset().top
		}, 1000);
		return false;
	});
	$('.packages-nav').click(function () {
		var offset = 0;
		$('a.fix-active').removeClass('fix-active');
		$('.packages-nav').addClass('fix-active');
		$('html, body').animate({
			scrollTop: $('.packages').offset().top - 100
		}, 1000);
		return false;
	});
         
         
});