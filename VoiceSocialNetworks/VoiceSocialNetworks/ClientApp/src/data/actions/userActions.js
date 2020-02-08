import UserDispatcher from '../dispatchers/userDispatcher';
import constants from '../constants/userConstans';

class Actions {
    updateUser = (user) => {
        console.log(`Action update user performing, payload: ${user}`);
        UserDispatcher.dispatch({
            name: constants.UPDATE_USER,
            user
        });
    }
}

export default new Actions();