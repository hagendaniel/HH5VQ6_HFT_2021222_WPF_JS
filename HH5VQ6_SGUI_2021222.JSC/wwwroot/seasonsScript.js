let seasons = [];
let connection = null;

let seasonIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27989/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("SeasonCreated", (user, message) => {
        getdata();
    });

    connection.on("SeasonDeleted", (user, message) => {
        getdata();
    });

    connection.on("SeasonUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function getdata() {
    fetch('http://localhost:27989/seasons')
        .then(x => x.json())
        .then(y => {
            seasons = y
            display();
        });
}

function display() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('resultarea').innerHTML = '';
    seasons.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.seasonId + "</td><td>" + t.seasonNickname + "</td><td>" + t.placeId + "</td><td>" + `<button type="button" onclick="remove(${t.seasonId})">Delete</button>` + `<button type="button" onclick="showupdate(${t.seasonId})">Edit</button>` + "</td></tr>";
        console.log(t.seasonName)
    });
}

function showupdate(id) {
    document.getElementById('seasonnametoupdate').value = seasons.find(t => t['seasonId'] == id)['seasonNickname'];
    document.getElementById('updateformdiv').style.display = 'flex';
    seasonIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('seasonnametoupdate').value;
    fetch('http://localhost:27989/seasons', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { seasonId: seasonIdToUpdate, seasonNickname: name,})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('seasonname').value;
    let placeid = document.getElementById('placeid').value;
    fetch('http://localhost:27989/seasons', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { seasonNickname: name, PlaceId: placeid })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:27989/seasons/' + id, {
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