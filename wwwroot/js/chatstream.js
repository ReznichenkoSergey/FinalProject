(function () {
    let webSocket
    var getWebSocketMessages = function (onMessageReceived) {
        let url = `wss://${location.host}/stream/get`;
        webSocket = new WebSocket(url);

        webSocket.onmessage = onMessageReceived;
    };

    let isRegistered = false;
    let ulElement = document.getElementById('chatMessages');
    let login;

    getWebSocketMessages(function (message) {
        isRegistered = true;
        ulElement.innerHTML = ulElement.innerHTML += `<li>${message.data}</li>`;
    });

    document.getElementById("sendmessage").addEventListener("click", function () {

        let textElement = document.getElementById("messageTextInput");
        let text = textElement.value;

        if (isRegistered == false) {
            login = text;
            webSocket.send(`{"Login":"${login}", "Content":"", "Registered":${isRegistered}}`);
            //
            isRegistered = true;
            document.getElementById("header").innerHTML = `Send Message (${text})`;
            document.getElementById("sendmessage").value = "Send";
        }
        else {
            webSocket.send(`{"Login":"${login}", "Content":"${text}", "Registered":${isRegistered}}`);
        }
        textElement.value = '';
    });

}());