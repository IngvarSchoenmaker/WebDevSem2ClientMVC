const connection = new signalR.HubConnectionBuilder()
    .withUrl("/unoHub")
    .build();

connection.start().then(function () {
    console.log("Connected to UnoHub");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("TableCreated", function (tableName) {
    // Voeg de nieuwe tafel toe aan de lijst
    updateTableList(tableName);
});

function updateTableList(tableName) {
    // Voeg hier de logica toe om de lijst van tafels bij te werken
    // bijvoorbeeld door een nieuwe tabel toe te voegen aan een HTML-element.
}
