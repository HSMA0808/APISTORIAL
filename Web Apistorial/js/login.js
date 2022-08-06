  var urlAPI = document.querySelector("#APIURL")
localStorage.setItem("userId", "Admin")
localStorage.setItem("Password", "Admin2012.")
var btnLogIn = document.querySelector("#btnLogIn")

btnLogIn.addEventListener("click", ()=>{
  var txtNombreUsuario = document.querySelector("#txtNombreUsuario")
  var txtPassword = document.querySelector("#txtPassword")
  if(txtNombreUsuario.value.trim() == "" || txtPassword.value.trim() == "")
  {
    alert("Favor de completar ambos campos para inciar sesion");
  }
  else {
    localStorage.setItem("userId", txtNombreUsuario.value)
    localStorage.setItem("Password", txtPassword.value)
    if(localStorage.getItem("userId").trim() != "Admin" && localStorage.getItem("Password").trim() != "Admin2012.")
    {
      alert("Credenciales Incorrectas. Intentelo de nuevo.")
    }
    else {
      window.location = window.location.origin + "/index.html"
    }
  }
})
