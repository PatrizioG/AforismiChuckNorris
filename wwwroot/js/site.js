// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

let tryItButton = document.getElementById("TryItButton");
let apiOutput = document.getElementById("ApiOutput");

let tryIt = function (e) {
    e.preventDefault();
  
    fetch('/api/aphorism/random')
        .then(response => response.json())
        .then(data => apiOutput.innerHTML = `\n"${data.value}"\n`);
}

tryItButton.addEventListener('click', tryIt);

