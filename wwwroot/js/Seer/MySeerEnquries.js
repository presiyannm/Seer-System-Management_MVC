// Handle checkbox for unfinished statuses
document.getElementById('unfinishedCheckbox').addEventListener('change', function () {
    const url = new URL(window.location.href);
    if (this.checked) {
        url.searchParams.set('showUnfinishedOnly', 'true');
    } else {
        url.searchParams.delete('showUnfinishedOnly');
    }
    window.location.href = url.toString();
});

// Handle status filter dropdown
document.getElementById('statusFilterDropdown').addEventListener('change', function () {
    const selectedStatus = this.value; // Get the selected status from the dropdown
    const url = new URL(window.location.href); // Create a URL object for the current page

    if (selectedStatus) {
        url.searchParams.set('statusFilter', selectedStatus); // Add or update the 'statusFilter' parameter
    } else {
        url.searchParams.delete('statusFilter'); // Remove the 'statusFilter' parameter if "Всички" is selected
    }

    window.location.href = url.toString(); // Reload the page with the updated query string
});

// Function to show the answer box dynamically
function showAnswerBox(enquiryId, enquiryStatusId, userId) {
    console.log(':pppp');
    if (enquiryStatusId === 3) {
        // Hide all other answer boxes
        document.querySelectorAll('[id^="answerBox-"]').forEach(box => {
            box.style.display = "none";
        });
        // Show the answer box for the specific enquiry
        const answerBox = document.getElementById(`answerBox-${enquiryId}`);
        if (answerBox) {
            answerBox.style.display = "block";
        }
    } else {
        console.log('Enquiry status is not 3, so no answer box is shown.');
        $.ajax({
            url: '/Seer/UpdateEnquiryById',
            async: false,
            data: {
                'enquiryId': enquiryId,
                'userId': userId,
                'answer': null
            },
            success: function () {
                location.reload(); // Reload the page after AJAX call
            },
            error: function () {
                alert("An error occurred while updating the enquiry.");
            }
        });
    }
}

// Function to submit the answer via AJAX
function submitAnswer(enquiryId, userId) {
    const answerText = document.getElementById(`answerText-${enquiryId}`).value;
    if (!answerText) {
        alert("Моля, въведете вашето гадателство.");
        return;
    }
    $.ajax({
        url: '/Seer/UpdateEnquiryById',
        async: false,
        data: {
            'enquiryId': enquiryId,
            'userId': userId,
            'answer': answerText
        },
        success: function () {
            location.reload(); // Reload the page after successful submission
        },
        error: function () {
            alert("An error occurred while submitting the answer.");
        }
    });
}