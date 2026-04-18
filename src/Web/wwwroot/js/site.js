document.querySelectorAll(".js-menu-form").forEach((form) => {
    const picker = form.querySelector(".js-menu-picker");
    const nameInput = form.querySelector("input[name='name']");
    const priceInput = form.querySelector("input[name='unitPrice']");
    const submitButton = form.querySelector("button[type='submit']");

    if (!picker || !nameInput || !priceInput) {
        return;
    }

    const syncSelection = () => {
        const selected = picker.selectedOptions[0];
        const hasSelection = Boolean(selected);

        nameInput.value = hasSelection ? selected.dataset.name ?? "" : "";
        priceInput.value = hasSelection ? selected.dataset.price ?? "0" : "0";

        if (submitButton) {
            submitButton.disabled = !hasSelection;
        }
    };

    picker.addEventListener("change", syncSelection);
    syncSelection();
});
