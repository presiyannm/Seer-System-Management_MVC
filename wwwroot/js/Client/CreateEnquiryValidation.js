document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("create-enquiry-form");

    form.addEventListener("submit", function (event) {
        // Clear previous error messages
        clearErrors();

        // Validate fields
        let isValid = true;

        // Validate Client Name
        const clientName = document.getElementById("ClientName").value.trim();
        if (!clientName) {
            showError("ClientNameError", "Полето 'Пълно име' е задължително.");
            isValid = false;
        }

        // Validate Client Birth Date
        const clientBirthDate = document.getElementById("ClientBirthDate").value;
        if (!clientBirthDate) {
            showError("ClientBirthDateError", "Полето 'Дата на раждане' е задължително.");
            isValid = false;
        }

        // Validate Description
        const description = document.getElementById("Description").value.trim();
        if (!description) {
            showError("DescriptionError", "Полето 'Описание на въпроса' е задължително.");
            isValid = false;
        }

        // Validate Wanted Result
        const wantedResult = document.getElementById("WantedResult").value.trim();
        if (!wantedResult) {
            showError("WantedResultError", "Полето 'Желан резултат' е задължително.");
            isValid = false;
        }

        // Validate Enquiry Type
        const enquiryTypeId = document.getElementById("EnquiryTypeId").value;
        if (!enquiryTypeId) {
            showError("EnquiryTypeIdError", "Моля, изберете вид гадаене.");
            isValid = false;
        }

        // Validate Seer
        const seerId = document.getElementById("SeerId").value;
        if (!seerId) {
            showError("SeerIdError", "Моля, изберете гадател.");
            isValid = false;
        }

        // Prevent form submission if validation fails
        if (!isValid) {
            event.preventDefault();
        }
    });

    // Function to display error messages
    function showError(elementId, message) {
        const errorElement = document.getElementById(elementId);
        errorElement.textContent = message;
    }

    // Function to clear all error messages
    function clearErrors() {
        const errorElements = document.querySelectorAll(".text-danger");
        errorElements.forEach(function (element) {
            element.textContent = "";
        });
    }
});