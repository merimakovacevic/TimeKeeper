import React, { useEffect } from "react";
import { withStyles } from "@material-ui/core/styles";
import { connect } from "react-redux";
import { Formik, Field, Form } from "formik";
import * as Yup from "yup";
import moment from "moment";

import './myStyles.css'

import {
	Input,
	Dialog,
	DialogContent,
	Button,
	InputLabel,
	Select,
	MenuItem,
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableRow,
	Paper
} from "@material-ui/core";
import styles from "../../../../styles/EmployeesModalStyles";
import { fetchEmployee, employeeCancel, employeePut, employeeAdd } from "../../../../store/actions/index";

/* const positions = [
	{ id: 1, name: "Project Manager" },
	{ id: 2, name: "Team Lead" },
	{ id: 3, name: "Software Developer" },
	{ id: 4, name: "UI/UX Designer" },
	{ id: 5, name: "Quality Assurance Engineer" }
];

const statuses = [
	{ id: 1, name: "Trial" },
	{ id: 2, name: "Active" },
	{ id: 3, name: "Leaver" }
]; */

const positions = [
	{ id: 1, name: "Chief Executive Officer" },
	{ id: 2, name: "Chief Technical Officer" },
	{ id: 3, name: "Chief Operations Officer" },
	{ id: 4, name: "Manager" },
	{ id: 5, name: "HR Manager" },
	{ id: 6, name: "Developer" },
	{ id: 7, name: "UI/UX Designer" },
	{ id: 8, name: "QA Enginee" }
  ];
  const statuses = [
	{ id: 1, name: "Waiting for the task" },
	{ id: 2, name: "Active" },
	{ id: 3, name: "On hold" },
	{ id: 4, name: "Leaver" }
  ];

const test = (membersData) => {
	let index = membersData.indexOf(",");
	let team = membersData.substr(index + 1);

	return team;
};

const Schema = Yup.object().shape({
	salary: Yup.number().required("Salary can't be empty!"),
	firstName: Yup.string()
		.min(2, "First Name too short!")
		.max(32, "First Name too long!")
		.required("First Name can't be empty!"),
	lastName: Yup.string()
		.min(2, "Last Name too short!")
		.max(32, "Last Name too long!")
		.required("Last Name can't be empty!"),
	email: Yup.string().required("Email can't be empty!"),
	phone: Yup.string().required("Phone Number can't be empty!"),
	birthday: Yup.string().required("Birth Date can't be empty!"),
	employmentBeginDate: Yup.string().required("Employment Begin Date can't be empty!"),
	employmentEndDate: Yup.string(),
	position: Yup.string().required("Job Title can't be empty!"),
	status: Yup.string().required("Status can't be empty!")
});

