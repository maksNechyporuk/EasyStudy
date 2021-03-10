import thunk from "redux-thunk";
import { Provider } from "react-redux";
import { composeWithDevTools } from "redux-devtools-extension";
import { createStore, combineReducers, applyMiddleware } from "redux";
import testState from "../state/testState";
let store = null;
const getStore = (useDevTool) => {
  if (store) {
    return store;
  }

  store = createStore(
    combineReducers({ testState }),
    useDevTool
      ? composeWithDevTools(applyMiddleware(thunk))
      : applyMiddleware(thunk)
  );
  return store;
};
export { getStore, Provider as ReduxProvider };
