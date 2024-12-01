// Function to format the current date into a readable format including the day of the week
function getCurrentDate() {
    const date = new Date();
    const options = {
        weekday: 'long',  // Full name of the day of the week
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    };
    return date.toLocaleDateString('vi-VN', options);
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