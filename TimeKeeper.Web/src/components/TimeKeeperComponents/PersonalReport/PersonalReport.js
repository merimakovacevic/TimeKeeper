import React from "react";

import PieChart from "../Charts/PieChart";

let data = {
	totalHours: 168.0,
	totalWorkingHours: 0.0
};

let yearData = {
	totalHours: 2016.0,
	totalWorkingHours: 0.0
};

const TotalHoursChart = (props) => {
	if (props.personalData) {
		// console.log(props.personalData.personalDashboardHours.workingMonthly);
		data.totalWorkingHours = props.personalData.personalDashboardHours.workingMonthly;
		yearData.totalWorkingHours = props.personalData.personalDashboardHours.workingYearly;
	}
	const modifiedData = [
		{ x: "Working Hours", y: parseInt(data.totalWorkingHours) },
		{ x: "Remaining Hours", y: parseInt(data.totalHours - data.totalWorkingHours) }
	];
	const modifiedYearData = [
		{ x: "Working Hours", y: parseInt(yearData.totalWorkingHours) },
		{ x: "Remaining Hours", y: parseInt(yearData.totalHours - data.totalWorkingHours) }
	];
	return (
		<div style={{ display: "flex" }}>
			<PieChart padAngle={1} data={modifiedData} title={"Total Hours (Month)"} />;
			<PieChart padAngle={1} data={modifiedYearData} title={"Total Hours (Year)"} />;
		</div>
	);
};

export default TotalHoursChart;
