// wwwroot/js/countdown.js

document.addEventListener('DOMContentLoaded', function () {
    var countdownElement = document.getElementById('time');
    var endTime = new Date().getTime() + 60000; // 1 phút từ bây giờ

    function updateCountdown() {
        var now = new Date().getTime();
        var timeLeft = endTime - now;

        if (timeLeft <= 0) {
            countdownElement.innerHTML = 'Hết thời gian';
            return;
        }

        var minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);
        countdownElement.innerHTML = minutes + ":" + (seconds < 10 ? '0' : '') + seconds;
    }

    updateCountdown();
    setInterval(updateCountdown, 1000);
});
