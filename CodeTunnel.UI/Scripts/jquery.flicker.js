(function ($)
{
    $.fn.flicker = function (options)
    {
        var settings = $.extend({
            delay: 1000,
            class: 'flicker'
        }, options);

        return this.each(function ()
        {
            $this = $(this);
            ChangeLetter($this);
        });

        function ChangeLetter(element)
        {
            var originalText = element.html();
            var index = Math.round(Math.random() * (originalText.length-1));
            var insert = '<span class="' + settings.class + '">' + originalText[index] + '</span>';
            var newText = originalText.substr(0, index) + insert + originalText.substr(index + 1);
            element.html(newText);
            setTimeout(function ()
            {
                ChangeLetterBack(element, originalText);
            }, settings.delay);
        }

        function ChangeLetterBack(element, originalText)
        {
            element.html(originalText);
            ChangeLetter(element);
        }
    };
})(jQuery);