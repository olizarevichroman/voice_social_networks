import eventer from '../../shared/js/events/eventer';
import ApiRequest from '../../shared/js/http/requests'; 
import RX from './constants';
import Actions from '../../data/actions/userActions';

eventer.onFire(RX.GET_USER, () => {
    const request = ApiRequest.GET("/api/users/getUser");

    request
    .send()
    .then((user) => {
        Actions.updateUser(user);
    })
    .catch(() => {
        console.log("error in get user request");
    });
});