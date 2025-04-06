window.bootstrapInterop = {
    closeModal: function (selector) {
        var modalEl = document.querySelector(selector);
        if (modalEl) {
            var modalInstance = bootstrap.Modal.getInstance(modalEl);
            if (modalInstance) {
                modalInstance.hide();
            }
        }
    },
    openModal: function (selector) {
        var modalEl = document.querySelector(selector);
        if (modalEl) {
            var modalInstance = new bootstrap.Modal(modalEl);
            modalInstance.show();
        }
    }
};
