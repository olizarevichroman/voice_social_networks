function getPromise(method, url, body) {
    return new Promise((success, fail) => {
        const request = new XMLHttpRequest();
        request.open(method, url, true);
        request.onload = function() {
            success(this.response);
        }
        request.onerror = function() {
            fail(this.status);
        }

        request.send(body);
    })
}

export default class ApiRequest {
    
    static POST(data) {
        const { body, url} = data;
        return getPromise("POST", url, body);
    }
}