$(document).ready(function () {
    
    //Basket Products
    
    // var basket = JSON.parse(Cookies.get('basket'))
    // var addMore = document.querySelectorAll("#addMoreProduct");
    // addMore.forEach(element=>{
    //     element.addEventListener('click', function (){
    //         var parent = this.closest("tr")
    //         $(parent).each(function(){
    //             $(this).find('td').each(function(){
    //                 // console.log(this.getAttribute('id').value())
    //                 // console.log($(this).attr('id'))
    //                 if (this.hasAttribute('id')){
    //                     if(this.getAttribute('id') === 'productId'){
    //                         for (let i = 0; i <= this.parentNode.rows; i++){
    //
    //                         }
    //                     }
    //                 }
    //             })
    //         })
    //     })
    // })
    
   


    //Search from Database
    
    $(document).on('keyup', '#input-search', function () {
        var searchedProduct = $(this).val();
        $("#searchedProducts li").slice(1).remove()
        
        $.ajax({
            type: "GET",
            url: "Home/Search?searchedProduct=" + searchedProduct,
            success: function (res){
                $("#searchedProducts").append(res);
            }
        });
    });
    
    //Products
    
    var skip = 4;
    var emptyProduct = $('<div>').css({margin: "0 auto"}).text("No more Product")
    
    // $(document).on('scroll', function(){
    //     $.ajax({
    //         type: "GET",
    //         url: "/Products/Load?skip=" + skip,
    //         success: function (res){
    //             $('#productRow').append(res)
    //             skip += 4
    //            
    //             if (skip >= $('#productsCount').val()){
    //                 $('#loadMoreAnim').remove()
    //                 $('#productRow').append(emptyProduct)
    //             }
    //         }
    //     })
    // })
    
    // HEADER

    $(document).on('click', '#search', function () {
        $(this).next().toggle();
    })

    $(document).on('click', '#mobile-navbar-close', function () {
        $(this).parent().removeClass("active");

    })
    $(document).on('click', '#mobile-navbar-show', function () {
        $('.mobile-navbar').addClass("active");

    })

    $(document).on('click', '.mobile-navbar ul li a', function () {
        if ($(this).children('i').hasClass('fa-caret-right')) {
            $(this).children('i').removeClass('fa-caret-right').addClass('fa-sort-down')
        }
        else {
            $(this).children('i').removeClass('fa-sort-down').addClass('fa-caret-right')
        }
        $(this).parent().next().slideToggle();
    })

    // SLIDER

    $(document).ready(function(){
        $(".slider").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });

    // PRODUCT

    $(document).on('click', '.categories', function(e)
    {
        e.preventDefault();
        $(this).next().next().slideToggle();
    })

    $(document).on('click', '.category li a', function (e) {
        e.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-item');
        
        products.each(function () {
            if(category == $(this).attr('data-id'))
            {
                $(this).parent().fadeIn();
            }
            else
            {
                $(this).parent().hide();
            }
        })
        if(category == 'all')
        {
            products.parent().fadeIn();
        }
    })

    // ACCORDION 

    $(document).on('click', '.question', function()
    {   
       $(this).siblings('.question').children('i').removeClass('fa-minus').addClass('fa-plus');
       $(this).siblings('.answer').not($(this).next()).slideUp();
       $(this).children('i').toggleClass('fa-plus').toggleClass('fa-minus');
       $(this).next().slideToggle();
       $(this).siblings('.active').removeClass('active');
       $(this).toggleClass('active');
    })

    // TAB

    $(document).on('click', 'ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().next().children('p.active').removeClass('active');

        $(this).parent().next().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    $(document).on('click', '.tab4 ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().parent().next().children().children('p.active').removeClass('active');

        $(this).parent().parent().next().children().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    // INSTAGRAM

    $(document).ready(function(){
        $(".instagram").owlCarousel(
            {
                items: 4,
                loop: true,
                autoplay: true,
                responsive:{
                    0:{
                        items:1
                    },
                    576:{
                        items:2
                    },
                    768:{
                        items:3
                    },
                    992:{
                        items:4
                    }
                }
            }
        );
      });

      $(document).ready(function(){
        $(".say").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });
})