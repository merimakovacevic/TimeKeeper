import { AUTH_FAIL, AUTH_SUCCESS } from "./actionTypes";

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
