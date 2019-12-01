import { AUTH_FAIL, AUTH_SUCCESS, AUTH_START } from "./actionTypes";

export const authStart = () => {
	return {
		type: AUTH_START
	};
};

export const authFail = (error) => {
	return {
		type: AUTH_FAIL,
		error: error
	};
};

export const authSuccess = (user) => {
	return {
		type: AUTH_SUCCESS,
		user
	};
};

export const auth = () => {
	return (dispatch) => {
		// dispatch(authStart());
	};
};
