// wwwroot/js/signalr.js
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/lobbyHub")
    .build();

const urlParams = new URLSearchParams(window.location.search);

connection.start().then(() => {
    console.log("Verbonden met SignalR.");

    connection.on("TableCreated", (newTable) => {
        console.log("Nieuwe tafel is toegevoegd:", newTable);
        // Roep een functie aan om de lijst met tafels weer te geven
        AddTableToList(newTable);

    });
    connection.on("PlayerJoined", function () {
        // Implementeer de logica om te reageren op het feit dat een speler aan een tafel is toegevoegd
        console.log("Player joined table");
        gameId = urlParams.get('TableId');
        connection.invoke("PlayerJoined", gameId).catch(error => {
            console.error("Fout bij updaten lijst:", error);
        });

    });
    connection.on("AddPlayerToList", function (list) {
        console.log("Lijst verversen");
        AddPlayerToList(list);
    });
    if (gameId = urlParams.get('TableId')) {
        console.log("De volgende tafel wordt gejoined: ", gameId);
        connection.invoke("JoinGroup", gameId).catch(error => {
            console.error("Fout bij het toevoegen aan groep:", error);
        });
    }

}).catch(error => {
    console.error("Fout bij het verbinden met SignalR:", error);
});
connection.onclose(function (e) {
    removePlayer();
    // This function will be called when the connection is closed
    console.error("Connection closed: ", e);
    // You can perform any cleanup or additional actions here
});
function removePlayer() {
    // Voeg hier code toe om de speler te verwijderen, bijvoorbeeld SignalR hub aanroepen
    var playerId = document.getElementById("playerId").value;
    connection.invoke("RemovePlayerFromList", playerId); // Vervang met de werkelijke speler-ID
    removePlayer();
}
function AddTableToList(newTable) {
    // Voeg de nieuwe tafel toe aan de lijst
    const tableList = document.getElementById("tableList");
    const newTablePlaceholder = document.getElementById("newTablePlaceholder");

    // Maak een nieuw lijstelement aan voor de nieuwe tafel
    const listItem = document.createElement("li");
    listItem.innerHTML = `
        <a href="/Game/JoinTable/${newTable.tableId}">
            Tafel: ${newTable.tableName} - Max aantal spelers: ${newTable.numberOfPlayers}
        </a>
    `;

    // Voeg het nieuwe lijstelement toe vóór het placeholder-element
    tableList.insertBefore(listItem, newTablePlaceholder);
}
function AddPlayerToList(players) {
    console.log("Lijst verversen");
    $('#playerList').empty();
    $('#playerCount').empty();

    // Voeg alle spelers opnieuw toe
    $('#playerCount').append(players.length);
    players.forEach(function (player) {
        $('#playerList').append('<li id="'+player.playerId+'">' + player.playerId + '</li>');
    });
}
function removePlayer(player) {
    $('#playerList').find('#' + player.playerId).remove();
    var count = document.getElementById('#playerCount').value;
    $('#playerCount').empty();
    $('#playerCount').append(count -1);
}
