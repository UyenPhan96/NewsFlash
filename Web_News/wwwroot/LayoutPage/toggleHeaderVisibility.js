let lastScrollTop = 0;
const header = document.querySelector('.header-top');
const menu = document.querySelector('.menu');

window.addEventListener('scroll', function () {
    const currentScroll = window.pageYOffset || document.documentElement.scrollTop;

    if (currentScroll > lastScrollTop) {
        // Cu?n xu?ng, ?n thanh header v� thanh menu
        header.style.transform = 'translateY(-100%)';
        menu.style.transform = 'translateY(-170%)';
    } else {
        // Cu?n l�n, hi?n th? l?i thanh header v� thanh menu
        header.style.transform = 'translateY(0)';
        menu.style.transform = 'translateY(0)';
    }

    lastScrollTop = currentScroll <= 0 ? 0 : currentScroll; 
});