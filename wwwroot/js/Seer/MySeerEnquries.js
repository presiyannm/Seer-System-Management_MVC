function showAnswerBox(enquiryId, enquiryStatusId, userId) {
    // If the status is 3, show the answer box
    if (enquiryStatusId === 3) {
        // Hide all other answer boxes
        document.querySelectorAll('[id^="answerBox-"]').forEach(box => {
            box.style.display = 'none';
        });

        // Show the answer box for the specific enquiry
        const answerBox = document.getElementById(`answerBox-${enquiryId}`);
        answerBox.style.display = 'block';
    } else {
        console.log('Enquiry status is not 3, so no answer box is shown.');
        $.ajax(
            {
                url: '/Seer/UpdateEnquiryById',
                async: false,
                data: {
                    'enquiryId': enquiryId,
                    'userId': userId,
                    'answer': null
                },
            });
        location.reload();
    }
}

function submitAnswer(enquiryId, userId) {
    const answerText = document.getElementById(`answerText-${enquiryId}`).value;

    if (!answerText) {
        alert("Моля, въведете вашето гадателство.");
        return;
    }

    // Send the answer to the server using AJAX
    $.ajax(
        {
            url: '/Seer/UpdateEnquiryById',
            async: false,
            data: {
                'enquiryId': enquiryId,
                'userId': userId,
                'answer': answerText
            },
        });
    location.reload();
}
