import { COMPANY_DASHBOARD_START, COMPANY_DASHBOARD_SUCCESS } from "./actionTypes";
import { apiGetAllRequest, companyDashboardUrl } from "../../utils/api";

export const fetchDashboard = (year, month) => {
	let url = `${companyDashboardUrl}/${year}/${month}`;
	return (dispatch) => {
		dispatch({ type: COMPANY_DASHBOARD_START });
		apiGetAllRequest(url)
			.then((res) => {
				// console.log(res);
				dispatch({ type: COMPANY_DASHBOARD_SUCCESS, data: res.data.data.dashboard });
			})
			.catch((err) => console.log(err));
	};
};
