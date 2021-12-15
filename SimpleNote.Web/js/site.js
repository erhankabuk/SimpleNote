/*VARIABLES */
var apiUrl = "https://localhost:5001/api/Notes";
var notes = [];
var activeNote = null;
var activeLink = null;
/*FUNCTIONS */
function loadNotes() {
    $.ajax({
        url: apiUrl,
        type: "GET",
        success: function (data) {
            notes = data;
            displayNotes();
        }
    });
}
/*
function displayNotes(){
    $("notes").html("");
    for(const note of notes){
        $("#notes").append(`
        <a href="#" class="list-group-item list-group-item-action">
        ${note.title}
        </a>`)
    }
}
*/


function displayNotes() {
    $("#notes").html("");
    for (const note of notes) {
        var a = createLink(note);
        $("#notes").append(a);
    }
    $("#notes>a:first-child").trigger("click");
}

function createLink(note) {
    var a = $("<a/>")
        .addClass("list-group-item list-group-item-action")
        .text(note.title)
        .attr("href", "#")
        .click(function (event) {
            event.preventDefault();
            activeLink = this;
            $(this).addClass("active");
            $(this).siblings().removeClass("active");
            displayNote(this.note);
        });
    a[0].note = note;
    return a[0];
}

function displayNote(note) {
    activeNote = note;
    $("#title").val(note.title);
    $("#content").val(note.content);
}


function saveNote(title, content) {
    var sendData = {
        id: activeNote.id,
        title: title,
        content: content
    };

    $.ajax({
        type: "PUT",
        url: apiUrl + "/" + activeNote.id,
        data: JSON.stringify(sendData),
        contentType: "application/json",
        success: function (data) {
            toastr.success("Saved successfully")
            activeLink.textContent = title;
            activeNote.title = title;
            activeNote.content = content;
        }
    });
}


function newNote() {
    var sendData = {
        title: "New Note",
        content: ""
    };

    $.ajax({
        type: "POST",
        url: apiUrl,
        data: JSON.stringify(sendData),
        contentType: "application/json",
        success: function (data) {
            toastr.success("Created successfully")
            notes.unshift(data);
            displayNotes();
        }
    });
}


function deleteNote() {
    $.ajax({
        url: apiUrl + "/" + activeNote.id,
        type: "DELETE",
        success: function () {
            toastr.success("Deleted successfully");
            var i = notes.indexOf(activeNote);
            notes.splice(i,1);
            activeNote=null;
            displayNotes();


        }


    });
}

/*EVENTS */
$("#btnSave").click(function () {
    if (!activeNote) return;
    var title = $("#title").val().trim();
    var content = $("#content").val();
    if (title == "") {
        toastr.error("Title is required!");
        return;
    }
    saveNote(title, content);
});

$("#btnNew").click(function (event) {
    event.preventDefault();
    newNote();
});
$("#btnDelete").click(function (event) {
    event.preventDefault();
    if (!activeNote) return;
    deleteNote();
});
/*INITIALIZATIONS */

loadNotes();

