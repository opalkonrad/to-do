$(document).ready(function () {
    /* To prevent warning:
     * [Deprecation] Synchronous XMLHttpRequest on the main thread is deprecated because of its detrimental
     * effects to the end user's experience. For more help, check https://xhr.spec.whatwg.org/.
     */
    $.ajaxPrefilter(function (options, original_Options, jqXHR) {
        options.async = true;
    });

    $.ajax({
        url: '/ToDoes/RefreshToDoTable',
        success: function (result) {
            $('#currTable').html(result);
        }
    });

    clearInput = function () {
        $('.form-control').val('');
    }
});
