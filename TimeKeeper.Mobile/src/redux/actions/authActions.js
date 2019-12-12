import axios from "axios";
import { AsyncStorage } from "react-native";

import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "./actionTypes";
import { loginUrl, apiPostRequest } from "../../utils/api";

// const setToken = (token) => {
// 	console.log(token);
// 	AsyncStorage.setItem("Token: ", token)
// 		.then((res) => {
// 			console.log("Token set! " + res);
// 		})
// 		.catch((err) => console.log(err));
// };

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
				//console.log(res.data.token);
				dispatch(authSuccess(res.data));
				//setToken(res.data.token);
			})
			.catch((err) => dispatch(authFail(err)));
	};
};
