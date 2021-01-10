// import update from '../../helpers/update';
import CaptchaService from './captchaService';


export const KEY_POST_STARTED = "captcha/KEY_POST_STARTED";
export const KEY_POST_SUCCESS = "captcha/KEY_POST_SUCCESS";
export const KEY_POST_FAILED = "captcha/KEY_POST_FAILED";

const initialState = {
    key: {
        data: "",
        error: false,
        loading: false,
        success: false
    },
}


export const captchaReducer = (state = initialState, action) => {
    let newState = state;

    switch (action.type) {

        case KEY_POST_STARTED: {
            newState={
                ...state,
                key: {
                    loading: true,
                    success: false,
                    data: ""
                }
            };
            // newState = update.set(state, 'key.loading', true);
            // newState = update.set(newState, 'key.success', false);
            // newState = update.set(newState, 'key.data', null);
            break;
        }

        case KEY_POST_SUCCESS: {
            newState={
                ...state,
                key: {
                    loading: false,
                    success: true,
                    data: action.payload.data
                }
            };
            // newState = update.set(state, 'key.loading', false);
            // newState = update.set(newState, 'key.success', true);
            // newState = update.set(newState, 'key.data', action.payload.data);
            break;
        }

        case KEY_POST_FAILED: {
            newState={
                ...state,
                key: {
                    loading: false,
                    data: "",
                    error: true,
                }
            };
            // newState = update.set(state, 'key.loading', false);
            // newState = update.set(newState, 'key.data', null);
            // newState = update.set(newState, 'key.error', true);
            break;
        }


        default: {
            return newState;
        }
    }

    return newState;
}

export const createNewKey = () => {
    return (dispatch) => {
        dispatch(keyCaptchaActions.started());

        CaptchaService.postNewKey()
            .then((response) => {
                dispatch(keyCaptchaActions.success(response));
            })
            .catch(() => {
                dispatch(keyCaptchaActions.failed());
            });
    }
}

export const keyCaptchaActions = {
    started: () => {
        return {
            type: KEY_POST_STARTED
        }
    },

    success: (data) => {
        return {
            type: KEY_POST_SUCCESS,
            payload: data
        }
    },

    failed: (error) => {
        return {
            type: KEY_POST_FAILED
        }
    }
}


