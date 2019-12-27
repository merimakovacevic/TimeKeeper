import {
	LOAD_CALENDAR_MONTH,
	LOAD_CALENDAR_MONTH_SUCCESS,
	LOAD_CALENDAR_MONTH_FAIL,
	TASK_EDITED_SUCCESS,
	TASK_EDITED_FAIL,
	TASK_ADD_FAIL,
	TASK_ADD_SUCCESS,
	TASK_DELETE_SUCCESS,
	TASK_DELETE_FAIL,
	ADD_DAY_WITH_TASK_FAIL,
	ADD_DAY_WITH_TASK_SUCCESS,
	ADD_DAY_SUCCESS,
	RELOAD_CALENDAR,
	ADD_DAY_FAIL,
	DAY_EDITED_FAIL,
	DAY_EDITED_SUCCESS,
	TASK_ADD_START,
	ADD_DAY_WITH_TASK_START,
	TASK_DELETE_START,
	TASK_EDITED_START,
	ADD_DAY_START
} from "../actions/actionTypes";
import { getCalendar, apiPutRequest, tasksUrl, apiPostRequest, apiDeleteRequest, calendarUrl } from "./../../utils/api";

export const loadCalendar = (id, year, month) => {
	return async (dispatch) => {
		dispatch({ type: LOAD_CALENDAR_MONTH });
		getCalendar(id, year, month)
			.then((res) => {
				dispatch({ type: LOAD_CALENDAR_MONTH_SUCCESS, data: res.data });
				// console.log("loadCalendar res", res);
			})
			.catch((err) => {
				dispatch({ type: LOAD_CALENDAR_MONTH_FAIL });
				// console.log("err", err);
			});
	};
};

export const editTask = (id, body) => {
	return (dispatch) => {
		dispatch({ type: TASK_EDITED_START });
		apiPutRequest(tasksUrl, id, body)
			.then((res) => {
				// console.log(res);
				dispatch(rldCal(true));
				dispatch({ type: TASK_EDITED_SUCCESS });
			})
			.catch((err) => {
				dispatch({ TASK_EDITED_FAIL, error: err });
			});
	};
};

export const addTask = (body) => {
	return (dispatch) => {
		dispatch({ type: TASK_ADD_START });
		apiPostRequest(tasksUrl, body)
			.then((res) => {
				// console.log(res);
				dispatch(rldCal(true));
				dispatch({ type: TASK_ADD_SUCCESS });
			})
			.catch((err) => {
				dispatch({ type: TASK_ADD_FAIL, error: err });
			});
	};
};

export const deleteTask = (id) => {
	return (dispatch) => {
		dispatch({ type: TASK_DELETE_START });
		apiDeleteRequest(tasksUrl, id)
			.then((res) => {
				//console.log(res);
				dispatch(rldCal(true));
				dispatch({ type: TASK_DELETE_SUCCESS });
			})
			.catch((err) => {
				//console.log(err);
				dispatch({ type: TASK_DELETE_FAIL });
			});
	};
};

export const addDay = (body) => {
	return async (dispatch) => {
		dispatch({ type: LOAD_CALENDAR_MONTH });
		dispatch({ type: ADD_DAY_START });
		apiPostRequest(calendarUrl, body)
			.then((res) => {
				dispatch(rldCal(true));
				dispatch({ type: ADD_DAY_SUCCESS, data: res.data });
			})
			.catch((err) => {
				dispatch({ type: ADD_DAY_FAIL });
				// console.log("err", err);
			});
	};
};

export const addDayWithTask = (day, task) => {
	return (dispatch) => {
		dispatch({ type: ADD_DAY_WITH_TASK_START });
		apiPostRequest(calendarUrl, day)
			.then((res) => {
				dispatch(rldCal(true));
				console.log("Added day", res.data.data.id);
				dispatch({ type: ADD_DAY_SUCCESS, data: res.data });
				let data = {
					day: {
						id: res.data.data.id
					},
					project: {
						id: task.project.id
					},
					description: task.description,
					hours: task.hours
				};
				apiPostRequest(tasksUrl, data)
					.then((res) => {
						dispatch(rldCal(true));
						dispatch({ type: ADD_DAY_WITH_TASK_SUCCESS, data: res.data });
					})
					.catch((err) => {
						dispatch({ type: ADD_DAY_WITH_TASK_FAIL });
						console.log("err", err);
					});
			})
			.catch((err) => {
				dispatch({ type: ADD_DAY_WITH_TASK_FAIL });
				console.log("err", err);
			});
	};
};

export const rldCal = (value) => {
	return {
		type: RELOAD_CALENDAR,
		value
	};
};

export const editDay = (id, body) => {
	return (dispatch) => {
		apiPutRequest(calendarUrl, id, body)
			.then((res) => {
				// console.log("EDITED DAY RES", res);
				dispatch({ type: DAY_EDITED_SUCCESS });
			})
			.catch((err) => {
				dispatch({ type: DAY_EDITED_FAIL, error: err });
			});
	};
};
