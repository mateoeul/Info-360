document.addEventListener('DOMContentLoaded', function () {
    const checkboxes = document.querySelectorAll('.compare-checkbox');
    const comparisonBar = document.getElementById('comparison-bar');
    const comparisonList = document.getElementById('comparison-list');

    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            const universityId = this.getAttribute('data-id');
            const universityName = this.getAttribute('data-nombre');
            const universityLogo = this.getAttribute('data-logo');

            if (this.checked) {
                // Crear un elemento para la universidad seleccionada
                const comparisonItem = document.createElement('div');
                comparisonItem.className = 'comparison-item';
                comparisonItem.setAttribute('data-id', universityId);

                comparisonItem.innerHTML = `
                    <img src="${universityLogo}" alt="${universityName}">
                    <span>${universityName}</span>
                `;

                comparisonList.appendChild(comparisonItem);
                comparisonBar.classList.remove('hidden');
            } else {
                // Eliminar la universidad de la barra
                const itemToRemove = document.querySelector(`.comparison-item[data-id="${universityId}"]`);
                if (itemToRemove) {
                    itemToRemove.remove();
                }

                // Ocultar la barra si no hay universidades seleccionadas
                if (comparisonList.children.length === 0) {
                    comparisonBar.classList.add('hidden');
                }
            }
        });
    });
});