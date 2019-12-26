// import { FETCH_ANNUAL_REPORT, FETCH_ANNUAL_REPORT_SUCCESS, FETCH_ANNUAL_REPORT_FAILURE } from "../actions/actionTypes";

export const FETCH_ANNUAL_REPORT = "FETCH_ANNUAL_REPORT";
export const FETCH_ANNUAL_REPORT_SUCCESS = "FETCH_ANNUAL_REPORT_SUCCESS";
export const FETCH_ANNUAL_REPORT_FAILURE = "FETCH_ANNUAL_REPORT_FAILURE";
export const START_ANNUAL_REPORT_LOADER = "START_ANNUAL_REPORT_LOADER";

const initialState = {
	table: {
		head: [],
		rows: [],
		actions: false
	},
	error: null,
	isLoading: true,
	loading: false
};

export const annualReport = (state = initialState, action) => {
	switch (action.type) {
		case FETCH_ANNUAL_REPORT:
			return Object.assign({}, state, {
				isLoading: true,
				loading: true
			});
		// return state;
		case FETCH_ANNUAL_REPORT_SUCCESS:
			return Object.assign({}, state, {
				table: {
					head: Object.keys(action.payload[0]),
					rows: action.payload,
					actions: false
				},
				isLoading: false,
				loading: false
			});
		case FETCH_ANNUAL_REPORT_FAILURE:
			return Object.assign({}, state, {
				error: action.payload
			});
		default:
			return state;
	}
};
