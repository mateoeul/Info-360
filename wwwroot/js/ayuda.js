function toggleAnswer(arrow) {
    const faqItem = arrow.closest('.faq-item');
    if (faqItem) {
        faqItem.classList.toggle('open');
        console.log('Clase toggleada:', faqItem.classList);
    } else {
        console.error('Error al encontrar el contenedor FAQ.');
    }
}
