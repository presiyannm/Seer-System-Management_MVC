document.addEventListener("DOMContentLoaded", () => {
    const registerForm = document.getElementById("registerForm");
    registerForm.addEventListener("submit", (event) => {
        if (!validateForm()) {
            event.preventDefault();
        }
    });
});

function validateForm() {
    let isValid = true;

    const username = document.getElementById("UserName").value.trim();
    const email = document.getElementById("Email").value.trim();
    const password = document.getElementById("Password").value.trim();
    const confirmPassword = document.getElementById("ConfirmPassword").value.trim();
    const firstName = document.getElementById("FirstName").value.trim();
    const lastName = document.getElementById("LastName").value.trim();

    clearErrors();

    if (username === "") {
        setError("UserName", "Потребителското име е задължително.");
        isValid = false;
    }

    if (email === "") {
        setError("Email", "Имейлът е задължителен.");
        isValid = false;
    }

    if (password === "") {
        setError("Password", "Паролата е задължителна.");
        isValid = false;
    }

    if (confirmPassword === "") {
        setError("ConfirmPassword", "Потвърждаването на паролата е задължително.");
        isValid = false;
    }

    if (firstName === "") {
        setError("FirstName", "Името е задължително.");
        isValid = false;
    }

    if (lastName === "") {
        setError("LastName", "Фамилията е задължителна.");
        isValid = false;
    }

    if (password !== confirmPassword) {
        setError("ConfirmPassword", "Паролите не съвпадат.");
        isValid = false;
    }

    return isValid;
}

function setError(fieldId, message) {
    const field = document.getElementById(fieldId);
    const errorSpan = field.nextElementSibling;
    errorSpan.textContent = message;
}

function clearErrors() {
    const errorSpans = document.querySelectorAll(".text-danger");
    errorSpans.forEach((span) => {
        span.textContent = "";
    });
}
