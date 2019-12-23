import { LOAD_CALENDAR_MONTH, LOAD_CALENDAR_MONTH_SUCCESS, LOAD_CALENDAR_MONTH_FAIL } from "../actions/actionTypes";
import { getCalendar } from "./../../utils/api";

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
