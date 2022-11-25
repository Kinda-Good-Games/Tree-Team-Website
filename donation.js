const btn = document.querySelector("button");

btn.addEventListener("click", floattext, true);
btn.addEventListener("click", rmvtext, true)

function floattext() {
    btn.style.border = "3px solid white"
}

function rmvtext() {
    btn.style.border = "none"
}