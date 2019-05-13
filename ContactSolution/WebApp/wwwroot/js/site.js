// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    $('[type="datetime-local"]')
        .attr('type', 'text')
        .flatpickr(
            {
                enableTime: true
            }
        );


    $('[type="datetime"]')
        .attr('type', 'text')
        .flatpickr(
            {
                enableTime: true,
            }
        );

    $('[type="date"]')
        .attr('type', 'text')
        .flatpickr(
            {
            }
        );

    $('[type="time"]')
        .attr('type', 'text')
        .flatpickr(
            {
                enableTime: true,
            }
        );

});