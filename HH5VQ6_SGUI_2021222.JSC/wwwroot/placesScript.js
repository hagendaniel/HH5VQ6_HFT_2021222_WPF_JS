let places = [];
let connection = null;

let placeIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27989/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PlaceCreated", (user, message) => {
        getdata();
    });

    connection.on("PlaceDeleted", (user, message) => {
        getdata();
    });

    connection.on("PlaceUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function getdata() {
    fetch('http://localhost:27989/places')
        .then(x => x.json())
        .then(y => {
            places = y
            //console.log(maps);
            display();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';
    places.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.placeId + "</td><td>" + t.placeName + "</td><td>" + t.country + "</td><td>" + `<button type="button" onclick="remove(${t.placeId})">Delete</button>` + `<button type="button" onclick="showupdate(${t.placeId})">Edit</button>` + "</td></tr>";
        console.log(t.placeName)
    });
}

function showupdate(id) {
    //alert(id);
    document.getElementById('placenametoupdate').value = places.find(t => t['placeId'] == id)['placeName'];
    //document.getElementById('difficultytoupdate').value = maps.find(t => t['mapId'] == id)['difficulty'];
    document.getElementById('updateformdiv').style.display = 'flex';
    placeIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('placenametoupdate').value;
    //let diff = document.getElementById('difficultytoupdate').value;
    fetch('http://localhost:27989/places', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { placeId: placeIdToUpdate, placeName: name, /*difficulty: diff*/ })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('placename').value;
    let country = document.getElementById('countryname').value;
    fetch('http://localhost:27989/places', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { placeName: name, country: country })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:27989/places/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); alert("This instance is connected to something else in the database, first you should delete them as well") });
}