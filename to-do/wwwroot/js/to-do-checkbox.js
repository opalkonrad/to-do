﻿$(document).ready(function () {
    $('.check').change(function () {
        var self = $(this);
        var id = self.attr('id');
        var val = self.prop('checked');

        $.ajax({
            url: 'ToDoes/AjaxEdit',
            data: {
                id: id,
                val: val
            },
            type: 'POST',
            success: function (result) {
                $('#currTable').html(result);
            }
        });
    });
});
