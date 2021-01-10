import axios from 'axios';

export default class RegisterService {
	static registerUser(model) {
		console.log("Model:", model);
		return axios.post('/api/Teacher/Register', model);
	}
}