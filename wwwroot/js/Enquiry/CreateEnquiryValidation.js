document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('create-enquiry-form');

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        clearErrorMessages();

        const clientName = document.getElementById('ClientName');
        const clientBirthDate = document.getElementById('ClientBirthDate');
        const description = document.getElementById('Description');
        const enquiryTypeId = document.getElementById('EnquiryTypeId');
        const seerId = document.getElementById('SeerId');

        let isValid = true;

        if (!clientName.value.trim()) {
            setErrorMessage('ClientNameError', 'Пълно име е задължително');
            isValid = false;
        }

        if (!clientBirthDate.value.trim()) {
            setErrorMessage('ClientBirthDateError', 'Дата на раждане е задължителна');
            isValid = false;
        }

        if (!description.value.trim()) {
            setErrorMessage('DescriptionError', 'Желан резултат е задължителен');
            isValid = false;
        }

        if (!enquiryTypeId.value) {
            setErrorMessage('EnquiryTypeIdError', 'Изборът на вид гадаене е задължителен');
            isValid = false;
        }

        if (!seerId.value) {
            setErrorMessage('SeerIdError', 'Изборът на гадател е задължителен');
            isValid = false;
        }


        if (isValid) {
            form.submit();
        }
    });

    function setErrorMessage(elementId, message) {
        const errorElement = document.getElementById(elementId);
        errorElement.textContent = message;
    }

    function clearErrorMessages() {
        const errorMessages = document.querySelectorAll('.text-danger');
        errorMessages.forEach(function (errorMessage) {
            errorMessage.textContent = '';
        });
    }
});
