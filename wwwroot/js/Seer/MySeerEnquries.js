document.getElementById('unfinishedCheckbox').addEventListener('change', function () {
    const url = new URL(window.location.href);
    if (this.checked) {
        url.searchParams.set('showUnfinishedOnly', 'true');
    } else {
        url.searchParams.delete('showUnfinishedOnly');
    }
    window.location.href = url.toString();
});

document.getElementById('statusFilterDropdown').addEventListener('change', function () {
    const selectedStatus = this.value;
    const url = new URL(window.location.href);

    if (selectedStatus) {
        url.searchParams.set('statusFilter', selectedStatus);
    } else {
        url.searchParams.delete('statusFilter');
    }

    window.location.href = url.toString();
});

function showAnswerBox(enquiryId, enquiryStatusId, userId) {
    console.log(':pppp');
    if (enquiryStatusId === 3) {
        document.querySelectorAll('[id^="answerBox-"]').forEach(box => {
            box.style.display = "none";
        });
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
                location.reload();
            },
            error: function () {
                alert("An error occurred while updating the enquiry.");
            }
        });
    }
}

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
            location.reload();
        },
        error: function () {
            alert("An error occurred while submitting the answer.");
        }
    });
}
