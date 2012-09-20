var monitorHash = false;
//DOM Ready
$(function ()
{
    $('#fileBrowser').colorbox({ width: "50%", height: "50%" });

    if (monitorHash)
    {
        $.history.init(function (hash)
        {
            if (hash == "")
            {
                // initialize your app
            }
            else
            {
                // restore the state from hash
                $('#ajax-loader').slideDown('fast');
                $('#mainContent').slideUp('normal', function ()
                {
                    $.ajax({
                        url: hash,
                        type: "GET",
                        success: function (response)
                        {
                            $('#ajax-loader').slideUp('fast');
                            $('#mainContent').html(response).slideDown();
                            $.validator.unobtrusive.parse('form');
                            Prettify();
                        },
                        error: function (jqxhr)
                        {
                            $('#mainConent').html($('body', jqxhr.responseText).html());
                        }
                    });
                });
            }
        },
        { unescape: ",/" });

        $('.pageLinks a').live('click', function (e)
        {
            e.preventDefault();
            var url = $(this).attr('href');
            $.history.load(url);
        });
    }

    $('.wmd').typeWatch({
        callback: function ()
        {
            Prettify();
        },
        wait: 750,
        highlight: false,
        captureLength: 1
    });

    ActivateWmd();
    CreateUploader();

    //$('div.summaryText').flicker();
});

// Events

$('.deleteLink').live('click', function (e)
{
    return confirm('Are you sure?');
});


// Functions

function Prettify()
{
    $('code').addClass('prettyprint');
    prettyPrint();
}

function ActivateWmd()
{
    $('.wmd').wmd();
    Prettify();
}

function CreateUploader()
{
    $('.file_uploader').each(function ()
    {
        var uploader = new qq.FileUploader(
        {
            element: this,
            action: $(this).attr('action'),
            showMessage: function (message) { alert(message); }
        });
    });
}