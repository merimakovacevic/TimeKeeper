import {
	EMPLOYEES_FETCH_START,
	EMPLOYEES_FETCH_SUCCESS,
	EMPLOYEES_FETCH_FAIL,
	EMPLOYEE_FETCH_START,
	EMPLOYEE_FETCH_SUCCESS,
	EMPLOYEE_FETCH_FAIL,
	EMPLOYEE_SELECT,
	EMPLOYEE_CANCEL,
	EMPLOYEE_EDIT_START,
	EMPLOYEE_EDIT_FAIL,
	EMPLOYEE_EDIT_SUCCESS
} from "./actionTypes";
import { employeesUrl, apiGetAllRequest, apiGetOneRequest, apiPutRequest } from "../../utils/api";

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
		apiGetAllRequest(employeesUrl)
			.then((res) => {
				dispatch(employeesFetchSuccess(res.data.data));
			})
			.catch((err) => dispatch(employeesFetchFail(err)));
	};
};

export const employeeSelect = (id, mode) => {
	return {
		type: EMPLOYEE_SELECT,
		id,
		mode
	};
};

const employeeFetchStart = () => {
	return {
		type: EMPLOYEE_FETCH_START
	};
};

const employeeFetchFail = (error) => {
	return {
		type: EMPLOYEE_FETCH_FAIL,
		error
	};
};

const employeeFetchSuccess = (data) => {
	return {
		type: EMPLOYEE_FETCH_SUCCESS,
		data
	};
};

export const fetchEmployee = (id) => {
	return (dispatch) => {
		dispatch(employeeFetchStart());
		apiGetOneRequest(employeesUrl, id)
			.then((res) => {
				return dispatch(employeeFetchSuccess(res.data.data));
			})
			.catch((err) => dispatch(employeeFetchFail(err)));
	};
};

const employeeEditStart = () => {
	return {
		type: EMPLOYEE_EDIT_START
	};
};

const employeeEditFail = (error) => {
	return {
		type: EMPLOYEE_EDIT_FAIL,
		error
	};
};

export const employeeEditCancel = () => {
	return {
		type: EMPLOYEE_CANCEL
	};
};

const employeeEditSuccess = () => {
	return {
		type: EMPLOYEE_EDIT_SUCCESS,
		reload: 'employeeEditReload'
	};
};

export const employeePut = (id, body) => {
	return (dispatch) => {
		dispatch(employeeEditStart());
		apiPutRequest(employeesUrl, id, body)
			.then((res) => {
				dispatch(employeeEditSuccess());
				dispatch(employeeEditCancel());
			})
			.catch((err) => {
				dispatch(employeeEditFail(err));
			});
	};
};
