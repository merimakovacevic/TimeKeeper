import thunk from "redux-thunk";

import { createStore, applyMiddleware, combineReducers } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";

import { employeesReducer } from "./reducers/index";

const rootReducer = combineReducers({
	employees: employeesReducer
});

const configureStore = () => {
	return createStore(rootReducer, composeWithDevTools(applyMiddleware(thunk)));
};

export default configureStore;
