import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "../actions/actionTypes";

const initialUserState = {
	user: null,
	token: null,
	loading: true,
	error: false
};

export const userReducer = (state = initialUserState, action) => {
	switch (action.type) {
		case AUTH_START:
			return {
				...state,
				loading: true
			};
		case AUTH_SUCCESS:
			return {
				...state,
				token: action.token,
				loading: false
			};
		case AUTH_FAIL:
			return {
				...state,
				error: action.error,
				loading: false
			};
		default:
			return state;
	}
};
