const Vector3 = require("./vector3");

/**
 * Connection status
 * 
 */
const connectionStatus = {
    DISCONNECTED: 0,
    CONNECTED: 1,
}

/**
 * Class representing a player.
 * @param {String} name
 * @param {Vector3} position
 * @param {Number} status
 * @param {String} address
 * @param {Number} port
 * @returns {Player}
 */
class Player {
    constructor(name, position, status, address, port) {
        this.name = name;
        this.position = position;
        this.status = status;
        this.address = address;
        this.port = port;
    }
}

module.exports = Player;
