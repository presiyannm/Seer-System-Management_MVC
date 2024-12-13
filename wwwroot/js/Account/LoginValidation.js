document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.getElementById("account");
    loginForm.addEventListener("submit", (event) => {
        if (!validateLoginForm()) {
            event.preventDefault();
        }
    });
});

function validateLoginForm() {
    let isValid = true;

    const username = document.getElementById("UserName").value.trim();
    const password = document.getElementById("Password").value.trim();

    clearErrors();

    if (username === "") {
        setError("UserName", "Потребителското име е задължително.");
        isValid = false;
    }

    if (password === "") {
        setError("Password", "Паролата е задължителна.");
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
