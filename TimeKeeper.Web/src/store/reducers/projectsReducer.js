// import {
// 	PROJECTS_FETCH_SUCCESS,
// 	PROJECTS_FETCH_START,
// 	PROJECTS_FETCH_FAIL,
// 	PROJECT_SELECTED,
// 	PROJECT_FETCH_FAIL,
// 	PROJECT_FETCH_START,
// 	PROJECT_FETCH_SUCCESS,
// 	PROJECT_SELECT,
// 	PROJECT_CANCEL,
// 	PROJECT_EDIT_SUCCESS,
// 	PROJECT_ADD_START,
// 	PROJECT_ADD_FAIL,
// 	PROJECT_ADD_SUCCESS,
// 	PROJECT_DELETE_FAIL,
// 	PROJECT_DELETE_START,
// 	PROJECT_DELETE_SUCCESS
// } from "../actions/actionTypes";
// import {
// 	PROJECTS_FETCH_SUCCESS,
// 	PROJECTS_FETCH_START,
// 	PROJECTS_FETCH_FAIL,
// 	PROJECT_SELECTED,
// 	PROJECT_FETCH_FAIL,
// 	PROJECT_FETCH_START,
// 	PROJECT_FETCH_SUCCESS,
// 	PROJECT_SELECT,
// 	PROJECT_CANCEL,
// 	PROJECT_EDIT_SUCCESS,
// 	PROJECT_ADD_START,
// 	PROJECT_ADD_FAIL,
// 	PROJECT_ADD_SUCCESS,
// 	PROJECT_DELETE_FAIL,
// 	PROJECT_DELETE_START,
// 	PROJECT_DELETE_SUCCESS
// } from "../actions/actionTypes";

export const PROJECTS_FETCH_START = "PROJECTS_FETCH_START";
export const PROJECTS_FETCH_SUCCESS = "PROJECTS_FETCH_SUCCESS";
export const PROJECTS_FETCH_FAIL = "PROJECTS_FETCH_FAIL";
export const PROJECT_SELECTED = "PROJECT_SELECTED";
export const PROJECT_EDITED = "PROJECT_EDITED";
export const PROJECT_SELECT = "PROJECT_SELECT";
export const PROJECT_FETCH_START = "PROJECT_FETCH_START";
export const PROJECT_FETCH_SUCCESS = "PROJECT_FETCH_SUCCESS";
export const PROJECT_FETCH_FAIL = "PROJECT_FETCH_FAIL";
export const PROJECT_ADD_START = "PROJECT_ADD_START";
export const PROJECT_ADD_SUCCESS = "PROJECT_ADD_SUCCESS";
export const PROJECT_ADD_FAIL = "PROJECT_ADD_FAIL";
export const PROJECT_EDIT_START = "PROJECT_EDIT_START";
export const PROJECT_EDIT_SUCCESS = "PROJECT_EDIT_SUCCESS";
export const PROJECT_EDIT_FAIL = "PROJECT_EDIT_FAIL";
export const PROJECT_CANCEL = "PROJECT_CANCEL";
export const PROJECT_DELETE_START = "PROJECT_DELETE_START";
export const PROJECT_DELETE_SUCCESS = "PROJECT_DELETE_SUCCESS";
export const PROJECT_DELETE_FAIL = "PROJECT_DELETE_FAIL";

const initialUserState = {
	data: [],
	loading: false,
	selected: false,
	project: null,
	error: null,
	reload: false
};

export const projectsReducer = (state = initialUserState, action) => {
	switch (action.type) {
		case PROJECTS_FETCH_START:
			return {
				...state,
				loading: true
			};
		case PROJECTS_FETCH_SUCCESS:
			return {
				...state,
				data: action.data,
				loading: false
			};
		case PROJECTS_FETCH_FAIL:
			return {
				...state,
				error: action.error,
				loading: false
			};
		case PROJECT_SELECTED:
			return {
				...state,
				selected: action.id
			};

		case PROJECT_FETCH_START:
			return {
				...state
			};
		case PROJECT_FETCH_SUCCESS:
			return {
				...state,
				project: action.data
			};
		case PROJECT_FETCH_FAIL:
			return {
				...state
			};
		case PROJECT_EDIT_SUCCESS:
			return {
				...state,
				data: action.data,
				reload: action.reload
			};
		case PROJECT_SELECT:
			return {
				...state,
				selected: {
					id: action.id,
					mode: action.mode
				}
			};
		case PROJECT_ADD_START:
			return {
				...state
			};
		case PROJECT_ADD_SUCCESS:
			return {
				...state,
				data: action.data,
				reload: action.reload
			};
		case PROJECT_ADD_FAIL:
			return {
				...state
			};
		case PROJECT_DELETE_START:
			return {
				...state
			};
		case PROJECT_DELETE_SUCCESS:
			return {
				...state,
				reload: action.reload
			};
		case PROJECT_DELETE_FAIL:
			return {
				...state,
				error: action.error
			};
		case PROJECT_CANCEL:
			return {
				...state,
				project: null,
				selected: false
			};

		default:
			return state;
	}
};
