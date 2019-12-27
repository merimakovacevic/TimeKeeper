import axios from "axios";

import { store } from "../index";

const url = "https://api-charlie.gigischool.rocks";
// const url = "http://localhost:8000";

export const loginUrl = `${url}/login`;
export const employeesUrl = `${url}/api/employees`;
export const customersUrl = `${url}/api/customers`;
export const calendarUrl = `${url}/api/calendar`;
export const tasksUrl = `${url}/api/tasks`;
export const projectsUrl = `${url}/api/projects`;
export const dropDownTeamsUrl = `${url}/api/teams`;
export const teamTrackingUrl = `${url}/api/reports/team-time-tracking`;
export const personalReportUrl = `${url}/api/dashboard/personal`;
export const companyDashboardUrl = `${url}/api/dashboard/company`;
export const awsAPI = "https://api-charlie.gigischool.rocks/";

export const login = (url, credentials) => {
	return axios
		.post(url, credentials)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};

export const getCalendar = (id, year, month) => {
  let newUrl = `${calendarUrl}/${id}/${year}/${month}`;

  const token = store.getState().user.token;

  let headers = new Headers();

  headers = {
    Accept: "application/json",
    Authorization: `Bearer ${token}`
  };

  const options = {
    headers
  };

  return axios(newUrl, options)
    .then((data) => ({ data }))
    .catch((err) => ({ err }));
};

export const apiGetAllRequest = (url, method = "GET") => {
	const token = store.getState().user.token;

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
};

export const apiGetTeamTracking = (url, team, year, month, method = "GET") => {
	let newUrl = `${url}/${team}/${year}/${month}`;

	const token = store.getState().user.token;

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

export const apiGetOneRequest = (url, id, method = "GET") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.token;
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

	const token = store.getState().user.token;
	let headers = new Headers();

  headers = {
    Accept: "application/json",
    "Content-Type": "application/json",
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
	const token = store.getState().user.token;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		"Content-Type": "application/json",
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

export const apiDeleteRequest = (url, id, method = "POST") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.token;

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
