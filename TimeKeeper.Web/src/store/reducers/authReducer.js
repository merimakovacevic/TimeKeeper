import { AUTH_FAIL, AUTH_SUCCESS } from "../actions/actionTypes";

const initialUserState = {
	user: null,
	error: false
};

export const userReducer = (state = initialUserState, action) => {
	switch (action.type) {
		case AUTH_SUCCESS:
			return {
				...state,
				user: action.user
			};
		case AUTH_FAIL:
			return {
				...state,
				error: action.error
			};
		default:
			return state;
	}
};
