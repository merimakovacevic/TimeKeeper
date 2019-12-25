import React from "react";
import {  IconButton } from "@material-ui/core";
import { Formik, Form } from "formik";
import AddIcon from "@material-ui/icons/Add";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";

import { addDay } from "../../../../store/actions/index";

function CalendarAbsent(props) {
	// useEffect(() => {}, [props.value, props.day]);
	return (
		<div>
			<Formik
				enableReinitialize
				initialValues={{
					dayType: props.value
				}}
				onSubmit={(values) => {
					console.log("ON SUBMIT", props.value, props.user.id, props.day.date);
					const data = {
						dayType: {
							id: props.value
						},
						employee: {
							id: props.user.id
						},
						date: props.day.date
					};
					//console.log("ADD DAY DATA", data);
					props.addDay(data);
				}}
			>
				<Form>
					<IconButton color="primary" type="submit">
						<AddIcon />
					</IconButton>
				</Form>
			</Formik>
		</div>
	);
}

const mapStateToProps = (state) => {
	return {
		user: state.user.user
	};
};

export default connect(mapStateToProps, { addDay })(withRouter(CalendarAbsent));
