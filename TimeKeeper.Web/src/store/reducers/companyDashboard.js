import { COMPANY_DASHBOARD_START, COMPANY_DASHBOARD_SUCCESS } from "../actions/actionTypes";

const initialDashboardState = {
	data: null,
	loading: true,
	error: false
};

export const companyDashboard = (state = initialDashboardState, action) => {
	// console.log(action.type);
	switch (action.type) {
		case COMPANY_DASHBOARD_START:
			return {
				...state,
				loading: true
			};
		case COMPANY_DASHBOARD_SUCCESS:
			return {
				...state,
				data: action.data,
				loading: false
			};
		default:
			return state;
	}
};
