$(document).ready(function () {
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
