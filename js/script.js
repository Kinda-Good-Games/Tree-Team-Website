const header = document.querySelector(".header-h1");
const mobile_Game = document.querySelector(".mobile-game");
const window_size = window.matchMedia("(max-width: 600px)");


header.addEventListener("click", () => {
  window.location.reload();
})

function gamemobile(window_size) {
    if (window_size.matches) {
      mobile_Game.style.display = "grid";
    } else {
        mobile_Game.style.display = "none";
    }
  }

gamemobile(window_size);
window_size.addListener(gamemobile);