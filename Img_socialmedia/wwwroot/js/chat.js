"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/comment").build();

//Disable send button until connection is established
document.getElementByClassName("btn-send").disabled = true;

connection.on("recevie", function (content) {

    var li = document.createElement("li");
    li.textContent = content;
    document.getElementById("user-comment-layout").appendChild(li);
});

connection.start().then(function () {
    document.getElementByClassName("btn-send").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementByClassName("btn-send").addEventListener("click", function (event) {
    var content = document.getElementByClassName("comment-content").value;
    connection.invoke("Commentation", content).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});