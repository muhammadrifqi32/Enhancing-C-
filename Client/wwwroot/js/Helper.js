function showSuccessAlert(message, timer = 1500) {
    Swal.fire({
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer
    });
}

function showErrorAlert(message, timer = 2000) {
    Swal.fire({
        icon: 'error',
        title: message,
        showConfirmButton: false,
        timer
    });
}
