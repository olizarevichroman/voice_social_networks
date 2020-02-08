function buildRequest(method, url) {
    const request = new XMLHttpRequest();
    request.responseType = "json";
    request.open(method, url, true);

    return request;
}

class ApiRequestExecutor {
    
    constructor(request) {
        this.request = request;
    }

    send = (body) => {
        return new Promise((success, fail) => {
            this.request.onload = function() {
                success(this.response);
            }
            this.request.onerror = function() {
                fail(this.response);
            }
            this.request.send(body);
        })
        .catch((error) => {
            console.log(error.message);
        });
    }
}

export default class ApiRequest {
    
    static POST(url) {
        const request = buildRequest("POST", url);

        return new ApiRequestExecutor(request);
    }

    static GET(url) {
        const request = buildRequest("GET", url);

        return new ApiRequestExecutor(request);
    }
}