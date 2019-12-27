import { createStore, applyMiddleware, combineReducers } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import thunk from "redux-thunk";

// IDP Authentication //
// import { reducer as oidcReducer } from "redux-oidc";
// import createOidcMiddleware from "redux-oidc";
// import userManager from "../utils/userManager";
// const oidcMiddleware = createOidcMiddleware(userManager);

import {
	employeesReducer,
	customersReducer,
	projectsReducer,
	userReducer,
	calendarReducer,
	yearReducer,
	monthReducer,
	teamsReducer,
	teamTrackingReducer,
	personalReportReducer,
	companyDashboard,
	monthlyReport,
	annualReport
} from "./reducers/index";

const rootReducer = combineReducers({
	employees: employeesReducer,
	user: userReducer,
	customers: customersReducer,
	projects: projectsReducer,
	calendarMonth: calendarReducer,
	teams: teamsReducer,
	teamTracking: teamTrackingReducer,
	year: yearReducer,
	month: monthReducer,
	personalReportReducer,
	companyDashboard,
	annualReport: annualReport,
  monthlyReport: monthlyReport
});

const configureStore = () => {
	return createStore(rootReducer, composeWithDevTools(applyMiddleware(thunk)));
};

export default configureStore;
