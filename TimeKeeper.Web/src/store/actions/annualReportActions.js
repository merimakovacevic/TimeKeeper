import Axios from "axios";
import Config from "../../../src/config";
// import {
// 	FETCH_ANNUAL_REPORT,
// 	//FETCH_ANNUAL_REPORT_SUCCESS,
// 	FETCH_ANNUAL_REPORT_FAILURE,
// 	//START_ANNUAL_REPORT_LOADER
// } from "./actionTypes";

// import {
// 	//FETCH_ANNUAL_REPORT,
// 	FETCH_ANNUAL_REPORT_SUCCESS,
// 	//FETCH_ANNUAL_REPORT_FAILURE,
// 	START_ANNUAL_REPORT_LOADER
// } from "./actionTypes";

export const FETCH_ANNUAL_REPORT = "FETCH_ANNUAL_REPORT";
export const FETCH_ANNUAL_REPORT_SUCCESS = "FETCH_ANNUAL_REPORT_SUCCESS";
export const FETCH_ANNUAL_REPORT_FAILURE = "FETCH_ANNUAL_REPORT_FAILURE";
export const START_ANNUAL_REPORT_LOADER = "START_ANNUAL_REPORT_LOADER";

export const getAnnualReport = (selectedYear) => {
	return (dispatch) => {
		console.log("in dispatch");
		dispatch({ type: FETCH_ANNUAL_REPORT });
		console.log("after dispatch fetch0");
		Axios.get(Config.apiUrl + "/reports/annual-overview-stored/" + selectedYear, Config.authHeader)
			.then((res) => {
				console.log("annual report success", res.data);
				// Map columns that will be used in table
				let data = res.data.map((x) => {
					return {
						project: x.project.name,
						total: x.total,
						jan: x.hours[0],
						feb: x.hours[1],
						mar: x.hours[2],
						apr: x.hours[3],
						may: x.hours[4],
						jun: x.hours[5],
						jul: x.hours[6],
						aug: x.hours[7],
						sep: x.hours[8],
						oct: x.hours[9],
						nov: x.hours[10],
						dec: x.hours[11]
					};
				});
				dispatch({ type: FETCH_ANNUAL_REPORT_SUCCESS, payload: data });
			})
			.catch((err) => {
				console.log(err);
				dispatch({ type: FETCH_ANNUAL_REPORT_FAILURE, payload: err });
			});
	};
};

export const startLoading = () => {
	return (dispatch) => {
		dispatch({ type: FETCH_ANNUAL_REPORT });
	};
};
