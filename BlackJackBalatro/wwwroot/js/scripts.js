async function gameStart() {
    document.getElementById("start").disabled = true;

    await fetch(`/Deck/Reset`);

    for (let i = 0; i < 2; i++) {

        await loadPartial(`/Deck/ComputerDraw`, "comp-area");
        await loadPartial(`/Deck/PlayerNormalDraw`, "player-area");
    }
    document.getElementById("standButton").disabled = false;
    await loadPartial('/Deck/statRefresh', "statDiv");
}

async function stand() {

    await loadPartial(`/Deck/ComputerTurn`, "comp-area");
    await loadPartial('/Deck/statRefresh', "statDiv");

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

    await loadPartial('/Deck/statRefresh', "statDiv");
    checkForLoseScreen();

    document.getElementById("betInput").disabled = true;
    document.getElementById("betBtn").disabled = true;
}

window.onload = () => {
    document.getElementById("standButton").disabled = true;
    document.getElementById("norm").disabled = true;
    document.getElementById("evil").disabled = true;
    document.getElementById("bonus").disabled = true;
    document.getElementById("betInput").disabled = true;
    document.getElementById("betBtn").disabled = true;
};

window.enableStartButton = function () {
    const btn = document.getElementById("start");
    if (btn) btn.disabled = false;

}
function checkForLoseScreen() {
    const loseScreen = document.querySelector("#player-area #lose-screen");
    if (loseScreen) {
        window.location.assign("/Deck/GameView");
    }
}