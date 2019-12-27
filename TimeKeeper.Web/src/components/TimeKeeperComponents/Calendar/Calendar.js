import React, { useState, useEffect } from "react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import Calendar from "react-calendar";
import moment from "moment";
import { LinearProgress, Typography, Modal, CircularProgress, Button, Backdrop } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import { withStyles } from "@material-ui/core/styles";

import "./Calendar.css";
import CalendarModal from "./CalendarModal/CalendarModal";
import { apiGetAllRequest, projectsUrl } from "../../../utils/api";
import { loadCalendar, rldCal, getPersonalReport } from "../../../store/actions";
import styles from "../../../styles/EmployeesPageStyles";
import PersonalReport from "../PersonalReport/PersonalReport";
import Wrapper from "../UI/Wrapper";

let calendarData = [
	{ id: 1, name: "Dream Client Site" },
	{ id: 2, name: "Local Business" },
	{ id: 3, name: "Improved Mobile Product" },
	{ id: 4, name: "Theme for WordPress" },
	{ id: 5, name: "Branding Package" },
	{ id: 6, name: "Icon Set" },
	{ id: 7, name: "365 Design" },
	{ id: 8, name: "Newsletter Template" },
	{ id: 9, name: "Titanic Data Set" },
	{ id: 10, name: "Loan Prediction" }
];

function CalendarDisplay(props) {
	const { classes } = props;
	const [date, setDate] = useState(new Date(2019, 5, 6, 10, 33, 30, 0));
	const [year, setYear] = useState(moment(date).format("YYYY"));
	const [month, setMonth] = useState(moment(date).format("MM"));
	const [day, setDay] = useState(moment(date).format("DD"));
	const [employeeId] = useState(props.user.user.id);
	const [projects, setProjects] = useState(calendarData);
	const [selectedTab, setSelectedTab] = useState(0);
	const [editday, setEditDay] = useState(false);

	// console.log(props.reload);
	useEffect(() => {
		props.getPersonalReport(employeeId, year, month);
		props.loadCalendar(employeeId, year, month);
	}, [month]);

	useEffect(() => {
		// apiGetAllRequest(projectsUrl).then((res) => {
		// 	console.log(res.data.data);
		// 	setProjects(res.data.data);
		// });

		props.getPersonalReport(employeeId, year, month);
		// console.log("reload");

		props.loadCalendar(employeeId, year, month);
		if (props.calendarMonth) {
			const selectedYear = moment(date).format("YYYY");
			const selectedMonth = moment(date).format("MM");
			const selectedDay = moment(date).format("DD");
			setDate(date);
			setYear(selectedYear);
			setMonth(selectedMonth);
			setDay(selectedDay);
		}
		props.rldCal(false);
	}, [props.reload]);
	const handleSelectedTab = (event, newValue) => {
		setSelectedTab(newValue);
	};
	const changeData = (selectedDate) => {
		const selectedYear = moment(selectedDate).format("YYYY");
		const selectedMonth = moment(selectedDate).format("MM");
		const selectedDay = moment(selectedDate).format("DD");
		// props.getPersonalReport(employeeId, year, month);
		if (selectedYear !== year || selectedMonth !== month) {
			// props.loadCalendar(employeeId, selectedYear, selectedMonth);
			setDate(selectedDate);
			setYear(selectedYear);
			setMonth(selectedMonth);
			setDay(selectedDay);
		} else if (selectedDay !== day) {
			setDate(selectedDate);
			setDay(selectedDay);
		}
	};
	// console.log(year169);
	// console.log(month);
	// console.log(employeeId);
	function onChange(date) {
		changeData(date);
		setEditDay(true);
	}

  function a11yProps(index) {
    return {
      id: `tab-${index}`,
      "aria-controls": `tabpanel-${index}`
    };
  }

	return (
		<React.Fragment>
			{editday ? (
				<div
					style={{
						width: "98.9vw",
						height: "98vh",
						zIndex: "15",
						position: "absolute",
						bottom: "0",
						right: "0",
						background: "rgba(0,0,0,0.4)"
					}}
				></div>
			) : null}
			<div style={{ display: "flex" }}>
				<Calendar onChange={onChange} value={date} className="react-calendar" />

				<React.Fragment>
					{props.personalDataLoader ? (
						<div style={{ width: 500, margin: "auto 5rem", textAlign: "center" }}>
							<Typography variant="h6" gutterBottom>
								Fetching personal report data...
							</Typography>
							<LinearProgress />
						</div>
					) : (
						<div
							style={{
								display: "flex",
								justifyContent: "center",
								alignItems: "center",
								flexDirection: "column"
							}}
						>
							<Typography variant="h5" gutterBottom>
								Personal Report
							</Typography>
							<PersonalReport personalData={props.personalReportData} />
						</div>
					)}
				</React.Fragment>

				<Wrapper open={editday}>
					{props.calendarMonth &&
					moment(props.calendarMonth[day - 1].date).format("YYYY-MM-DD") ===
						moment(date).format("YYYY-MM-DD") ? (
						<div
							style={{
								width: 950,
								position: "absolute",
								top: "50%",
								left: "50%",
								transform: "translate(-50%, -50%)",
								zIndex: 1000
							}}
						>
							<CalendarModal
								selectedTab={selectedTab}
								handleSelectedTab={handleSelectedTab}
								a11yProps={a11yProps}
								calendarMonth={props.calendarMonth}
								projects={projects}
								day={props.calendarMonth[day - 1]}
							/>
							<div style={{ position: "absolute", top: "5px", right: "36px" }}>
								<Button variant="contained" color="secondary" onClick={() => setEditDay(false)}>
									Back
								</Button>
							</div>
						</div>
					) : (
						<div
							style={{
								width: 500,
								position: "absolute",
								top: "50%",
								left: "50%",
								transform: "translate(-50%, -50%)",
								textAlign: "center",
								zIndex: 1500
							}}
						>
							<div className={classes.center}>
								<CircularProgress size={100} className={classes.loader} />
								<h1 className={classes.loaderText}>Fetching month data...</h1>
							</div>
						</div>
					)}
				</Wrapper>
			</div>
		</React.Fragment>
	);
}

const mapStateToProps = (state) => {
	return {
		loading: state.calendarMonth.loading,
		calendarMonth: state.calendarMonth.data.data,
		user: state.user,
		reload: state.calendarMonth.reload,
		personalReportData: state.personalReportReducer.data,
		personalDataLoader: state.personalReportReducer.loading
	};
};

export default connect(mapStateToProps, { loadCalendar, rldCal, getPersonalReport })(
	withRouter(withStyles(styles)(CalendarDisplay))
);
