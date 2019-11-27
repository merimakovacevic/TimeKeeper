import { createStore, applyMiddleware, combineReducers } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import { reducer as oidcReducer } from "redux-oidc";
import createOidcMiddleware from "redux-oidc";
import thunk from "redux-thunk";

import userManager from "../utils/userManager";
import { userReducer, employeesReducer } from "./reducers/index";

const oidcMiddleware = createOidcMiddleware(userManager);

const rootReducer = combineReducers({ employees: employeesReducer, user: oidcReducer });

const configureStore = () => {
	return createStore(rootReducer, composeWithDevTools(applyMiddleware(oidcMiddleware, thunk)));
};

export default configureStore;
