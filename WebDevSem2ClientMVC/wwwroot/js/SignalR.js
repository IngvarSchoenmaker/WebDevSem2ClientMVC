// wwwroot/js/signalr.js
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/lobbyHub")
    .build();

connection.start().then(() => {
    console.log("Verbonden met SignalR.");

    connection.on("TableCreated", (newTable) => {
        console.log("Nieuwe tafel is toegevoegd:", newTable);
        // Roep een functie aan om de lijst met tafels weer te geven
        addTableToList(newTable);

    });
    connection.on("PlayerJoinedTable", function (tableId) {
        // Implementeer de logica om te reageren op het feit dat een speler aan een tafel is toegevoegd
        console.log("Player joined table:", tableId);
    });
}).catch(error => {
    console.error("Fout bij het verbinden met SignalR:", error);
});

function addTableToList(newTable) {
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
