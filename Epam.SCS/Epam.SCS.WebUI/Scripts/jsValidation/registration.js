$(document).ready(function () {
    $.validator.addMethod(
    "myDate",
    function (value, element) {        
        return value.match(/^\d\d?\/\d\d?\/\d\d\d\d$/);
    },
    "Please enter a date in the format dd/mm/yyyy."
);

    $('#registerForm').validate({
        rules: {
            login: {
                required: true,
                minlength: 1
            },
            password: {
                required: true,
                minlength: 1
            },
            confirmPassword: {
                required: true,
                equalTo: "#password"
            },
            name: {
                required: true,
                minlength: 1
            },
            surname: {
                required: true,
                minlength: 1
            },
            patronymic: {
                required: false
            },
            date: {
                required: true,
                myDate: true
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
            },
            confirmPassword: {
                required: "Please enter you password again",
                equalTo: "Passwords not match"
            },
            name: {
                required: "Please specify your name",
                minlength: "Name must have a length greater than 1 character"
            },
            surname: {
                required: "Please specify your surname",
                minlength: "Surname must have a length greater than 1 character"
            },
            date: {
                required: "Please specify your date of birth",
                myDate: "please enter date in this format \"dd/MM/yyyy\"",
            }
        }
    });
});