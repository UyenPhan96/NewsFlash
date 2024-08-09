// Function to format the current date into a readable format
function getCurrentDate() {
    const date = new Date();
    return date.toLocaleDateString('vi-VN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    });
}

// Function to update date-time elements with the current date
function updateCurrentDate() {
    const dateTimeElements = document.querySelectorAll('.date-time');
    dateTimeElements.forEach(element => {
        element.textContent = getCurrentDate();
    });
}

// Call the function to update dates when the page loads
window.onload = updateCurrentDate;

