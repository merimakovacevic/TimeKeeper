import {
	LOAD_CALENDAR_MONTH,
	LOAD_CALENDAR_MONTH_SUCCESS,
	LOAD_CALENDAR_MONTH_FAIL,
	TASK_EDITED_SUCCESS,
	TASK_EDITED_FAIL
} from "../actions/actionTypes";
import { getCalendar, apiPutRequest, tasksUrl } from "./../../utils/api";

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
		apiPutRequest(tasksUrl, id, body)
			.then((res) => {
				// console.log(res);
				dispatch({ type: TASK_EDITED_SUCCESS });
			})
			.catch((err) => {
				dispatch({ TASK_EDITED_FAIL, error: err });
			});
	};
};
