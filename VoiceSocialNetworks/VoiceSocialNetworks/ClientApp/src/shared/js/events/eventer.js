import { EventEmitter } from 'events';

class Eventer extends EventEmitter {
    fire = (eventName, data) => {
        console.log(`Emitting event ${eventName} with data: ${data}`);
        this.emit(eventName, data);
    }

    onFire = (eventName, callback) => {
        console.log(`Subscribing on ${eventName} event`);
        this.on(eventName, callback);
    }
}

export default new Eventer();