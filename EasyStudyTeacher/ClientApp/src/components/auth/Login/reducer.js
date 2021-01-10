import * as types from './types';

import isEmpty from 'lodash/isEmpty';


const initialState = {
    user: {
      id: '',
      name: '',
      roles: []
    },
    isAuthenticated: false,
    loading: false,
    success: false,
    failed: false,
    errors: {
        showCaptcha:false
    },
}

export const authReducer = (state = initialState, action) => {
    let newState = state;
    switch (action.type) {
        case types.LOGIN_STARTED: {
            newState = {
                ...state, 
                loading: true,
                errors: {}
            };
            break;
        }
        case types.LOGIN_SUCCESS: {
            newState = {...state, loading: false};
            break;
        }
        case types.LOGIN_FAILED: {
            newState = {
                ...state, 
                loading: false, 
                errors: action.errors
            };
            break;
        }
        case types.LOGIN_SET_CURRENT_USER:{
            newState = {
                ...state, 
                user: action.user,
                isAuthenticated: !isEmpty(action.user),
            };
            break;
        }
        case types.LOGIN_SET_ERRORS:{
            newState = {
                ...state, 
                errors: !!!action.errors ? {} : {...state.errors, ...action.errors },
            };
            break;
        }
        default: {
            return state;
        }
    }
    return newState;
}

