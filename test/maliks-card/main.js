const card = document.querySelector(".card");
const text = document.querySelector(".text");
const cardheader = document.querySelector("h3");
let div = document.createElement('div');
div.classList.add('gradient');

card.addEventListener("mouseover", overlol);
card.addEventListener("mouseout", outlol);

function overlol() {
    text.style.top = "240px";
    cardheader.style.borderBottom = "0px";
    card.appendChild(div)
    cardheader.style.color = "#000";
}

function outlol() {
    text.style.top = "60px";
    cardheader.style.borderBottom = "2px solid CurrentColor";
    cardheader.style.color = "#fff";
    card.removeChild(div)
}