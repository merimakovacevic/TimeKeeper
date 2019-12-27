import Axios from "axios";
import Config from "../../config";

// import { FETCH_MONTHLY_REPORT, FETCH_MONTHLY_REPORT_SUCCESS, FETCH_MONTHLY_REPORT_FAILURE } from "./actionTypes";

export const FETCH_MONTHLY_REPORT = "FETCH_MONTHLY_REPORT";
export const FETCH_MONTHLY_REPORT_SUCCESS = "FETCH_MONTHLY_REPORT_SUCCESS";
export const FETCH_MONTHLY_REPORT_FAILURE = "FETCH_MONTHLY_REPORT_FAILURE";

// import { FETCH_MONTHLY_REPORT, FETCH_MONTHLY_REPORT_SUCCESS, FETCH_MONTHLY_REPORT_FAILURE } from "./actionTypes";

export const getMonthlyReport = (selectedYear, selectedMonth) => {
	return (dispatch) => {
		dispatch({ type: FETCH_MONTHLY_REPORT });
		Axios.get(
			// "http://192.168.60.74/timekeeper/api/reports/monthly-overview/" +
			"http://api-delta.gigischool.rocks/api/reports/monthly-overview/" +
				//   "http://api-charlie.gigischool.rocks/api/reports/monthly-overview-stored/" +
				/*       awsAPI +
        "/api/reports/monthly-overview-stored/" + */
				selectedYear +
				"/" +
				selectedMonth,
			Config.authHeader
		)
			.then((res) => {
				// Map columns that will be used in table
				let data = res.data.employeeProjectHours.map((x) => {
					return {
						employee: x.employee.name,
						"total hours": x.totalHours,
						...x.hoursByProject,
						"paid time off": x.paidTimeOff
					};
				});
				// ADD TOTALS TO THE TABLE
				data.push({
					employee: "TOTAL",
					"total hours": res.data.totalHours,
					...res.data.hoursByProject,
					"paid time off": res.data.employeeProjectHours
						.map((x) => x.paidTimeOff)
						.reduce((accumulator, currentValue) => accumulator + currentValue, 0)
				});
				dispatch({
					type: FETCH_MONTHLY_REPORT_SUCCESS,
					payload: {
						data: data,
						totalDays: res.data.totalWorkingDays,
						totalHours: res.data.totalPossibleWorkingHours
					}
				});
			})
			.catch((err) => {
				console.log(err);
				dispatch({ type: FETCH_MONTHLY_REPORT_FAILURE, payload: err });
			});
	};
};

export const startLoading = () => {
	return (dispatch) => {
		dispatch({ type: FETCH_MONTHLY_REPORT });
	};
};
