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
			cur_index = 0;

        // Appending the markup to the page
        overlay.hide().appendTo('body');
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
            cur_index = index;
        }

        // Preload an image by its index in the items array
        function preload(index) {

            setTimeout(function () {
                showImage(index);
                showComments();
            }, 0);
        }

        // Show image in the slider
        function showImage(index) {

            // If the index is outside the bonds of the array
            if (index < 0 || index >= items.length) {
                return false;
            }

            // Call the load function with the href attribute of the item
            loadImage(items.eq(index).attr('href'), function () {
                placeholders.eq(index).html(this);
            });

        }

        // Load the image and execute a callback function.
        // Returns a jQuery object

        function loadImage(src, callback) {

            var img = $('<img>').on('load', function () {
                callback.call(img);
            });

            img.attr('src', src);
            
        }

        function showNext() {

            // If this is not the last image
            if (index + 1 < items.length) {
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

        function showComments()
        {
            var place = placeholders.eq(cur_index);
            if (place.find('div.commentsBlock').length <= 0) {
                var id = items.eq(index).attr('data-id');

                var div = $("<div class='comtrols_div'>");
                var mess_div = $("<div class='mess_div'>");
                var btns_div = $("<div class='btns_div'>");
                var content = $("<div class='commentsBlock'>");
                var textbox = $("<textarea>");
                var delete_btn = $("<a href='/Photo/Delete/"+ id +"' class='btn btn-danger btn-block'>Delete</a>");
                var send_btn = $("<a href='' class='btn btn-success btn-block'>Send</a>");

                textbox.appendTo(mess_div);
                delete_btn.appendTo(btns_div);
                send_btn.appendTo(btns_div);
                mess_div.appendTo(div);
                btns_div.appendTo(div);
                div.appendTo(content);
                place.append(content);
            }
            else {
                return;
            }
        }
    };

})(jQuery);