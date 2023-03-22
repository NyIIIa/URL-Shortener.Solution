// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$("#AddNewUrl").click(AddUrl);
$("#DeleteUrl").click(DeleteUrl);

function DeleteUrl(){
    let idUrl = $("#IdUrl").val().trim();
    if(idUrl == null){
        alert("Input an id url!");
    }
    $.ajax({
        type: "DELETE",
        url: `http://localhost:5137/ShortUrl/Delete?idUrl=${idUrl}`,
        success : function (){
            $(`#${idUrl}`).remove();
            alert("The specified url has been successfully deleted!");
        },
        error : function(httpObj) {
            if(httpObj.status == 401){
                alert("The server responded with a status of 401 (Unauthorized)");
            }
            else if(httpObj.status == 403){
                alert("The server responded with a status of 403 (Forbidden)");
            }
            else {
                alert("Error: " + httpObj.responseText);
            }
        }
    });
}
function AddUrl(){
    let longUrl = $("#longUrl").val().trim();
    if(longUrl == null){
        alert("Input a long url!");
    }
    $.ajax({
        type: "POST",
        url: `http://localhost:5137/ShortUrl/Add?url=${longUrl}`,
        success : function (result){
            $("#shorUrlsTable").find("tbody").append(
                `<tr id="${result.id}">` +
                    `<td>${result.id}</td>` +
                    `<td>${result.originalUrl}</td>` +
                    `<td>${result.shortUrl}</td>` +
                    `<td><a href='http://localhost:5137/ShortUrl/Info?originalUrl=${result.originalUrl}'>Info view</a></td>` +
                "</tr>");
            alert("The input url has been added!");
        },
        error : function(httpObj) {
            if(httpObj.status == 401){
                alert("The server responded with a status of 401 (Unauthorized)");
            }
            else if(httpObj.status == 403){
                alert("The server responded with a status of 403 (Forbidden)");
            }
            else {
                alert("Error: " + httpObj.responseText);   
            }
        }
    });
}
