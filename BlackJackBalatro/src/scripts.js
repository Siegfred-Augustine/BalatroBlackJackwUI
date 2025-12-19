async function gameStart() {
    document.getElementById("start").disabled = true;

    await fetch(`/Deck/Reset`);

    for (let i = 0; i < 2; i++) {

        const chtml = await fetch(`/Deck/ComputerDraw`);
        const phtml = await fetch(`/Deck/PlayerNormalDraw`);

        document.getElementById("comp-area").innerHTML = await chtml.text();
        document.getElementById("player-area").innerHTML = await phtml.text();

    }
    document.getElementById("standButton").disabled = false;

    window.location.href = "/Deck/GameView"
    window.location.reload();
}

async function stand() {

    const html = await fetch(`/Deck/ComputerTurn`);
    document.getElementById("comp-area").innerHTML = await html.text();

    const phtml = await fetch(`/Deck/PlayerAreaRefresh`);
    document.getElementById("player-area").innerHTML = await phtml.text();
    //window.location.assign("/Deck/GameView");

    window.location.assign("/Deck/GameView");

    document.getElementById("start").disabled = false;
    document.getElementById("standButton").disabled = true;
    document.getElementById("norm").disabled = true;
    document.getElementById("evil").disabled = true;
    document.getElementById("bonus").disabled = true;
    document.getElementById("betInput").disabled = true;
}

async function loadPartial(url, targetId) {
    const response = await fetch(url);
    const html = await response.text();
    document.getElementById(targetId).innerHTML = html;
}

async function drawCard(action) {
    const response = await fetch(`/Deck/${action}`);
    document.getElementById("player-area").innerHTML = await response.text();

    const stats = await fetch(`/Deck/statRefresh`)
    document.getElementById("statDiv").innerHTML = await stats.text();
}

async function placeBet() {
    let amount = document.getElementById("betInput").value;

    if (!amount || amount <= 0 || amount > @Model.Player.chips) {
        alert("Enter a valid bet!");
        return;
    }

    const result = await fetch(`/Deck/PlaceBet?amount=${amount}`);

    // Refresh Stats area
    const stats = await fetch(`/Deck/statRefresh`);
    document.getElementById("statDiv").innerHTML = await stats.text();

    document.getElementById("betInput").disabled = true;
    document.getElementById("betBtn").disabled = true;
}