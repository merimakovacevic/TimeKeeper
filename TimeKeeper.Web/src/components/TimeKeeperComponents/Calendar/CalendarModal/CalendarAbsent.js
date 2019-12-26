import React from "react";
import { IconButton } from "@material-ui/core";
import { Formik, Form } from "formik";
import AddIcon from "@material-ui/icons/Add";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import axios from "axios";

import { apiDeleteRequest, calendarUrl } from "../../../../utils/api";

import { addDay, editTask, editDay } from "../../../../store/actions/index";

function CalendarAbsent(props) {
	// useEffect(() => {}, [props.value, props.day]);
	console.log(props);
	return (
		<div>
			<Formik
				enableReinitialize
				initialValues={{
					dayType: props.value
				}}
				onSubmit={(values) => {
					console.log("ON SUBMIT", props.value, props.user.id, props.day.date);
					let data = {
						dayType: {
							id: props.value
						},
						employee: {
							id: props.user.id
						},
						date: props.day.date,
						totalHours: 0
					};
					console.log("ADD DAY DATA", data);
					if (props.day.id === 0) {
						props.addDay(data);
					} else {
						data.id = props.day.id;
						console.log("DATA BEFORE EDITING DAY", data);
						props.editDay(props.day.id, data);
					}
				}}
			>
				{true ? (
					<Form>
						<IconButton color="primary" type="submit">
							<AddIcon />
						</IconButton>
					</Form>
				) : (
					<button
						onClick={() => {
							apiDeleteRequest(calendarUrl, props.day.id)
								.then((res) => console.log(res))
								.catch((err) => console.log(err));
						}}
					>
						nema vise
					</button>
				)}
			</Formik>
		</div>
	);
}

const mapStateToProps = (state) => {
	return {
		user: state.user.user
	};
};

export default connect(mapStateToProps, { addDay, editTask, editDay })(withRouter(CalendarAbsent));