const EmployeesModal = (props) => {
	const { classes, open, employee, id, mode } = props;
	const { fetchEmployee, employeeCancel, employeePut, employeeAdd } = props;
	let rows = [];

	if (employee) {
		let fetchedName = [];
		employee.members.forEach((r) => {
			let team = test(r.name);
			let id = r.id;
			let data = { id, team };
			fetchedName.push(data);
		});
		rows = fetchedName;
	}

	useEffect(() => {
		if (id !== null) {
			fetchEmployee(id);
		}
	}, []);

	const CustomInputComponent = (props) => (
		<Input
			disabled={mode === "view" ? true : null}
			fullWidth={true}
			className={classes.input}
			{...props}
		/>
	);

	const CustomSelectComponent = (props) => {
		return (
			<Select fullWidth {...props} className={classes.input} disabled={mode === "view" ? true : null}>
				     <MenuItem value={1}>Chief Executive Officer</MenuItem>
        <MenuItem value={2}>Chief Technical Officer</MenuItem>
        <MenuItem value={3}>Chief Operations Officer</MenuItem>
        <MenuItem value={4}>Manager</MenuItem>
        <MenuItem value={5}>HR Manager</MenuItem>
        <MenuItem value={6}>Developer</MenuItem>
        <MenuItem value={7}>UI/UX Designer</MenuItem>
        <MenuItem value={8}>QA Engineer</MenuItem>
			</Select>
		);
	};

	const CustomStatusComponent = (props) => {
		return (
			<Select fullWidth {...props} className={classes.input} disabled={mode === "view" ? true : null}>
			 <MenuItem value={1}>Waiting for the task</MenuItem>
        <MenuItem value={2}>Active</MenuItem>
        <MenuItem value={3}>On hold</MenuItem>
        <MenuItem value={4}>Leaver</MenuItem>
			</Select>
		);
	};

	return (
		<React.Fragment>
			{employee || mode === "add" ? (
				<Formik
					validationSchema={Schema}
					initialValues={{
						salary: employee ? employee.salary : "",
						firstName: employee ? employee.firstName : "",
						lastName: employee ? employee.lastName : "",
						email: employee ? employee.email : "",
						phone: employee ? employee.phone : "",
						birthday: employee ? moment(employee.birthday).format("YYYY-MM-DD") : "",
						employmentBeginDate: employee ? moment(employee.beginDate).format("YYYY-MM-DD") : "",
						employmentEndDate: employee ? moment(employee.endDate).format("YYYY-MM-DD") : "",
						position: employee ? employee.position.id : "",
						status: employee ? employee.status.id : ""
					}}
					onSubmit={(values) => {
						values.birthday = moment(values.birthday).format("YYYY-MM-DD HH:mm:ss");
						if (values.endDate !== undefined) {
							values.endDate = moment(values.endDate).format("YYYY-MM-DD HH:mm:ss");
						}

						values.beginDate = moment(values.beginDate).format("YYYY-MM-DD HH:mm:ss");

						let newPosition = positions.filter((p) => values.position === p.id);
						let newStatus = statuses.filter((s) => values.status === s.id);

						values.position = newPosition[0];
						values.status = newStatus[0];

						if (employee) {
							values.id = employee.id;
							employeePut(employee.id, values);
						} else {
							values.image = "image";
							employeeAdd(values);
						}
					}}
				>
					{({ errors, touched }) => (
						<Dialog open={open} aria-labelledby="form-dialog-title" maxWidth="lg">
							<DialogContent>
								<Form>
									<div className={classes.wrapper}>
										<div className={classes.container}>
											<img
												src="https://middle.pngfans.com/20190620/er/avatar-icon-png-computer-icons-avatar-clipart-e1c00a5950d1849e.jpg"
												alt="imasda"
												className={classes.img}
											/>
											<InputLabel>Salary</InputLabel>
											{errors.salary && touched.salary ? (
												<div className={classes.errorMessage}>{errors.salary}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field
												name="salary"
												type="number"
												autoComplete="off"
												as={CustomInputComponent}
											/>

											<Paper className={classes.root}>
												<Table className={classes.table}>
													<TableHead className={classes.tableHead}>
														<TableRow>
															<TableCell className={classes.tableCell}>Team</TableCell>
															<TableCell className={classes.tableCell} align="right">
																Role
															</TableCell>
														</TableRow>
													</TableHead>
													<TableBody>
														{rows.map((row) => (
															<TableRow key={row.id}>
																<TableCell component="th" scope="row">
																	{row.team}
																</TableCell>
																<TableCell align="right">
																	{employee.position.name}
																</TableCell>
															</TableRow>
														))}
													</TableBody>
												</Table>
											</Paper>
										</div>

										<div className={classes.container}>
											<InputLabel>First Name</InputLabel>
											{errors.firstName && touched.firstName ? (
												<div className={classes.errorMessage}>{errors.firstName}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field name="firstName" autoComplete="off" as={CustomInputComponent} />
											<InputLabel>Last Name</InputLabel>
											{errors.lastName && touched.lastName ? (
												<div className={classes.errorMessage}>{errors.lastName}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field autoComplete="off" name="lastName" as={CustomInputComponent} />
											<InputLabel>E-Mail</InputLabel>
											{errors.email && touched.email ? (
												<div className={classes.errorMessage}>{errors.email}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field
												name="email"
												type="email"
												autoComplete="off"
												as={CustomInputComponent}
											/>
											<InputLabel>Phone Number</InputLabel>
											{errors.phone && touched.phone ? (
												<div className={classes.errorMessage}>{errors.phone}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field autoComplete="off" name="phone" as={CustomInputComponent} />
											<InputLabel>Birth Date</InputLabel>
											{errors.birthday && touched.birthday ? (
												<div className={classes.errorMessage}>{errors.birthday}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field
												autoComplete="off"
												type="date"
												name="birthday"
												as={CustomInputComponent}
											/>
										</div>
										<div className={classes.container}>
											<InputLabel>Employment Begin Date</InputLabel>
											{errors.employmentBeginDate && touched.employmentBeginDate ? (
												<div className={classes.errorMessage}>{errors.employmentBeginDate}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field
												name="employmentBeginDate"
												autoComplete="off"
												type="date"
												as={CustomInputComponent}
											/>
											<InputLabel>Status</InputLabel>
											{errors.status && touched.status ? (
												<div className={classes.errorMessage}>{errors.status}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field autoComplete="off" name="status" as={CustomStatusComponent} />
											<InputLabel>Job Title</InputLabel>
											{errors.position && touched.position ? (
												<div className={classes.errorMessage}>{errors.position}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field name="position" autoComplete="off" as={CustomSelectComponent} />
											<InputLabel>Employement End Date</InputLabel>
											{errors.employementEndDate && touched.employementEndDate ? (
												<div className={classes.errorMessage}>{errors.employementEndDate}</div>
											) : (
												<div className={classes.errorMessage}> &nbsp; </div>
											)}
											<Field
												autoComplete="off"
												name="employmentEndDate"
												type="date"
												as={CustomInputComponent}
											/>
											<div className={classes.buttons}>
												{mode !== "view" ? (
													<Button variant="contained" color="primary" type="submit"
														>
														Submit
													</Button>
												) : null}
												<Button
													variant="contained"
													color={mode === "view" ? "primary" : "secondary"}
													onClick={() => employeeCancel()}
												>
													{mode === "view" ? "Back" : "Cancel"}
												</Button>
											</div>
										</div>
									</div>
								</Form>
							</DialogContent>
						</Dialog>
					)}
				</Formik>
			) : null}
		</React.Fragment>
	);
};

const mapStateToProps = (state) => {
	return {
		id: state.employees.selected.id,
		employee: state.employees.employee,
		mode: state.employees.selected.mode
	};
};

export default connect(mapStateToProps, { fetchEmployee, employeeCancel, employeePut, employeeAdd })(
	withStyles(styles)(EmployeesModal)
);