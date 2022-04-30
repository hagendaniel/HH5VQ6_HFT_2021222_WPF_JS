let maps = [];
let connection = null;

let mapIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27989/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MapCreated", (user, message) => {
        getdata();
    });

    connection.on("MapDeleted", (user, message) => {
        getdata();
    });

    connection.on("MapUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function getdata() {
    fetch('http://localhost:27989/maps')
        .then(x => x.json())
        .then(y => {
            maps = y
            //console.log(maps);
            display();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';
    maps.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.mapId + "</td><td>" + t.mapName + "</td><td>" + t.difficulty + "</td><td>" + `<button type="button" onclick="remove(${t.mapId})">Delete</button>` + `<button type="button" onclick="showupdate(${t.mapId})">Edit</button>` + "</td></tr>";
        console.log(t.mapName)
    });
}

function showupdate(id) {
    //alert(id);
    document.getElementById('mapnametoupdate').value = maps.find(t => t['mapId'] == id)['mapName'];
    //document.getElementById('difficultytoupdate').value = maps.find(t => t['mapId'] == id)['difficulty'];
    document.getElementById('updateformdiv').style.display = 'flex';
    mapIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('mapnametoupdate').value;
    //let diff = document.getElementById('difficultytoupdate').value;
    fetch('http://localhost:27989/maps', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { mapId:mapIdToUpdate, mapName: name, /*difficulty: diff*/ })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('mapname').value;
    let diff = document.getElementById('difficulty').value;
    fetch('http://localhost:27989/maps', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { mapName: name, difficulty: diff })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:27989/maps/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); alert("This instance is connected to something else in the database, first you should delete them as well")});
}