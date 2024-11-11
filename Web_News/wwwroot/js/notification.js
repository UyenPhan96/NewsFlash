// Khởi tạo kết nối SignalR
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7005/notificationHub")
    .build();


// Khi có thông báo mới từ SignalR, thêm thông báo vào danh sách
connection.on("ReceiveNotification", (advertisementId, contactName, content, createdDate) => {
    const notificationList = document.getElementById('notificationList');

    // Tạo phần tử thông báo mới
    const notificationItem = document.createElement('a');
    notificationItem.className = 'dropdown-item preview-item notification-item-new'; // Thêm class cho thông báo chưa đọc
    notificationItem.href = `/Contact/Details/${advertisementId}`;
    notificationItem.onclick = (event) => {
        event.preventDefault();
        markAsRead(advertisementId);
    };

    notificationItem.innerHTML = `
        <div class="preview-thumbnail">
            <div class="preview-icon bg-success">
                <i class="mdi mdi-calendar"></i>
            </div>
        </div>
        <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
            <h6 class="preview-subject mb-1">${contactName}</h6>
            <p class="content-preview">${content}</p>
            <p class="text-gray small">${new Date(createdDate).toLocaleString()}</p>
        </div>
    `;

    // Thêm thông báo vào đầu danh sách
    notificationList.prepend(notificationItem);

    // Giới hạn số lượng thông báo hiển thị tối đa là 5
    const notificationItems = notificationList.getElementsByClassName('preview-item');
    if (notificationItems.length > 5) {
        notificationList.removeChild(notificationItems[notificationItems.length - 1]);
    }
});

// Hàm tải danh sách thông báo ban đầu
async function loadNotifications() {
    const response = await fetch('/Contact/GetRecentNotifications');
    const notifications = await response.json();

    const notificationList = document.getElementById('notificationList');
    notificationList.innerHTML = '';

    notifications.forEach(notification => {
        const notificationItem = document.createElement('a');
        notificationItem.className = `dropdown-item preview-item ${notification.isRead ? 'notification-item-read' : 'notification-item-new'}`; // Class tùy thuộc vào trạng thái thông báo
        notificationItem.href = `/Contact/Details/${notification.advertisementId}`;
        notificationItem.onclick = (event) => {
            event.preventDefault();
            markAsRead(notification.advertisementId);
        };

        notificationItem.innerHTML = `
            <div class="preview-thumbnail">
                <div class="preview-icon bg-success">
                    <i class="mdi mdi-calendar"></i>
                </div>
            </div>
            <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                <h6 class="preview-subject mb-1">${notification.contactName}</h6>
                <p class="content-preview">${notification.content}</p>
                <p class="text-gray small">${new Date(notification.createdDate).toLocaleString()}</p>
            </div>
        `;

        // Thêm thông báo vào danh sách
        notificationList.appendChild(notificationItem);
    });

    // Giới hạn số lượng thông báo hiển thị tối đa là 5
    const notificationItems = notificationList.getElementsByClassName('preview-item');
    if (notificationItems.length > 5) {
        notificationList.removeChild(notificationItems[notificationItems.length - 1]);
    }
}

// Đánh dấu thông báo là đã đọc và điều hướng đến trang chi tiết
async function markAsRead(advertisementId) {
    await fetch(`/Contact/MarkAsRead?advertisementId=${advertisementId}`, {
        method: 'POST'
    });

    window.location.href = `/Admin/Advertisement/Approve/${advertisementId}`;
}

// Gọi hàm loadNotifications khi trang được tải
document.addEventListener('DOMContentLoaded', loadNotifications);
