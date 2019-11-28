import {
	EMPLOYEES_FETCH_SUCCESS,
	EMPLOYEES_FETCH_START,
	EMPLOYEES_FETCH_FAIL,
	EMPLOYEE_SELECTED
} from "../actions/actionTypes";

const initialUserState = {
	data: [],
	loading: false,
	selectedEmployee: null,
	error: null
};

export const employeesReducer = (state = initialUserState, action) => {
	switch (action.type) {
		case EMPLOYEES_FETCH_START:
			return {
				...state,
				loading: true
			};
		case EMPLOYEES_FETCH_SUCCESS:
			return {
				...state,
				data: action.data,
				loading: false
			};
		case EMPLOYEES_FETCH_FAIL:
			return {
				...state,
				error: action.error,
				loading: false
			};
		case EMPLOYEE_SELECTED:
			return {
				...state,
				selectedEmployee: action.id
			};
		default:
			return state;
	}
};
