// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function visibilityToggle(){
    let password = document.getElementById("password");
    let img = document.getElementById("visibility-icon");
    if (password.type === "password"){
        password.type = "text";
        img.src = "/siteimgs/visibility_on.svg"
    }
    else{
        password.type = "password";
        img.src = "/siteimgs/visibility_off.svg";
    }
    return false;
}

function accountControlProfileImageContainer(){
    document.getElementById("imageInput").click();
}

function handleFiles(files){
    let file = files[0];
    //console.log("haha")
    if(file && file.type.startsWith("image")){
        if(file.type.startsWith("image")){
            console.log("haha")
        }
        let reader = new FileReader();
        reader.onload = function (event){
            document.getElementById("account-controls-profile-image").src = event.target.result;
        };
        reader.readAsDataURL(file);
    }
    else{
        alert("Incorrect File Type");
    }
}

function validateUserUpdate(){
    console.log("haaahah")
    let birthDate = (document.getElementById("BirthDate").value).padStart(2,'0')
    let birthMonth = (document.getElementById("BirthMonth").value).padStart(2,'0')
    let birthYear = (document.getElementById("BirthYear").value)
    let dateError = document.getElementById("DateError")
    let str = `${birthYear}-${birthMonth}-${birthDate}`

    document.getElementById("BirthDay").value = str;

    if (isNaN(Date.parse(str))){
        dateError.innerText = "Error in Date Format"
        return false
    }
    else{
        dateError.innerText = "";
        return true
    }
}

window.addEventListener("load", (e)=>{
    if (document.getElementById("SignUpForm")){
        let countryTag= document.getElementById("Country")
        fetch("https://api.ipify.org/?format=json").then(

            response=> response.json().then(
                json => {
                    const ip = json.ip
                }))

        fetch(`http://api.country.is/`).then(
            response => response.json().then(
                json=>{
                    countryTag.value = json.country;
                    console.log(countryTag.value)
                }))
    }
})