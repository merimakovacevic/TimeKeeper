import axios from "axios";
import { AsyncStorage } from "react-native";

import { AUTH_START, AUTH_SUCCESS, AUTH_FAIL, AUTH_LOGOUT } from "./actionTypes";
import { loginUrl, apiPostRequest } from "../../utils/api";

const setToken = async (token) => {
	console.log(token);
	try {
		await AsyncStorage.setItem("@MySuperStore:key", token);
	} catch (error) {
		// Error saving data
		console.log("error");
	}
};

export const _retrieveData = async () => {
	try {
		const value = await AsyncStorage.getItem("@MySuperStore:key");
		if (value !== null) {
			// We have data!!
			//console.log("ono sto mi treba" + value);
			return value;
		}
	} catch (error) {
		console.log("error");
	}
};

export const logout = () => {
	return {
		type: AUTH_FAIL
	};
};

export const logoutToken = async () => {
	console.log("remove");
	await AsyncStorage.removeItem("@MySuperStore:key");
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
		axios
			.post(loginUrl, credentials)
			.then((res) => {
				//console.log(res.data.token);
				dispatch(authSuccess(res.data));
				setToken(res.data.token);
				//setToken(res.data.token);
			})
			.catch((err) => dispatch(authFail(err)));
	};
};

export const isLoggedIn = () => {
	return (dispatch) => {
		dispatch(authStart());
		_retrieveData()
			.then((res) => {
				//console.log(res);
				if (res == "undefined") {
					//console.log(res);
					dispatch(authFail("error"));
				} else {
					let user = {
						token: res
					};
					dispatch(authSuccess(user));
				}
			})
			.catch((err) => console.log("error"));
	};
};
