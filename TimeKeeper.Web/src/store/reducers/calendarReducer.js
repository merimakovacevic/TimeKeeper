import {
	LOAD_CALENDAR_MONTH,
	LOAD_CALENDAR_MONTH_SUCCESS,
	LOAD_CALENDAR_MONTH_FAIL,
	TASK_EDITED_SUCCESS,
	TASK_EDITED_FAIL,
	RELOAD_CALENDAR,
	TASK_DELETE_SUCCESS,
	ADD_DAY_SUCCESS
} from "../actions/actionTypes";

const initialCalendarState = {
	data: [],
	loading: false,
	error: null,
	reload: false
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
				...state,
				reload: true
			};
		case TASK_EDITED_FAIL:
			return {
				...state,
				error: action.error
			};
		case ADD_DAY_SUCCESS:
			return {
				...state,
				reload: true
			};
		case RELOAD_CALENDAR:
			return {
				...state,
				reload: action.value
			};
		case TASK_DELETE_SUCCESS:
			return {
				...state,
				reload: true
			};
		default:
			return state;
	}
};
