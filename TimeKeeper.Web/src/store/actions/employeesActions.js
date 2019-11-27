import axios from "axios";
import { store } from "../../index";

import { EMPLOYEES_FETCH_START, EMPLOYEES_FETCH_SUCCESS, EMPLOYEES_FETCH_FAIL, EMPLOYEE_SELECTED } from "./actionTypes";
import { employeesUrl } from "../../utils/api";

const employeesFetchStart = () => {
	return {
		type: EMPLOYEES_FETCH_START
	};
};

const employeesFetchSuccess = (data) => {
	return {
		type: EMPLOYEES_FETCH_SUCCESS,
		data
	};
};

const employeesFetchFail = (error) => {
	return {
		type: EMPLOYEES_FETCH_FAIL,
		error
	};
};

export const fetchEmployees = () => {
	return (dispatch) => {
		dispatch(employeesFetchStart());
		apiRequest(employeesUrl)
			.then((res) => {
				console.log(res);
				dispatch(employeesFetchSuccess(res.data.data));
			})
			.catch((err) => dispatch(employeesFetchFail(err)));
	};
};

export const employeeSelect = (id) => {
	return {
		type: EMPLOYEE_SELECTED,
		id
	};
};

function loadEmployees() {
	return apiRequest(employeesUrl).then((result) => {
		console.log(result);
	});
}

// a request helper which reads the access_token from the redux state and passes it in its HTTP request
function apiRequest(url, method = "GET") {
	const token = store.getState().user.user.access_token;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios(url, options)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
}
