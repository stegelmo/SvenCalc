// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {

    // Sätt validering av tal så det kan hantera svenska decimaler"," för jquery.validate.unobtrusive
    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^(?:-?\d+|-?\d{1,3}(?:[\s\.,]\d{3})+)?(?:[\.,]\d+)?(?:[Ee][\+-]\d+)?$/.test(value);
    };
});