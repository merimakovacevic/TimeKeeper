import axios from "axios";

import { store } from "../index";

export const loginUrl = "http://localhost:8000/login";
// export const employeesUrl = "https://localhost:44321/api/employees";
export const employeesUrl = "http://api-charlie.gigischool.rocks/api/employees";
export const customersUrl = "http://api-charlie.gigischool.rocks/api/customers";
//export const customersUrl = "https://localhost:44321/api/customers";
// export const calendarUrl = "http://localhost:8000/api/calendar";
export const calendarUrl = "http://api-charlie.gigischool.rocks/api/calendar";
export const tasksUrl = "http://api-charlie.gigischool.rocks/api/tasks";
export const projectsUrl = "http://api-charlie.gigischool.rocks/api/projects";
export const dropDownTeamsUrl = "http://api-charlie.gigischool.rocks/api/teams";
export const teamTrackingUrl =
  "http://api-charlie.gigischool.rocks/api/reports/team-time-tracking";

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
