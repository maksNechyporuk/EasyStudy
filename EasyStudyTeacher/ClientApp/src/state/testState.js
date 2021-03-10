import { createAction } from "redux-actions";

const prefix = "TERMINAL_INFO";
const UPDATE_POSTMATS_ADDRESS = `${prefix}/UPDATE_POSTMATS_ADDRESS`;
const UPDATE_POSTMATS_ID = `${prefix}/UPDATE_POSTAMATS_ID`;
const UPDATE_PHONE_NUMBER = `${prefix}/UPDATE_PHONE_NUMBER`;
const UPDATE_SITE = `${prefix}/UPDATE_SITE`;
const TRANSACTION_GUID = `${prefix}/TRANSACTION_GUID`;

export const updatePostmatsAddress = createAction(UPDATE_POSTMATS_ADDRESS);
export const updatePostmatsId = createAction(UPDATE_POSTMATS_ID);
export const updatePhoneNumber = createAction(UPDATE_PHONE_NUMBER);
export const updateSite = createAction(UPDATE_SITE);
export const updateTransactionGuid = createAction(TRANSACTION_GUID);

const initialState = {
  postmatsAddress: "",
  postmatsId: "",
  phoneNumber: "",
  site: "",
  transactionGuid: "",
};

export default (state = initialState, action) => {
  switch (action.type) {
    case UPDATE_POSTMATS_ADDRESS: {
      return {
        ...state,
        postmatsAddress: action.payload,
      };
    }
    case UPDATE_POSTMATS_ID: {
      return {
        ...state,
        postmatsId: action.payload,
      };
    }
    case UPDATE_PHONE_NUMBER: {
      return {
        ...state,
        phoneNumber: action.payload,
      };
    }
    case UPDATE_SITE: {
      return {
        ...state,
        site: action.payload,
      };
    }
    case TRANSACTION_GUID: {
      return {
        ...state,
        transactionGuid: action.payload,
      };
    }

    default:
      return state;
  }
};
