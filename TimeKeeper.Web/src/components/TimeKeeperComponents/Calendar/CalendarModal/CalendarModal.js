import React, { Fragment, useState, useEffect } from "react";
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
	Select,
	FormControl
} from "@material-ui/core";
import moment from "moment";

import CalendarTask from "./CalendarTask";
import CalendarAbsent from "./CalendarAbsent";

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

const CalendarModal = (props) => {
	const [value, setValue] = useState(props.day.dayType.id ? props.day.dayType.id : value);

	const handleChange = (event) => {
		setValue(event.target.value);
		console.log(event);
	};

	useEffect(() => {
		setValue(props.day.dayType.id && props.day.dayType.id !== 11 ? props.day.dayType.id : 1);
		console.log("renderuje se");
	}, [props.day.dayType]);

	// console.log(value);

	return (
		<Fragment>
			<Container>
				<Grid container>
					<Grid item sm={12}>
						<AppBar position="static" style={{ backgroundColor: "#24292e" }}>
							<Tabs
								variant="fullWidth"
								value={props.selectedTab}
								onChange={props.handleSelectedTab}
								aria-label="Working Hours Entry"
							>
								<Tab label={`${moment(props.day.date).format("DD/MM/YYYY")}`} {...props.a11yProps(0)} />
							</Tabs>
						</AppBar>
						<Paper>
							<TabPanel>
								<FormControl>
									<CustomeSelectDayTypes value={value} onChange={handleChange} />
								</FormControl>

								{value === 1 ? (
									<Fragment>
										{props.day.jobDetails.length > 0
											? props.day.jobDetails.map((jobDetail) => (
													<CalendarTask
														key={jobDetail.id}
														day={props.day}
														data={jobDetail}
														projects={props.projects}
													/>
											  ))
											: null}
										<Divider style={{ width: "100%", margin: "1rem 0" }} />
										<CalendarTask day={props.day} projects={props.projects} />
									</Fragment>
								) : (
									<CalendarAbsent value={value} day={props.day} />
								)}
							</TabPanel>
						</Paper>
					</Grid>
				</Grid>
			</Container>
		</Fragment>
	);
};

export default CalendarModal;
