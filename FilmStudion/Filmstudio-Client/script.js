

function fetchData() {
    fetch("https://localhost:5001/api/Film/GetAllFilms").then(response =>{
        return response.json();
    }).then(data =>{
        console.log(data);
        document.getElementById("films");
        appendData(data);
    }).catch(error =>{
        console.log(error)
    })
    
}
function appendData(data) {
   
    for (var i = 0; i < data.length; i++) {
        var mainContainer = document.getElementById("films");
        var p = document.createElement("p");
        p.innerHTML = 'Movietitle: ' + data[i].name + '. Director: ' + data[i].director ;
        mainContainer.appendChild(p);
    }
}


fetchData();