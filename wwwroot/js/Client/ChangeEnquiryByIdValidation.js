document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("change-enquiry-form");

    form.addEventListener("submit", function (event) {
        clearErrors();

        let isValid = true;

        const clientName = document.getElementById("ClientName").value.trim();
        if (!clientName) {
            showError("ClientNameError", "Полето 'Пълно име' е задължително.");
            isValid = false;
        }

        const clientBirthDate = document.getElementById("ClientBirthDate").value;
        if (!clientBirthDate) {
            showError("ClientBirthDateError", "Полето 'Рожденна дата' е задължително.");
            isValid = false;
        }

        const description = document.getElementById("Description").value.trim();
        if (!description) {
            showError("DescriptionError", "Полето 'Въпрос' е задължително.");
            isValid = false;
        }

        const enquiryTypeId = document.getElementById("EnquiryTypeId").value;
        if (!enquiryTypeId) {
            showError("EnquiryTypeIdError", "Моля, изберете вид гадаене.");
            isValid = false;
        }

        if (!isValid) {
            event.preventDefault();
        }
    });

    function showError(elementId, message) {
        const errorElement = document.getElementById(elementId);
        errorElement.textContent = message;
    }

    function clearErrors() {
        const errorElements = document.querySelectorAll(".text-danger");
        errorElements.forEach(function (element) {
            element.textContent = "";
        });
    }
});
