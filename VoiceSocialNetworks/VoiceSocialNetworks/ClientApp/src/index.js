import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import ApiRequest from './shared/js/http/requests';

const data = {
    url: "http://localhost:64039/api/test/ping",
    body: {}
};

ApiRequest.POST(data)
.then(data => {
    console.log(data);
});

ReactDOM.render(<App />, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
