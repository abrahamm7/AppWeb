//Validation for Login Page//
function RequestLogin() {
    var usertxt = document.getElementById("userbox").value;
    var passtxt = document.getElementById("passbox").value;
    if (usertxt.length == 0 || passtxt.length == 0) {
        window.alert("Empty fields");
    }
}

//Validation for Register Page//
function RequestRegister() {
    var nametxt = document.getElementById("nametxt").value;
    var passtxt = document.getElementById("passtxt").value;
    var usertxt = document.getElementById("usertxt").value;
    var lastnametxt = document.getElementById("lastnametxt").value;
    if (usertxt.length == 0 || passtxt.length == 0 || nametxt.length == 0 || lastnametxt.length == 0 ) {
        window.alert("Empty fields");
    }
}

//Validation for new category
function RequestNewCategory() {
    var categorybox = document.getElementById("categorybox").value;
    var descbox = document.getElementById("descbox").value;
    if (categorybox.length == 0 || descbox.length == 0) {
        window.alert("Empty fields");
    }
}