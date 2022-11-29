const btn = document.querySelector("button");
const eins = document.getElementById("1");
const zwei = document.getElementById("2");
const drei = document.getElementById("3");
const vier = document.getElementById("4");
const fünf = document.getElementById("5");
const sechs = document.getElementById("6");


function chngstt1() {
    eins.classList.toggle("clickstt");
}

function chngstt2() {
    zwei.classList.toggle("clickstt");
}

function chngstt3() {
    drei.classList.toggle("clickstt");
}

function chngstt4() {
    vier.classList.toggle("clickstt");
}

function chngstt5() {
    fünf.classList.toggle("clickstt");
}

function chngstt6() {
    sechs.classList.toggle("clickstt");
}

function donate() {
    window.open("thankyou.html")
}
