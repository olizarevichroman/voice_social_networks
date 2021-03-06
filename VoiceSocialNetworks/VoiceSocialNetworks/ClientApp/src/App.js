import './App.css';
import './rx/user/userRx';
import React, { Component } from 'react'
import Eventer from './shared/js/events/eventer';
import RX from './rx/user/constants';
import UserStore from './data/stores/userStore';
import { Container } from 'flux/utils';

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
    Eventer.fire(RX.GET_USER);
  }

  render() {
    const { state } = this;
    const { user } = state ;
    const isUserAuthenticated = user.isAuthenticated;

    return (
      <div className="App">
        {isUserAuthenticated 
            ? <div className="welcome-message">{user.name}, добро пожаловать</div>
            : false
        } 
        <header className="App-header">
            
        </header>
        <div className="content">
          {isUserAuthenticated
                  ? <a className="connect-link" href="/authenticate/vk">Привязать Vk</a>
                  : <a href="/authenticate/yandex">
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
