import axios from "axios";

import { store } from "../../App";

export const loginUrl = "http://192.168.60.73/TimeKeeper/login";
//export const employeesUrl = "https://localhost:44350/api/employees";
export const employeesUrl = "http://192.168.60.73/TimeKeeper/api/mobile/employees";
export const customersUrl = "http://192.168.60.73/TimeKeeper/api/mobile/customers";
export const projectsUrl = "http://192.168.60.73/TimeKeeper/api/mobile/projects";
export const teamsUrl = "http://192.168.60.73/TimeKeeper/api/mobile/teams";
//export const customersUrl = "https://localhost:44350/api/customers";
//export const projectsUrl = "https://localhost:44350/api/projects";

export const getCalendarMonth = async (id, year, month) => {
	const token = store.getState().user.user.token;

	let headers = new Headers();

	headers = {
		"Content-Type": "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		headers
	};

	const result = await axios.get(
		"http://192.168.60.71/timekeeper/api/mobile/calendar" + "/" + id + "/" + year + "/" + month,
		options
	);
	if (result && result.data) {
		// console.log(result);
		return result.data;
	}
	return false;
};

export const apiGetAllRequest = (url, method = "GET") => {
	const token = store.getState().user.user.token;
	// const token =
	// 	"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6Ik1pY2hhZWwgSm9uZXMiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE1NzYxNDEwNDQsImV4cCI6MTU3Njc0NTg0NCwiaWF0IjoxNTc2MTQxMDQ0fQ.Ke5dtOENkChOiydoAINbqycfltxSHRMW7W-0pd_Y64o";
	let headers = new Headers();

	headers = {
		"Content-Type": "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios(url, options)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};

export const apiGetOneRequest = (url, id, method = "GET") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios(newUrl, options)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};

export const apiPutRequest = (url, id, body, method = "PUT") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios
		.put(newUrl, body, options)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};

export const apiPostRequest = (url, body, method = "POST") => {
	const token = store.getState().user;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios
		.post(url, body, options)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};

export const apiDeleteRequest = (url, id, method = "DELETE") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios
		.delete(newUrl, options)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};
