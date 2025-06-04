// ventas.js - Script para la página de ventas
document.addEventListener('DOMContentLoaded', function () {
    // Agregar clase al body
    document.body.classList.add('ventas-page');

    // Agregar efecto de carga al envío del formulario
    initializeFormSubmission();

    // Agregar animaciones sutiles en scroll
    initializeScrollAnimations();
});

function initializeFormSubmission() {
    const form = document.querySelector('form');
    if (form) {
        form.addEventListener('submit', function () {
            const button = this.querySelector('.btn-elegant');
            const originalContent = button.innerHTML;

            button.innerHTML = `
                <div class="loading-spinner"></div>
                Procesando...
            `;
            button.disabled = true;

            // Re-habilitar después del envío del formulario
            setTimeout(() => {
                button.innerHTML = originalContent;
                button.disabled = false;
            }, 3000);
        });
    }
}

function initializeScrollAnimations() {
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver(function (entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, observerOptions);

    // Observar tarjetas para animación
    document.querySelectorAll('.elegant-card, .stat-card').forEach(card => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(20px)';
        card.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        observer.observe(card);
    });
}