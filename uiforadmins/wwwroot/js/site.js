/*let Inputs = document.getElementsByClassName("");
let selects = document.getElementsByTagName("select");
let inputsReadonly = true;
let editButton = document.getElementById("edit");

editButton.addEventListener("click", e => {
    if (inputsReadonly) {
        for (var i = 0; i < Inputs.length; i++) {
            Inputs[i].removeAttribute("readonly")
        }
        for (var j = 0; j < selects.length; j++) {
            selects[j].style.backgroundColor = "#ffffff0a";
            selects[j].style.cursor = "pointer";
        }
        inputsReadonly = false;
    }
    else {
        for (var i = 0; i < Inputs.length; i++) {
            Inputs[i].readOnly = true;
        }
        for (var j = 0; j < selects.length; j++) {
            selects[j].style.backgroundColor = "transparent";
            selects[j].style.cursor = "pointer";
        }
        inputsReadonly = true;
    }
})
*/