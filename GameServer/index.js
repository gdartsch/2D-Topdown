const dgram = require('dgram');
const server = dgram.createSocket('udp4');
const Player = require('./player');
const Vector3 = require('./vector3');

/**
 * Array of players
 */
let players = [];

server.on('error', (err) => {
        console.log(`server error:\n${err.stack}`);
        server.close();
    }
);

server.on('listening', () => {
        const address = server.address();
        console.log(`server listening ${address.address}:${address.port}`);
    }
);

server.on('message', (msg, senderInfo) => {

    msg = msg.toString();

    msgs = msg.split('|');
   
    switch (msgs[0]) {
        //Connect|PlayerName|Vector3
        case "Connect":
            connectToServer(senderInfo.port, senderInfo.address, msg);
            break;
        //Update|PlayerName|Vector3
        case "Update":
            update(senderInfo.port, senderInfo.address, msg);
            break;
        //Disconnect|PlayerName
        case "Disconnect":
            disconnectPlayer(senderInfo.port, senderInfo.address, msg);
            break;
        default:
            console.log("Unknown message");
            break;
    }
});

server.on('close', (senderInfo) => {

    players.forEach((player) => {
        if (player.address == senderInfo.address && player.port == senderInfo.port) {
            players.splice(players.indexOf(player), 1);
        }
    });

    msg = `Goodbye ${Player.name}, the current connected players names are`;

    server.send(msg, senderInfo.port, senderInfo.address, (err) => {
        if (err) {
            console.log(err);
        } else {
            console.log(`message sent to ${senderInfo.address}:${senderInfo.port}`);
        }
    });
});

/**
 * Connects to the server
 * @param {*} port  port to connect to
 * @param {*} address address to connect to
 * @param {*} msg received message
 * @returns 
 */
const connectToServer = (port, address, msg) => {
    msgs = msg.split('|');

    const playerName = msgs[1];

    const playerExists = players.some(player => player.name === playerName);

    if (!playerExists) {
        const newPlayer = new Player(playerName, { x: 0, y: 0, z: 0 }, 1, address, port);
        players.push(newPlayer);
        console.log("Player added: " + newPlayer.name);

        players.forEach((player) => {
            if (player.port !== port || player.address !== address) {
                const notificationMsg = `NewPlayer|${playerName}`;
                server.send(notificationMsg, player.port, player.address, (err) => {
                    if (err) {
                        console.log(err);
                    } else {
                        console.log(`Sent new player notification to ${player.name}`);
                    }
                });
            }
        });
    } else {
        console.log("Player name already exists");
        return;
    }

    let playersInfo = players.map(player => `Spawn|${player.name}|${player.position.x},${player.position.y},${player.position.z}`).join(';');
    players.forEach((player) => {
        server.send(playersInfo, player.port, player.address, (err) => {
            if (err) {
                console.log(err);
            } else {
                console.log(`Sent player information to ${player.name}`);
            }
        });
    });
};

/**
 * Updates the player position and sends it to all other players
 * @param {*} port  port to connect to
 * @param {*} address  address to connect to
 * @param {*} msg received message
 */
const update = (port, address, msg) => {
    //msg comes in as Update-PlayerName-Vector3
    msgs = msg.split('|');

    const name = msgs[1];
    const position = msgs[2].split(',');

    const transform = new Vector3(position[0], position[1], position[2]);

    players.forEach((player) => {
        if (player.name == name) {
            player.position.x = transform.x;
            player.position.y = transform.y;
            player.position.z = transform.z;
        }
    });

    players.forEach((player) => {
        
        msg = `Update|${name}|${transform.x},${transform.y},${transform.z}`;
        server.send(msg, player.port, player.address, (err) => {
            if (err) {
                console.log(err);
            } else {
                console.log(`message sent to ${player.address}:${player.port}`);
            }
        });
    });
}

/**
 * Disconnects the player and sends a notification to all other players
 * @param {*} port  port to connect to
 * @param {*} address  address to connect to
 * @param {*} msg  received message
 */
const disconnectPlayer = (port, address, msg) => {

    console.log("Disconnecting player");
    msgs = msg.split('|');

    const name = msgs[1];

    players.forEach((player) => {
        if (player.name == name) {
            players.splice(players.indexOf(player), 1);
        }
    });

    players.forEach((player) => {
            msg = `Disconnect|${name}`;
            server.send(msg, player.port, player.address, (err) => {
                if (err) {
                    console.log(err);
                } else {
                    console.log(`message sent to ${player.address}:${player.port}`);
                }
            });
       // }
    });
}

server.bind(5500);