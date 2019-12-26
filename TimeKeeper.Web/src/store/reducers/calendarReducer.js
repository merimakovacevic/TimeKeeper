import {
	LOAD_CALENDAR_MONTH,
	LOAD_CALENDAR_MONTH_SUCCESS,
	LOAD_CALENDAR_MONTH_FAIL,
	TASK_EDITED_SUCCESS,
	TASK_EDITED_FAIL,
	RELOAD_CALENDAR,
	TASK_DELETE_SUCCESS,
	ADD_DAY_SUCCESS,
	TASK_ADD_START,
	ADD_DAY_WITH_TASK_START,
	TASK_DELETE_START,
	TASK_EDITED_START,
	ADD_DAY_START,
	TASK_ADD_SUCCESS,
	ADD_DAY_WITH_TASK_SUCCESS
} from "../actions/actionTypes";

const initialCalendarState = {
	data: [],
	loading: false,
	error: null,
	reload: false,
	modalLoading: false
};

export const calendarReducer = (state = initialCalendarState, action) => {
	switch (action.type) {
		case ADD_DAY_START:
			return {
				...state,
				modalLoading: true
			};
		case TASK_EDITED_START:
			return {
				...state,
				modalLoading: true
			};
		case TASK_DELETE_START:
			return {
				...state,
				modalLoading: true
			};
		case TASK_DELETE_SUCCESS:
			return {
				...state,
				modalLoading: false
			};
		case ADD_DAY_WITH_TASK_START:
			return {
				...state,
				modalLoading: true
			};
		case ADD_DAY_WITH_TASK_SUCCESS:
			return {
				...state,
				modalLoading: false
			};
		case LOAD_CALENDAR_MONTH:
			return {
				...state,
				loading: true,
				modalLoading: true
			};
		case LOAD_CALENDAR_MONTH_SUCCESS:
			return {
				...state,
				data: action.data,
				loading: false,
				modalLoading: false
			};
		case TASK_ADD_START:
			return {
				...state,
				modalLoading: true
			};
		case TASK_ADD_SUCCESS:
			return {
				...state,
				modalLoading: false
			};
		case TASK_EDITED_SUCCESS:
			return {
				...state,
				reload: true,
				modalLoading: false
			};
		case TASK_EDITED_FAIL:
			return {
				...state,
				error: action.error
			};
		case ADD_DAY_SUCCESS:
			return {
				...state,
				reload: true,
				modalLoading: false
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
