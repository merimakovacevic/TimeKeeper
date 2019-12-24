import React, { Fragment } from "react";
import {
	Container,
	Grid,
	AppBar,
	Tabs,
	Tab,
	Paper,
	Divider,
	Box,
	Typography,
	IconButton,
	MenuItem,
	Select
} from "@material-ui/core";
import { Formik, Field, Form } from "formik";
import AddIcon from "@material-ui/icons/Add";

import CalendarTask from "./CalendarTask";

function TabPanel(props) {
	const { children, value, index, ...other } = props;
	return (
		<Typography
			component="div"
			role="tabpanel"
			hidden={value !== index}
			id={`tabpanel-${index}`}
			aria-labelledby={`tab-${index}`}
			{...other}
		>
			<Box p={3}>{children}</Box>
		</Typography>
	);
}

const CustomeSelectDayTypes = (props) => {
	return (
		<Select fullWidth {...props}>
			<MenuItem value={1}>Workday</MenuItem>
			<MenuItem value={2}>Holiday</MenuItem>
			<MenuItem value={3}>Busines</MenuItem>
			<MenuItem value={4}>Religious</MenuItem>
			<MenuItem value={5}>Sick</MenuItem>
			<MenuItem value={6}>Vacation</MenuItem>
			<MenuItem value={7}>Other</MenuItem>
		</Select>
	);
};

const CalendarModal = (props) => (
	<Fragment>
		<Container>
			<Grid container>
				<Grid item sm={12}>
					<AppBar position="static">
						<Tabs
							variant="fullWidth"
							value={props.selectedTab}
							onChange={props.handleSelectedTab}
							aria-label="Working Hours Entry"
						>
							<Tab label="Working Hours" {...props.a11yProps(0)} />
							<Tab label="Absent Days" {...props.a11yProps(1)} />
						</Tabs>
					</AppBar>
					<Paper>
						<TabPanel value={props.selectedTab} index={0}>
							{props.day.jobDetails.length > 0
								? props.day.jobDetails.map((x) => {
										// console.log(x);
										return <CalendarTask day={props.day} data={x} projects={props.projects} />;
								  })
								: null}
							<CalendarTask
								calendarMonth={props.calendarMonth}
								day={props.day}
								projects={props.projects}
							/>
							<Divider style={{ width: "100%", margin: "1rem 0" }} />
						</TabPanel>
					</Paper>
					<Paper elevation={4}>
						<TabPanel value={props.selectedTab} index={1}>
							<Formik
								initialValues={{
									dayType: 1
								}}
								onSubmit={() => {
									console.log("submited");
								}}
							>
								<Form>
									<Field name="dayType" as={CustomeSelectDayTypes}></Field>
									<IconButton color="primary" type="submit">
										<AddIcon />
									</IconButton>
								</Form>
							</Formik>
						</TabPanel>
					</Paper>
				</Grid>
			</Grid>
		</Container>
	</Fragment>
);

export default CalendarModal;
