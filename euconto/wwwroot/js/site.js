//Likes Actions
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

//Commentaries Actions
function Comentaries(InteractionId, UserName, UserId) {
    if (document.getElementById('Comentaries-' + InteractionId).style.display === "block") {
        document.getElementById('Comentaries-' + InteractionId).innerHTML = '';
        document.getElementById('Comentaries-' + InteractionId).style.display = "none";

        return;
    }

    var LayoutBtnCommentary = '<div class="media mt-3 shadow-textarea" id="NewComentarie-' + InteractionId + '">' +
                            '<img class="d-flex rounded-circle avatar z-depth-1-half mr-3" src="https://mdbootstrap.com/img/Photos/Avatars/avatar-8.jpg"' +
                                    'alt="Generic placeholder image">' +
                            '<div class="media-body">' +
                                '<h5 class="mt-0 font-weight-bold blue-text"><a href="/profile?userId=' + UserId + '">@' + UserName + '</a></h5>' +
                                '<div class="form-group basic-textarea rounded-corners">' +
                                    '<textarea id="CommentaryToSave-' + InteractionId + '" class="form-control z-depth-1" rows="3" placeholder="Escreva seu comentário..."></textarea>' +
                                    '<a class="btn btn-outline-black btn-sm float-md-right waves-effect" onclick="SaveInteraction(\'' + InteractionId + '\')"> Comentar</a>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<hr />';

    var Layout = LayoutBtnCommentary;

    div = document.getElementById('Comentaries-' + InteractionId);
    div.insertAdjacentHTML('beforeend', Layout);

    InteractionComentaries(InteractionId);

    document.getElementById('Comentaries-' + InteractionId).style.display = "block";
}

function InteractionComentaries(InterectionId) {
    $.post("InteractionById/", { interactionid: InterectionId }, function (result, status) {
        result.interactions.forEach(function (element, index, array) {
            var LayoutReturn = '<div class="media mt-3 shadow-textarea" id="ComentarieId-' + element.commentarieId + '">' +
                                    '<img class="d-flex rounded-circle avatar z-depth-1-half mr-3" src = "https://mdbootstrap.com/img/Photos/Avatars/avatar-10.jpg"' +
                                    'alt = "Avatar" >' +
                                    '<div class="media-body">' +
                                        '<h5 class="mt-0 font-weight-bold blue-text"><a href="/profile?userId=' + element.userId + '">@' + element.userName + '</a></h5>' +
                                        element.text +
                                        '<hr />';
                                        if(element.isEditable){
                                            LayoutReturn += '<a class="btn btn-outline-black btn-sm waves-effect" onclick="DeleteInteractionComent(\'' + element.commentarieId + '\', \'' + InterectionId + '\' )"> Excluir</a>';
                                        }                                            

                                    if (element.subInteractionId != "") {
                                        LayoutReturn += InteractionComentaries(element.subInteractionId);
                                    }

            LayoutReturn +=         '</div>' +
                                '</div >';

            document.getElementById('Comentaries-' + InterectionId).insertAdjacentHTML('beforeend', LayoutReturn);
        });
    });
}

function SaveInteraction(interactionID, UserName, UserId) {
    var commentaryText = document.getElementById('CommentaryToSave-' + interactionID).value;
                
    $.post("SaveInteraction/", {
        interactionid: interactionID,
        commentarytext: commentaryText
    }, function (result, status) {
        refNode = document.getElementById('NewComentarie-' + interactionID);

        var Layout = '<div class="media mt-3 shadow-textarea" id="ComentarieId-' + result.commentarieId + '">' +
                            '<img class="d-flex rounded-circle avatar z-depth-1-half mr-3" src = "https://mdbootstrap.com/img/Photos/Avatars/avatar-10.jpg"' +
                            'alt = "Avatar" >' +
                            '<div class="media-body">' +
                                '<h5 class="mt-0 font-weight-bold blue-text"><a href="/profile?userId=' + result.userId + '">@' + result.userName + '</a></h5>' +
                                result.text +
                                '<hr />'+
                            '<a class="btn btn-outline-black btn-sm waves-effect" onclick="DeleteInteractionComent(\'' + result.commentarieId + '\', \'' + interactionID + '\' )"> Excluir</a>'+
                            '</div>' +
                        '</div >';

        refNode.insertAdjacentHTML('afterend', Layout);

        var commentaryText = document.getElementById('CommentaryToSave-' + interactionID).value = "";
        document.getElementById("ActionComentaries-" + interactionID).innerText = (parseInt(document.getElementById("ActionComentaries-" + interactionID).innerText) + 1);
    });
}

function DeleteInteractionComent(commentarieId, interactionID) {       
    $.post("DeleteInteraction/", {
        commentarieid: commentarieId
    }, function (result, status) {
        document.getElementById("ComentarieId-" + commentarieId).remove();
        document.getElementById("ActionComentaries-" + interactionID).innerText = (parseInt(document.getElementById("ActionComentaries-" + interactionID).innerText) - 1);
    });
}