/**
 * @name		jQuery touchTouch plugin
 * @author		Martin Angelov
 * @version 	1.0
 * @url			http://tutorialzine.com/2012/04/mobile-touch-gallery/
 * @license		MIT License
 */


(function () {

    /* Private variables */

    var overlay = $('<div id="galleryOverlay">'),
		slider = $('<div id="gallerySlider">'),
		prevArrow = $('<a id="prevArrow"></a>'),
		nextArrow = $('<a id="nextArrow"></a>'),
		overlayVisible = false;

    /* Creating the plugin */

    $.fn.touchTouch = function () {

        var placeholders = $([]),
			index = 0,
			allitems = this,
			items = allitems,
            current_pos = 0;

        // Appending the markup to the page
        overlay.hide().appendTo('body').css('z-index', '50');
        slider.appendTo(overlay);

        // Creating a placeholder for each image
        items.each(function () {

            placeholders = placeholders.add($('<div class="placeholder">'));

        });

        // Hide the gallery if the background is touched / clicked
        slider.append(placeholders).on('click', function (e) {

            var coutn = 0;
            if ($(e.target).is('.placeholder')) {
                hideOverlay();
            }
        });

        // Listen for touch events on the body and check if they
        // originated in #gallerySlider img - the images in the slider.
        $('body').on('touchstart', '#gallerySlider img', function (e) {

            var touch = e.originalEvent,
				startX = touch.changedTouches[0].pageX;

            slider.on('touchmove', function (e) {

                e.preventDefault();

                touch = e.originalEvent.touches[0] ||
						e.originalEvent.changedTouches[0];

                if (touch.pageX - startX > 10) {

                    slider.off('touchmove');
                    showPrevious();
                }

                else if (touch.pageX - startX < -10) {

                    slider.off('touchmove');
                    showNext();
                }
            });

            // Return false to prevent image 
            // highlighting on Android
            return false;

        }).on('touchend', function () {

            slider.off('touchmove');

        });

        // Listening for clicks on the thumbnails
        items.on('click', function (e) {

            e.preventDefault();

            var $this = $(this),
				galleryName,
				selectorType,
				$closestGallery = $this.parent().closest('[data-gallery]');

            // Find gallery name and change items object to only have 
            // that gallery

            //If gallery name given to each item
            if ($this.attr('data-gallery')) {

                galleryName = $this.attr('data-gallery');
                selectorType = 'item';

                //If gallery name given to some ancestor
            } else if ($closestGallery.length) {

                galleryName = $closestGallery.attr('data-gallery');
                selectorType = 'ancestor';

            }

            //These statements kept seperate in case elements have data-gallery on both
            //items and ancestor. Ancestor will always win because of above statments.
            if (galleryName && selectorType == 'item') {

                items = $('[data-gallery=' + galleryName + ']');

            } else if (galleryName && selectorType == 'ancestor') {

                //Filter to check if item has an ancestory with data-gallery attribute
                items = items.filter(function () {

                    return $(this).parent().closest('[data-gallery]').length;

                });

            }

            // Find the position of this image
            // in the collection
            index = items.index(this);
            showOverlay(index);
            showImage(index);

            // Preload the next image
            preload(index + 1);

            // Preload the previous
            preload(index - 1);

        });

        // If the browser does not have support 
        // for touch, display the arrows
        if (!("ontouchstart" in window)) {
            overlay.append(prevArrow).append(nextArrow);

            prevArrow.click(function (e) {
                e.preventDefault();
                showPrevious();
            });

            nextArrow.click(function (e) {
                e.preventDefault();
                showNext();
            });
        }

        // Listen for arrow keys
        $(window).bind('keydown', function (e) {

            if (e.keyCode == 37) {
                showPrevious();
            }

            else if (e.keyCode == 39) {
                showNext();
            }

        });


        /* Private functions */


        function showOverlay(index) {
            // If the overlay is already shown, exit
            if (overlayVisible) {
                return false;
            }
            current_pos = index;
            // Show the overlay
            overlay.show();

            setTimeout(function () {
                // Trigger the opacity CSS transition
                overlay.addClass('visible');
            }, 100);

            // Move the slider to the correct image
            offsetSlider(index);

            // Raise the visible flag
            overlayVisible = true;
            
        }

        function hideOverlay() {

            // If the overlay is not shown, exit
            if (!overlayVisible) {
                return false;
            }

            // Hide the overlay
            overlay.hide().removeClass('visible');
            overlayVisible = false;

            //Clear preloaded items
            $('.placeholder').empty();

            //Reset possibly filtered items
            items = allitems;
        }

        function offsetSlider(index) {

            // This will trigger a smooth css transition
            slider.css('left', (-index * 100) + '%');
            current_pos = index;
            showComments(index);
        }

        // Preload an image by its index in the items array
        function preload(index) {

            setTimeout(function () {
                showImage(index);
                
            }, 0);
        }

        // Show image in the slider
        function showImage(index) {
            // If the index is outside the bonds of the array
            if (index < 0 || index >= items.length) {
                return false;
            }

            // Call the load function with the href attribute of the item
            loadImage(items.eq(index).attr('href'), index, function () {
                placeholders.eq(index).html(this);

                showComments(current_pos);
            });
        }

        // Load the image and execute a callback function.
        // Returns a jQuery object

        function loadImage(src, index, callback) {
            var presenter = $("<div class='presenter'>");
            var img = $("<img>");
            img.attr('src', src);
            presenter.append(img);
            callback.call(presenter);
        }

        function showNext() {

            // If this is not the last image
            if (index + 1 < items.length) {
                var coms = $(".commentsBlock").remove();
                index++;
                offsetSlider(index);
                preload(index + 1);
            }

            else {
                // Trigger the spring animation
                slider.addClass('rightSpring');
                setTimeout(function () {
                    slider.removeClass('rightSpring');
                }, 500);
            }
        }

        function showPrevious() {

            // If this is not the first image
            if (index > 0) {
                var coms = $(".commentsBlock").remove();
                index--;
                offsetSlider(index);
                preload(index - 1);
            }

            else {
                // Trigger the spring animation
                slider.addClass('leftSpring');
                setTimeout(function () {
                    slider.removeClass('leftSpring');
                }, 500);
            }
        }

        function showComments(index)
        {
            var place = placeholders.eq(index).find("div.presenter");
           
            if (place.find('div.commentsBlock').length <= 0)
            {
                var id = items.eq(index).attr('data-id');
                getCommentsControls(id, place);    
            }
            else {
                return;
            }
        }

        function getCommentsControls(id, place)
        {
            $.ajax({
                async: false,
                type: "GET",
                url: "/Photo/CommentsPartial/" + id,
                success: function (viewHTML) {
                    place.append(viewHTML);
                    var _index = placeholders.eq(index).find(".index").val(index);
                    var all_comments = place.find(".all_comments");
                    getCommentsList(id, all_comments);
                }
            });
        }

        function getCommentsList(id, all_comments) {
            $.ajax({
                async: false,
                type: "GET",
                url: "/Comment/CommentList/" + id,
                success: function (viewHTML) {
                    all_comments.append(viewHTML);
                },
                error: function (mess) { }
            });
        }
    };

})(jQuery);

function sendCom() {
    var place = $(".placeholder");
    var mes_div = $(".mess_div");
    var mess_text = mes_div.find("textarea").val();
    var id_mess = mes_div.find("#id_mes").val();
    $.ajax({
        async: false,
        type: "POST",
        url: "/Comment/Create/",
        data: { text: mess_text, id: id_mess },
        success: function (viewHTML) {
            var coms = mes_div.find(".all_comments");
            coms.append(viewHTML);
            mes_div.find("textarea").val("");
        }
    });
}

function del_comment(id, id_com)
{
    //alert(id_com);
    $.ajax({
        async: false,
        type: "POST",
        url: "/Comment/DeleteConfirmed/",
        data: { id: id },
        success: function (viewHTML) {
            id_com.remove();
        }
    });
}
