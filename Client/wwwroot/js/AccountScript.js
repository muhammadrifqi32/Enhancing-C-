function togglePassword() {
            const passwordInput = document.getElementById("password");
            const toggleIcon = document.getElementById("toggleIcon");

            const isPassword = passwordInput.type === "password";
            passwordInput.type = isPassword ? "text" : "password";
            toggleIcon.classList.toggle("fa-eye");
            toggleIcon.classList.toggle("fa-eye-slash");
        }

$(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault();

        const email = $('input[type="email"]').val();
        const password = $('#password').val();

        // Tampilkan loader sebelum AJAX
        Swal.fire({
            title: 'Memproses login...',
            allowOutsideClick: false,
            didOpen: () => {
                Swal.showLoading();
            }
        });

        $.ajax({
            url: '/Accounts/Login',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ email, password }),
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Login Berhasil',
                        text: response.message,
                        showConfirmButton: false,
                        timer: 750
                    });

                    setTimeout(() => {
                        window.location.href = 'https://localhost:7021/dashboards';
                    }, 750);
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Login Gagal',
                        text: response.message
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Login Gagal',
                    text: 'Terjadi kesalahan pada server'
                });
            }
        });
    });
});
