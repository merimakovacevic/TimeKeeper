import { PERSONAL_REPORT_START, PERSONAL_REPORT_SUCCESS } from "../actions/actionTypes";

const initialUserState = {
	data: null,
	loading: false
};

export const personalReportReducer = (state = initialUserState, action) => {
	// console.log(action.type);
	switch (action.type) {
		case PERSONAL_REPORT_START:
			return {
				...state,
				loading: true
			};
		case PERSONAL_REPORT_SUCCESS:
			return {
				...state,
				loading: false,
				data: action.data
			};
		default:
			return state;
	}
};
