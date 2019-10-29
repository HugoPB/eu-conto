// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ActionLike(StoryId) {
    $.post("UserLikeStory/", { storyId: StoryId }, function (result, status) {
        if (document.getElementById('LikeIcon').classList[0] == "far") {
            document.getElementById('LikeIcon').classList.replace("far", "fas");
            document.getElementById("ActionLike").innerText = (parseInt(document.getElementById("ActionLike").innerText) + 1);
        }
        else {
            document.getElementById('LikeIcon').classList.replace("fas", "far");
            document.getElementById("ActionLike").innerText = (parseInt(document.getElementById("ActionLike").innerText) - 1);
        }
    });
}

function ActionLikeChapter(ChapterId) {
    $.post("UserLikeChapter/", { chapterId: ChapterId }, function (result, status) {
        if (document.getElementById('LikeIcon').classList[0] == "far") {
            document.getElementById('LikeIcon').classList.replace("far", "fas");
            document.getElementById("ActionLike").innerText = (parseInt(document.getElementById("ActionLike").innerText) + 1);
        }
        else {
            document.getElementById('LikeIcon').classList.replace("fas", "far");
            document.getElementById("ActionLike").innerText = (parseInt(document.getElementById("ActionLike").innerText) - 1);
        }
    });
}