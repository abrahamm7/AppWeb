function RequestLogin() {
    var usertxt = document.getElementById("userbox").value;
    var passtxt = document.getElementById("passbox").value;
    if (usertxt.length == 0 || passtxt.length == 0) {
        window.alert("Empty fields");
    }
}