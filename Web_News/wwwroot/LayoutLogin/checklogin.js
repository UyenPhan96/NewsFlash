document.addEventListener('DOMContentLoaded', function () {
    const toggleUsernameOrEmail = document.getElementById('toggle-usernameOrEmail');
    const togglePassword = document.getElementById('toggle-password');
    const usernameOrEmailInput = document.getElementById('usernameOrEmail');
    const passwordInput = document.getElementById('password');

    toggleUsernameOrEmail.addEventListener('click', function () {
        if (usernameOrEmailInput.type === 'password') {
            usernameOrEmailInput.type = 'text';
            toggleUsernameOrEmail.querySelector('i').classList.replace('fa-eye-slash', 'fa-eye');
        } else {
            usernameOrEmailInput.type = 'password';
            toggleUsernameOrEmail.querySelector('i').classList.replace('fa-eye', 'fa-eye-slash');
        }
    });

    togglePassword.addEventListener('click', function () {
        if (passwordInput.type === 'password') {
            passwordInput.type = 'text';
            togglePassword.querySelector('i').classList.replace('fa-eye-slash', 'fa-eye');
        } else {
            passwordInput.type = 'password';
            togglePassword.querySelector('i').classList.replace('fa-eye', 'fa-eye-slash');
        }
    });
});
