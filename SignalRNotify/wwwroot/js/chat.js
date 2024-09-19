"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();
var connId = "";
//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("SetConnId", function(user, msg){
    connId = user;
    console.log('connid', user)
    console.log('connid', msg)
});
connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${connId} says ${message}`;
});
connection.onclose(async (err) => {
    console.log("vue conn error:", err);
    await connection.start();
  });

connection.start().then(function () {
    
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("triggerModal").addEventListener("click", function (event) {    
    connection.invoke("TriggerModal", connId, true).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendNotify", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});