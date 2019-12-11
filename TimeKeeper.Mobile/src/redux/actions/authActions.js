import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "./actionTypes";
import { loginUrl, apiPostRequest } from "../../utils/api";
import axios from "axios";

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
		axios
			.post(loginUrl, credentials)
			.then((res) => {
				console.log(res);
				dispatch(authSuccess(res.data));
			})
			.catch((err) => dispatch(authFail(err)));
	};
};
