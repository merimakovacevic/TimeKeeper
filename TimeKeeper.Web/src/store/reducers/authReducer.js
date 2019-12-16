import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "../actions/actionTypes";

const initialUserState = {
	user: null,
	token: null,
	loading: false,
	error: false
};

export const userReducer = (state = initialUserState, action) => {
	// console.log(action.type);
	switch (action.type) {
		case AUTH_START:
			return {
				...state,
				loading: true
			};
		case AUTH_SUCCESS:
			return {
				...state,
				user: action.user.user,
				token: action.user.token,
				loading: false
			};
		case AUTH_FAIL:
			return {
				...state,
				error: action.error,
				loading: false
			};
		case AUTH_LOGOUT:
			return {
				...state,
				user: null,
				token: null
			};
		default:
			return state;
	}
};
