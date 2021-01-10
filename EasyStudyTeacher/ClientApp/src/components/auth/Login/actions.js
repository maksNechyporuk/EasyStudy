import LoginService from './service';
import * as types from './types';

import { push } from 'connected-react-router';
import jwt from 'jsonwebtoken';
import setAuthorisationToken from '../../../utils/setAuthorisationToken.js';

export const loginActions = {
	started: () => {
		return {
			type: types.LOGIN_STARTED
		}
	},

	success: () => {
		return {
			type: types.LOGIN_SUCCESS
		}
	},

	failed: (response) => {
		return {
			type: types.LOGIN_FAILED,
			errors: response.data
		}
	},

	setCurrentUser: (user) => {
		return {
			type: types.LOGIN_SET_CURRENT_USER,
			user
		}
	},

	setErrors: (errors) => {
		return {
			type: types.LOGIN_SET_ERRORS,
			errors
		}
	}
}

export const setErrors = (errors) => {
	return (dispatch) => {
		dispatch(loginActions.setErrors(errors));
	};
}

export const loginUser = (model) => {
	return (dispatch) => {
		dispatch(loginActions.started());
		LoginService.loginUser(model)
			.then((response) => {
				dispatch(loginActions.success());
				loginByJWT(response.data, dispatch);
				dispatch(push('/'));
			}, err => {
				dispatch(loginActions.failed(err.response));
			})
			.catch(err => {
				console.log('Global Server problen in controler message', err);
			});
	};
}

export const loginByJWT = (tokens, dispatch) => {

	const { token, refreshToken } = tokens;
	var user = jwt.decode(token);
	console.log('Hello app', user);

	if (!Array.isArray(user.roles)) {
		user.roles = Array.of(user.roles);
	}
	localStorage.setItem('authToken', token);
	localStorage.setItem('refreshToken', refreshToken);
	setAuthorisationToken(token);
	dispatch(loginActions.setCurrentUser(user));
}

export function logout() {
	return dispatch => {
		logoutByJWT(dispatch);
	};
}

export const logoutByJWT = (dispatch) => {
	localStorage.removeItem('authToken');
	localStorage.removeItem('refreshToken');
	setAuthorisationToken(false);
	dispatch(loginActions.setCurrentUser({}));
}