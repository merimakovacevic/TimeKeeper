import {
	FETCH_MONTHLY_REPORT,
	FETCH_MONTHLY_REPORT_SUCCESS,
	FETCH_MONTHLY_REPORT_FAILURE
} from "../actions/actionTypes";

const initialState = {
	table: {
		head: [],
		rows: [],
		actions: false
	},
	totalDays: null,
	totalHours: null,
	error: null,
	isLoading: true
};
export const monthlyReport = (state = initialState, action) => {
	switch (action.type) {
		case FETCH_MONTHLY_REPORT:
			return Object.assign({}, state, {
				isLoading: true
			});
		// return state;
		case FETCH_MONTHLY_REPORT_SUCCESS:
			return Object.assign({}, state, {
				table: {
					head: Object.keys(action.payload.data[0]),
					rows: action.payload.data,
					actions: false
				},
				totalHours: action.payload.totalHours,
				totalDays: action.payload.totalDays,
				isLoading: false
			});
		case FETCH_MONTHLY_REPORT_FAILURE:
			return Object.assign({}, state, {
				error: action.payload
			});
		default:
			return state;
	}
};
