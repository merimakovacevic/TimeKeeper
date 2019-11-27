import { createStore, applyMiddleware, combineReducers } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import thunk from "redux-thunk";

import {
  userReducer,
  employeesReducer,
  customersReducer,
  projectsReducer
} from "./reducers/index";

const rootReducer = combineReducers({
  user: userReducer,
  employees: employeesReducer,
  customers: customersReducer,
  projects: projectsReducer
});

const configureStore = () => {
  return createStore(rootReducer, composeWithDevTools(applyMiddleware(thunk)));
};

export default configureStore;
