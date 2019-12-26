import React, { Fragment, useState, useEffect } from "react";
// import Config from "../Config";
// import BlockElementSpinner from "../../components/BlockElementSpinner/BlockElementSpinner";
// import { connect } from "react-redux";
import data from "./data";
import PieChart from "../Charts/PieChart";
import BarChart from "../Charts/BarChart";
import { Container, Grid } from "@material-ui/core";

function CompanyDashboard(props) {
	useEffect(() => {}, []);

	const TotalHoursChart = ({ data }) => {
		console.log(data);
		const modifiedData = [
			{ x: "Working Hours", y: parseInt(data.totalWorkingHours) },
			{ x: "Remaining Hours", y: parseInt(data.totalHours - data.totalWorkingHours) }
		];
		return <PieChart padAngle={1} data={modifiedData} title={"Total Hours"} />;
	};
	const RevenueChart = ({ data }) => {
		const modifiedData = data.map((x, i) => {
			return { name: x.project.name, value: parseInt(x.revenue) };
		});
		return <BarChart data={modifiedData} height={350} width={350} />;
	};
	const PtoChart = ({ data }) => {
		const modifiedData = data.map((x, i) => {
			return { name: x.teamName, value: parseInt(x.paidTimeOff) };
		});
		return <BarChart data={modifiedData} height={350} width={350} />;
	};
	const OvertimeChart = ({ data }) => {
		const modifiedData = data.map((x, i) => {
			return { name: x.teamName, value: parseInt(x.overtime) };
		});
		return <BarChart data={modifiedData} height={350} width={350} />;
	};
	const MissingEntriesChart = ({ data }) => {
		const modifiedData = data.map((x) => {
			return { name: x.employeeName, value: parseInt(x.missingEntriesHours) };
		});
		return (
			<BarChart
				data={modifiedData}
				height={250}
				width={250}
				horizontal={true}
				angle={0}
				labelPadding={0}
				fontSize={7}
			/>
		);
	};
	const UtilizationCharts = ({ data }) => {
		return data.map((x, i) => {
			const modifiedData = [
				{ x: "Working Hours", y: parseInt(x.workingHours) },
				{ x: "Remaining Hours", y: parseInt(x.totalHours - x.workingHours) }
			];
			return (
				<Grid item md={2} key={i}>
					<PieChart height={250} width={250} padAngle={1} data={modifiedData} title={x.roleName} />
				</Grid>
			);
		});
	};

	return (
		<Fragment>
			{/* {!props.annualReport.isLoading && ( */}
			<Container>
				<Grid container spacing={4}>
					<Grid item>TOTAL HOURS: {data.dashboard.totalHours}</Grid>
					<Grid item>EMPLOYEES: {data.dashboard.employeesCount}</Grid>
					<Grid item>PROJECTS: {data.dashboard.projectsCount}</Grid>
				</Grid>
				<Grid container spacing={4}>
					<Grid item md={3}>
						<TotalHoursChart data={data.dashboard} />
					</Grid>
					<Grid item md={3}>
						<RevenueChart data={data.dashboard.projects} />
					</Grid>
					<Grid item md={3}>
						<PtoChart data={data.dashboard.teams} />
					</Grid>
					<Grid item md={3}>
						<OvertimeChart data={data.dashboard.teams} />
					</Grid>
				</Grid>
				<Grid container spacing={4}>
					<Grid item md={2}>
						<MissingEntriesChart data={data.dashboard.missingEntries} />
					</Grid>
					<UtilizationCharts data={data.dashboard.roles} />
				</Grid>
			</Container>
			{/* )} */}
			{/* {props.annualReport.isLoading && <BlockElementSpinner />} */}
		</Fragment>
	);
}

// const mapStateToProps = state => {
//   return {
//     annualReport: state.annualReport
//   };
// };

// export default connect(mapStateToProps, { getAnnualReport, startLoading })(AnnualReport);
export default CompanyDashboard;
