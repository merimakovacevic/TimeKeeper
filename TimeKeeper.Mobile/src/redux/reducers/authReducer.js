import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "../actions/actionTypes";

const initialUserState = {
	user: null,
	loading: true,
	error: false
};

export const userReducer = (state = initialUserState, action) => {
	//console.log(action.type);
	switch (action.type) {
		case AUTH_START:
			return {
				...state,
				loading: true
			};
		case AUTH_FAIL:
			return {
				...state,
				user: {
					token: undefined
				}
			};
		case AUTH_SUCCESS:
			return {
				...state,
				user: action.user,
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
