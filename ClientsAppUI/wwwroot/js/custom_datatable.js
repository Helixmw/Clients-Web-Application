window.initializeDataTable = () => {
    document.addEventListener("DOMContentLoaded", function () {
        const table = document.getElementById('clientsTable');
        if (table && window.DataTable) {
            new DataTable(table);
        } else {
            console.error("DataTable not loaded or table not found");
        }
    });
};
