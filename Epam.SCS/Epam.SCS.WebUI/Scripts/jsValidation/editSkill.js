$(document).ready(function () {
    $('#editForm').validate({
        rules: {
            title: {
                required: true,
                minlength: 1
            }           
        },
        messages: {
            title: {
                required: "Please specify skill`s title",
                minlength: "title must have a length greater than 1 character"
            }           
        }
    });
});