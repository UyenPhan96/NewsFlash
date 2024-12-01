document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('form').addEventListener('submit', function (event) {
        event.preventDefault(); // Ngăn chặn gửi form mặc định

        // Gửi dữ liệu đến máy chủ và nhận phản hồi
        fetch('/api/register', {
            method: 'POST',
            body: new FormData(event.target),
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    // Hiển thị modal thành công nếu đăng ký thành công
                    const modal = new bootstrap.Modal(document.getElementById('registerSuccessModal'));
                    modal.show();

                    // Tự động chuyển hướng sau một khoảng thời gian (ví dụ: 3 giây)
                    setTimeout(function () {
                        window.location.href = '/'; // Đổi thành URL của trang chủ hoặc trang mong muốn
                    }, 3000); // 3000 milliseconds = 3 seconds
                } else {
                    // Không hiển thị modal nếu có lỗi
                    console.error('Đăng ký không thành công:', data.message);
                    // Có thể hiển thị thông báo lỗi cho người dùng nếu muốn
                }
            })
            .catch(error => {
                // Xử lý lỗi nếu có
                console.error('Có lỗi xảy ra:', error);
                // Có thể hiển thị thông báo lỗi cho người dùng nếu muốn
            });
    });
});
