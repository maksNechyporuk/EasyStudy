import RegisterService from './service';
import * as types from './types';
import * as captchaActions from '../../common/captcha/reducer';
import * as loginActions from '../Login/actions';

import { push } from 'connected-react-router';

export const registerActions = {
	started: () => {
		return {
			type: types.REGISTER_STARTED
		}
	},

	success: () => {
		return {
			type: types.REGISTER_SUCCESS
		}
	},

	failed: (response) => {
		return {
			type: types.REGISTER_FAILED,
			errors: response.data
		}
	}
}

export const setErrors = (errors) => {
	return (dispatch) => {
		dispatch(registerActions.setErrors(errors));
	};
}

export const registerUser = (model) => {
	return (dispatch) => {
		dispatch(registerActions.started());
		RegisterService.registerUser(model)
			.then((response) => {
				dispatch(registerActions.success());
				//login user
				loginActions.loginByJWT(response.data, dispatch);
				dispatch(push('/'));
			}, err => {
				console.log("Error:", err);
				dispatch(registerActions.failed(err.response));
				dispatch(captchaActions.createNewKey());
			})
			.catch(err => {
				console.log('Global Server problen in controler message', err);
			});
	};
}


