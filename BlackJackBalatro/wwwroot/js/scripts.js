async function gameStart() {
    document.getElementById("start").disabled = true;

    await fetch(`/Deck/Reset`);
    await loadPartial(`/Deck/ComputerDraw`, "comp-area");

    for (let i = 0; i < 2; i++) {
        await loadPartial(`/Deck/PlayerNormalDraw`, "player-area");
    }
    document.getElementById("standButton").disabled = false;
    await loadPartial('/Deck/statRefresh', "statDiv");
    //await loadPartial('/Deck/computerAreaRefresh', "comp-area");
    return;
}

async function stand() {
    const data = document.getElementById("innerstat");
    const bet = parseInt(data.dataset.bet);

    if (bet < 1) {
        alert("Place a bet!");
        return;
    }

    await loadPartial(`/Deck/ComputerTurn`, "comp-area");
    await loadPartial('/Deck/statRefresh', "statDiv");


    document.getElementById("start").disabled = false;
    document.getElementById("standButton").disabled = true;
    document.getElementById("norm").disabled = true;
    document.getElementById("evil").disabled = true;
    document.getElementById("bonus").disabled = true;
    document.getElementById("betInput").disabled = true;

    window.location.assign("/Deck/GameView");

    return;
}

async function loadPartial(url, targetId) {
    const response = await fetch(url);
    const html = await response.text();
    document.getElementById(targetId).innerHTML = html;
    return;
}

async function drawCard(action) {
    const data = document.getElementById("innerstat");
    const bet = parseInt(data.dataset.bet);

    if (bet < 1) {
        alert("Place a bet!");
        return;
    }

    const response = await fetch(`/Deck/${action}`);
    document.getElementById("player-area").innerHTML = await response.text();

    await loadPartial('/Deck/statRefresh', "statDiv");
    checkForLoseScreen();

    document.getElementById("betInput").disabled = true;
    document.getElementById("betBtn").disabled = true;
    return;
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
    return;
}
function checkForLoseScreen() {
    const loseScreen = document.querySelector("#player-area #lose-screen");
    if (loseScreen) {
        window.location.assign("/Deck/GameView");
        return;
    }
    return;
}

async function placeBet() {
    const data = document.getElementById("innerstat");
    const chips = parseInt(data.dataset.chips);

    let amount = document.getElementById("betInput").value;

    if (!amount || amount <= 0 || amount > chips) {
        alert("Enter a valid bet!");
        return;
    }

    const result = await fetch(`/Deck/PlaceBet?amount=${amount}`);

    // Refresh Stats area
    await loadPartial(`/Deck/statRefresh`, "statDiv");

    document.getElementById("betInput").disabled = true;
    document.getElementById("betBtn").disabled = true;
}