import React, { Fragment, useState, useEffect } from "react";
// import Config from "../Config";
//import BlockElementSpinner from "../../components/BlockElementSpinner/BlockElementSpinner";
import { connect } from "react-redux";
import data from "./data";
import PieChart from "../Charts/PieChart";
import BarChart from "../Charts/BarChart";
import { Container, Grid, Paper, TextField, MenuItem } from "@material-ui/core";
import "./CompanyDashboard.css";

import { fetchDashboard } from "../../../store/actions/index";

import { apiGetAllRequest, companyDashboardUrl } from "../../../utils/api";
function CompanyDashboard(props) {
	const [selectedYear, setSelectedYear] = useState(2019);
	const [selectedMonth, setSelectedMonth] = useState(1);

	useEffect(() => {
		props.fetchDashboard(selectedYear, selectedMonth);
	}, [selectedMonth, selectedYear]);
	const handleSelectedYear = (e) => {
		// props.startLoading();
		setSelectedYear(e.target.value);
	};

	const handleSelectedMonth = (e) => {
		setSelectedMonth(e.target.value);
	};

	const TotalHoursChart = ({ data }) => {
		// console.log(data);
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
		return <BarChart data={modifiedData} height={350} width={350} xLabel="Projects" yLabel="Revenue" />;
	};
	const PtoChart = ({ data }) => {
		const modifiedData = data.map((x, i) => {
			return { name: x.teamName, value: parseInt(x.paidTimeOff) };
		});
		return <BarChart data={modifiedData} height={350} width={350} xLabel="Teams" yLabel="Paid Time Off" />;
	};
	const OvertimeChart = ({ data }) => {
		const modifiedData = data.map((x, i) => {
			return { name: x.teamName, value: parseInt(x.overtime) };
		});
		return <BarChart data={modifiedData} height={350} width={350} xLabel="Teams" yLabel="Overtime" />;
	};
	const MissingEntriesChart = ({ data }) => {
		const modifiedData = data.map((x) => {
			return { name: x.employeeName, value: parseInt(x.missingEntriesHours) };
		});
		return (
			<BarChart
				data={modifiedData}
				height={500}
				width={500}
				horizontal={true}
				angle={25}
				labelPadding={0}
				fontSize={8}
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
					<PieChart height={250} padAngle={1} data={modifiedData} title={x.roleName} />
				</Grid>
			);
		});
	};
	const YearDropdown = () => (
		<TextField
			variant="outlined"
			id="selected-year"
			select
			label="Selected Year"
			value={selectedYear}
			onChange={handleSelectedYear}
			margin="normal"
			fullWidth={true}
		>
			{[2019, 2018, 2017].map((x) => {
				return (
					<MenuItem value={x} key={x}>
						{x}
					</MenuItem>
				);
			})}
		</TextField>
	);

	const MonthDropdown = () => (
		<TextField
			variant="outlined"
			id="selected-month"
			select
			label="Selected Month"
			fullWidth={true}
			margin="normal"
			value={selectedMonth}
			onChange={handleSelectedMonth}
		>
			{[1, 2, 3, 4, 5, 6, 7, 8, 9].map((x) => {
				return (
					<MenuItem value={x} key={x}>
						{x}
					</MenuItem>
				);
			})}
		</TextField>
	);

	return (
		<Fragment>
			{/* {!props.annualReport.isLoading && ( */}
			{props.loading ? (
				<div>loading</div>
			) : (
				<Container maxWidth="xl">
					<Grid container alignItems="center" justify="space-between" className="mb-1-5">
						<Grid item sm={9}>
							<h3 className="mb-0 mt-0">Company Dashboard</h3>
						</Grid>
						<Grid item sm={3}>
							<div style={{ display: "flex", justifyContent: "center", alignItems: "center" }}>
								<YearDropdown />
								<MonthDropdown />
							</div>
						</Grid>
					</Grid>
					<Grid container spacing={4} justify="space-between" alignItems="center" className="mb-1-5">
						<Grid item md={4}>
							<Paper elevation={3} className="company-totals">
								<div className="flex flex-column align-items-center justify-content-center">
									<h4>TOTAL HOURS</h4> <h1 className="mb-0 mt-0">{props.data.totalHours}</h1>
								</div>
							</Paper>
						</Grid>
						<Grid item md={4}>
							<Paper elevation={3} className="company-totals">
								<div className="flex flex-column align-items-center justify-content-center">
									<h4>EMPLOYEES</h4> <h1 className="mb-0 mt-0">{props.data.employeesCount}</h1>
								</div>
							</Paper>
						</Grid>
						<Grid item md={4}>
							<Paper elevation={3} className="company-totals">
								<div className="flex flex-column align-items-center justify-content-center">
									<h4>PROJECTS </h4> <h1 className="mb-0 mt-0">{props.data.projectsCount}</h1>
								</div>
							</Paper>
						</Grid>
					</Grid>
					<Paper elevation={3} className="company-totals" className="mb-3">
						<Grid container spacing={4} justify="center" alignItems="center">
							<Grid item md={3}>
								<TotalHoursChart data={props.data} />
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
					</Paper>
					<Paper elevation={3} className="company-totals">
						<Grid container spacing={4} justify="center" alignItems="center">
							<UtilizationCharts data={props.data.roles} />
						</Grid>
					</Paper>
					<Paper>
						<MissingEntriesChart data={data.dashboard.missingEntries} />
					</Paper>
				</Container>
			)}
			{/* )} */}
			{/* {props.annualReport.isLoading && <BlockElementSpinner />} */}
		</Fragment>
	);
}

const mapStateTopProps = (state) => {
	return {
		loading: state.companyDashboard.loading,
		data: state.companyDashboard.data
	};
};

export default connect(mapStateTopProps, { fetchDashboard })(CompanyDashboard);
