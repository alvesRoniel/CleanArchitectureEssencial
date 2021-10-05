// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.

$(function () {
    $('#upload').change(function () {
        const file = $(this)[0].files[0]
        const fileReader = new FileReader()
        fileReader.onloadend = function () {
            $('#imgUpload').attr('src', fileReader.result)
        }
        fileReader.readAsDataURL(file)
    })
})



