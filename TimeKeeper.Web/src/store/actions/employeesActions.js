import axios from "axios";

import { EMPLOYEES_FETCH_START, EMPLOYEES_FETCH_SUCCESS, EMPLOYEES_FETCH_FAIL, EMPLOYEE_SELECTED } from "./actionTypes";
import config from "../../config";

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
		axios(`${config.apiUrl}employees`, {
			headers: {
				"Content-Type": "application/json",
				Authorization: config.token
			}
		})
			.then((res) => {
				dispatch(employeesFetchSuccess(res.data));
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
