const searchInput = document.getElementById('searchInput');
const tableBody = document.querySelector('#parameterTable tbody');
let timeoutId;
// Store all original rows so we can reset easily
let originalRows = [];

function saveOriginalRows() {
    originalRows = Array.from(tableBody.querySelectorAll('.tableRow'));
}

function filterTable(searchTerm) {
    const term = searchTerm.toLowerCase().trim();

    if (term === '') {
        // Reset to full table
        tableBody.innerHTML = '';
        originalRows.forEach(row => tableBody.appendChild(row));
        return;
    }

    // Filter rows
    const filteredRows = originalRows.filter(row => {
        const rowText = row.textContent.toLowerCase();
        return rowText.includes(term);
    });

    tableBody.innerHTML = '';
    filteredRows.forEach(row => tableBody.appendChild(row));
}

// Initialize
window.addEventListener('load', () => {
    saveOriginalRows();

    searchInput.addEventListener('input', (e) => {
        // debounce search
        if (timeoutId) clearTimeout(timeoutId);
        timeoutId = setTimeout(() => {
            filterTable(e.target.value)
            timeoutId = null;
        }, 1000);
    });
});