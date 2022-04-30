fetch('http://localhost:27989/maps')
    .then(x => x.json())
    .then(y => console.log(y));