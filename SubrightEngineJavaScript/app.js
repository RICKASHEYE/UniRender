const express = require('express');
const path = require('path');
const app = express();

var debug = true;

app.get('/', (req, res) => {
    res.status(200).sendFile(path.join(__dirname, 'index.html'));
});

app.post('/', (req, res, next) => {
    console.log(req);
});

app.listen(80, () => {
    console.info('Running on port 207.180.229.33:80');
});

require('dotenv').config({ path: __dirname + '/.env' })

app.use('/static', express.static(path.join(__dirname, 'public')));

app.use((err, req, res, next) => {
    switch (err.message) {
        case 'NoCodeProvided':
            return res.status(400).send({
                status: 'ERROR',
                error: err.message,
            });
        default:
            return res.status(500).send({
                status: 'ERROR',
                error: err.message,
            });
    }
});

const fs = require('fs');
const directoryPath = path.join(__dirname, 'experiments');

//passing directorypath to callback function!
app.get('/experimentgrab', (req, res) => {
    //Get the experiments!
    console.log("someone asked for experiments!");
    fs.access(directoryPath, function (err) {
        if (err) {
            console.log("Directory does not exist creating...!");
            fs.mkdirSync(directoryPath, { recursive: false });
        } else {
            console.log("Directory exists!");
        }
    });

    fs.readdir(directoryPath, function (err, files) {
        if (err) {
            return console.log("Unable to scan directory: " + err);
        }

        files.forEach(function (file) {
            console.log('grabbed: ' + file);
        });
        res.send(files);
    })
});

//The 404 Route (ALWAYS Keep this as the last route)
app.get('*', function (req, res) {
    res.status(404).sendFile(path.join(__dirname, '404.html'));
    if (debug == true) {
        res.status(200).sendFile(path.join(__dirname, 'maintence.html'));
    }
});