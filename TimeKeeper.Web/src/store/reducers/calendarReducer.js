import { LOAD_CALENDAR_MONTH, LOAD_CALENDAR_MONTH_SUCCESS, LOAD_CALENDAR_MONTH_FAIL } from "../actions/actionTypes";

const initialCalendarState = {
	data: [],
	loading: false,
	error: null
};

export const calendarReducer = (state = initialCalendarState, action) => {
	switch (action.type) {
		case LOAD_CALENDAR_MONTH:
			return {
				...state,
				loading: true
			};
		case LOAD_CALENDAR_MONTH_SUCCESS:
			return {
				...state,
				data: action.data,
				loading: false
			};
		default:
			return state;
	}
};
