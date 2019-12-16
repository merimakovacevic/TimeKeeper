import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "./actionTypes";
import { loginUrl, login } from "../../utils/api";

export const logout = () => {
	return {
		type: AUTH_LOGOUT
	};
};

const authStart = () => {
	return {
		type: AUTH_START
	};
};

const authSuccess = (user) => {
	return {
		type: AUTH_SUCCESS,
		user
	};
};

const authFail = (error) => {
	return {
		type: AUTH_FAIL,
		error
	};
};

export const auth = (credentials) => {
	return (dispatch) => {
		dispatch(authStart());
		login(loginUrl, credentials)
			.then((res) => {
				dispatch(authSuccess(res.data.data));
			})
			.catch((err) => dispatch(authFail(err)));
	};
};
