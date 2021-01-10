import * as types from './types';

const initialState = {
    loading: false,
    success: false,
    failed: false,
    errors: {
        
    },
}

export const registerReducer = (state = initialState, action) => {
    let newState = state;
    switch (action.type) {
        case types.REGISTER_STARTED: {
            newState = {
                ...state, 
                loading: true,
                errors: {}
            };
            break;
        }
        case types.REGISTER_SUCCESS: {
            newState = {...state, loading: false, errors: {} };
            break;
        }
        case types.REGISTER_FAILED: {
            newState = {
                ...state, 
                loading: false, 
                errors: action.errors
            };
            break;
        }
        default: {
            return state;
        }
    }
    return newState;
}

