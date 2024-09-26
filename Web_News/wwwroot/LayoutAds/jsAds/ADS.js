
    document.getElementById('contactForm').addEventListener('submit', function(event) {
            var fileInput = document.querySelector('input[name="MediaFile"]');
    var errorSpan = document.getElementById('imageError');

    if (fileInput.files.length === 0) {
        errorSpan.textContent = 'Vui lòng chọn một ảnh.';
    errorSpan.style.display = 'block';
    event.preventDefault();
    return;
            }

    var file = fileInput.files[0];
    if (file.type.startsWith('image/')) {
                var img = new Image();

    var reader = new FileReader();
    reader.onload = function(e) {
        img.src = e.target.result;
    img.onload = function() {
                        // Kiểm tra kích thước (ví dụ: chiều rộng tối đa 800px, chiều cao tối đa 600px)
                        if (img.width > 300 || img.height > 200) {
        errorSpan.textContent = 'Ảnh phải có kích thước tối đa là 800x600 px.';
    errorSpan.style.display = 'block';
    event.preventDefault();
                        } else {
        errorSpan.style.display = 'none';
                        }
                    };
                };
    reader.readAsDataURL(file);
            } else {
        errorSpan.textContent = 'Vui lòng chọn một ảnh. (Hỗ trợ chỉ hình ảnh)';
    errorSpan.style.display = 'block';
    event.preventDefault();
            }
        });
