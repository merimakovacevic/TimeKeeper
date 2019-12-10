import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "./actionTypes";
import { loginUrl, apiPostRequest } from "../../utils/api";

const authStart = () => {
	return {
		type: AUTH_START
	};
};

const authSuccess = (token) => {
	return {
		type: AUTH_SUCCESS,
		token
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
		dispatch(authStart);
		apiPostRequest(loginUrl, credentials)
			.then((res) => {
				console.log(res);
				dispatch(authSuccess());
			})
			.catch((err) => dispatch(authFail(err)));
	};
};
