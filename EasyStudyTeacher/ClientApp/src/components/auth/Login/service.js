import axios from 'axios';

export default class LoginService {
    static loginUser(model) {
        return axios.post('/api/account/login', model);
    }
}