import {
	LOAD_CALENDAR_MONTH,
	LOAD_CALENDAR_MONTH_SUCCESS,
	LOAD_CALENDAR_MONTH_FAIL,
	TASK_EDITED_SUCCESS,
	TASK_EDITED_FAIL
} from "../actions/actionTypes";

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
		case TASK_EDITED_SUCCESS:
			return {
				...state
			};
		case TASK_EDITED_FAIL:
			return {
				...state,
				error: action.error
			};
		default:
			return state;
	}
};
