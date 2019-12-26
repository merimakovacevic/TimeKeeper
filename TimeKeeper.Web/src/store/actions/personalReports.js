import { PERSONAL_REPORT_START, PERSONAL_REPORT_SUCCESS, PERSONAL_REPORT_FAIL } from "./actionTypes";

import { personalReportUrl, apiGetAllRequest } from "../../utils/api";

export const getPersonalReport = (id, year, month) => {
	let url = `${personalReportUrl}/${id}/${year}/${month}`;

	return (dispatch) => {
		dispatch({ type: PERSONAL_REPORT_START });
		// console.log(url);
		apiGetAllRequest(url)
			.then((res) => {
				// console.log(res);
				dispatch({ type: PERSONAL_REPORT_SUCCESS, data: res.data.data });
			})
			.catch((err) => {
				dispatch({ type: PERSONAL_REPORT_FAIL });
				//	console.log(err)
			});
	};
};
