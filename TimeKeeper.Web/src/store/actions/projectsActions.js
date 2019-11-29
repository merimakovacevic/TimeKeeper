import { PROJECTS_FETCH_START, PROJECTS_FETCH_SUCCESS, PROJECTS_FETCH_FAIL, PROJECT_SELECTED } from "./actionTypes";

import { projectsUrl, apiGetAllRequest } from "../../utils/api";

const projectsFetchStart = () => {
	return {
		type: PROJECTS_FETCH_START
	};
};

const projectsFetchSuccess = (data) => {
	return {
		type: PROJECTS_FETCH_SUCCESS,
		data
	};
};

const projectsFetchFail = (error) => {
	return {
		type: PROJECTS_FETCH_FAIL,
		error
	};
};

export const fetchProjects = () => {
	return (dispatch) => {
		dispatch(projectsFetchStart());
		apiGetAllRequest(projectsUrl)
			.then((res) => {
				console.log(res);
				dispatch(projectsFetchSuccess(res.data.data));
			})
			.catch((err) => dispatch(projectsFetchFail(err)));
	};
};

export const projectSelect = (id) => {
	return {
		type: PROJECT_SELECTED,
		id
	};
};
