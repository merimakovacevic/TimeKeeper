import { AUTH_START, AUTH_FAIL, AUTH_SUCCESS } from "./actionTypes";

const authStart = () => {
	return {
		type: AUTH_START
	};
};

const authFail = (error) => {
	return {
		type: AUTH_FAIL,
		error: error
	};
};

// const authSuccess = () => {
// 	return {};
// };

export const auth = () => {
	return (dispatch) => {
		dispatch(authStart());
	};
};
