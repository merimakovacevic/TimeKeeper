import React, { Fragment } from "react";
import { Grid, Select, MenuItem, TextField, Input, IconButton } from "@material-ui/core";
import { Formik, Form, Field } from "formik";

import AddIcon from "@material-ui/icons/Add";
import DeleteIcon from "@material-ui/icons/Delete";
import TodayIcon from "@material-ui/icons/Today";

import moment from "moment";

const CustomSelectComponent = (props) => {
	return (
		<Select fullWidth {...props}>
			{props.data.map((p) => (
				<MenuItem value={p.id} key={p.id}>
					{p.name}
				</MenuItem>
			))}
		</Select>
	);
};

const CustomeTextFieldComponent = (props) => <TextField {...props} />;

const CustomInputComponent = (props) => <Input {...props} fullWidth />;

const CalendarTask = (props) => (
	<Formik
		initialValues={{
			hours: 0,
			projects: 1,
			description: ""
		}}
		onSubmit={(values) => {
			console.log(values);
		}}
	>
		<Form>
			<Grid container alignItems="center" className="mb-1-5">
				<span>{moment(props.calendarMonth[props.day - 1].date).format("YYYY-MM-DD")}</span>
				<TodayIcon />
			</Grid>

			<Fragment>
				<Grid container spacing={4} alignItems="center">
					<Grid item xs={3}>
						<Grid>
							<Field
								name={"projects"}
								id={"project-select-"}
								label="Project"
								data={props.projects}
								as={CustomSelectComponent}
								// value={1}
								// value={x.selectedProject}
							/>
						</Grid>
						<Grid
							item
							style={{
								padding: "1rem 0"
							}}
						>
							<Field as={CustomInputComponent} name={"hours"} placeholder="Hours Worked" />
						</Grid>
					</Grid>
					<Grid item xs={8}>
						<Field
							as={CustomeTextFieldComponent}
							name={"description"}
							label="Description"
							multiline={true}
							rows={2}
							variant="outlined"
							fullWidth
						/>
					</Grid>
					<Grid item xs={1} className="text-right">
						<Grid>
							<IconButton
								className="align-adjust-margin"
								aria-label="delete-working-hours"
								color="primary"
								type="submit"
							>
								<AddIcon />
							</IconButton>
						</Grid>
						{/* <Grid>
                    <IconButton
                        className="align-adjust-margin"
                        aria-label="delete-working-hours"
                        color="secondary"
                    >
                        <DeleteIcon />
                    </IconButton>
                </Grid> */}
					</Grid>
				</Grid>
			</Fragment>
		</Form>
	</Formik>
);

export default CalendarTask;
