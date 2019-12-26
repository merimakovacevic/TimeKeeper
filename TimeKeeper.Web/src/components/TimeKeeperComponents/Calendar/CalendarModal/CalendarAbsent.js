import React from "react";
import { IconButton } from "@material-ui/core";
import { Formik, Form } from "formik";
import AddIcon from "@material-ui/icons/Add";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import axios from "axios";

import { apiDeleteRequest, calendarUrl } from "../../../../utils/api";

import { addDay, editTask } from "../../../../store/actions/index";

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
					// const data = {
					// 	dayType: {
					// 		id: props.value
					// 	},
					// 	employee: {
					// 		id: props.user.id
					// 	},
					// 	date: props.day.date
					// };
					//console.log("ADD DAY DATA", data);
					// props.addDay(data);
					///////////////////////////////
					let data = {
						id: 12603,
						employee: {
							id: 1,
							name: "Michael Jones"
						},
						date: "2019-06-06T00:00:00",
						dayType: {
							id: 1
						},
						totalHours: 8.0,
						jobDetails: []
					};
					console.log(data);

					props.editTask(12603, data);
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

export default connect(mapStateToProps, { addDay, editTask })(withRouter(CalendarAbsent));
