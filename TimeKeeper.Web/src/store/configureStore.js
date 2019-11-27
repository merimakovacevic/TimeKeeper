import { createStore, applyMiddleware, combineReducers } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import thunk from "redux-thunk";

import {
  userReducer,
  employeesReducer,
  customersReducer
} from "./reducers/index";
/* import { customersReducer } from "./reducers/customersReducer"; */

const rootReducer = combineReducers({
  user: userReducer,
  employees: employeesReducer,
  customers: customersReducer
});

const configureStore = () => {
  return createStore(rootReducer, composeWithDevTools(applyMiddleware(thunk)));
};

export default configureStore;
