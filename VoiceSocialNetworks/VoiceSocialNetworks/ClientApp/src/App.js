import './App.css';
import './shared/css/header.css';
import './rx/user/userRx';
import React, { Component } from 'react'
import Eventer from './shared/js/events/eventer';
import RX from './rx/user/constants';
import UserStore from './data/stores/userStore';
import { Container } from 'flux/utils';
import LoginForm from './shared/js/components/loginForm.jsx';

class App extends Component {
  static getStores() {
    return [UserStore];
  }

  static calculateState(prevState) {
    return {
      user: UserStore.getState()
    }
  }

  componentDidMount() {
    // Eventer.fire(RX.GET_USER);
  }

  render() {
    const { state } = this;
    const { user } = state ;
    const isUserAuthenticated = false;

    return (
      <div className="App">
        {/* {user.isAuthenticated 
            ? <div className="welcome-message">{user.name}, добро пожаловать</div>
            : false
        }  */}
        <header className="App-header">
            
        </header>
        <div className="content">
          {isUserAuthenticated
                  ? <a className="connect-link" href="http://localhost:64039/authenticate/slack">Привязать Slack</a>
                  : <a href="http://localhost:64039/authenticate/yandex">
                      <p id="pointer">
                        <span className="capitalized-letter">Я</span>
                          ндекс connect
                      </p>
                    </a> }
                  {console.log(`Header rendering with user ${user}`)}
        </div>

        {/* <LoginForm/> */}
      </div>
    )
  }
}

const container = Container.create(App);

export default container;
