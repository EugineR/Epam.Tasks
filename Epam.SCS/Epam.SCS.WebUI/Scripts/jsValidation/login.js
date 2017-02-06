$(document).ready(function () {
    $('#loginForm').validate({
        rules: {
            login: {
                required: true,
                minlength: 1
            },
            password: {
                required: true,
                minlength: 1
            }
        },
        messages: {
            login: {
                required: "Please specify your login",
                minlength: "Login must have a length greater than 1 character"
            },
            password: {
                required: "Please enter you password",
                minlength: "Password must have a length greater than 1 character"
            }
        }
    });
});