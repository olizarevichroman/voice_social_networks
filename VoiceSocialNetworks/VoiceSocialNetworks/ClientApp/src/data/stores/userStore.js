import UserDispatcher from '../dispatchers/userDispatcher';
import { ReduceStore } from 'flux/utils';
import CONSTANS from '../constants/userConstans';

class UserStore extends ReduceStore {

    constructor() {
        super(UserDispatcher);
    }

    getInitialState() {
        return { isAuthenticated: false };
    }

    reduce(state, action) {
        if (action.name === CONSTANS.UPDATE_USER) {
            return action.user;
        }
        else {
            return state;
        }
    }
}

export default new UserStore();