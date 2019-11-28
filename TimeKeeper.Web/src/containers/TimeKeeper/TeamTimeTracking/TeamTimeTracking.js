// import React from "react";
// import axios from "axios";
// import classNames from "classnames";
// import config from "../../../config";

// import { withStyles } from "@material-ui/core/styles";
// import { ButtonGroup } from "@material-ui/core";
// import { Backdrop } from "@material-ui/core";
// import styles from "../../../styles/TableStyles";
// import Table from "@material-ui/core/Table";
// import TableBody from "@material-ui/core/TableBody";
// import TableCell from "@material-ui/core/TableCell";
// import TableHead from "@material-ui/core/TableHead";
// import TablePagination from "@material-ui/core/TablePagination";
// import TableRow from "@material-ui/core/TableRow";
// import TableSortLabel from "@material-ui/core/TableSortLabel";
// import Toolbar from "@material-ui/core/Toolbar";
// import Typography from "@material-ui/core/Typography";
// import Paper from "@material-ui/core/Paper";
// import Button from "@material-ui/core/Button";
// import CircularProgress from "@material-ui/core/CircularProgress";
// import Tooltip from "@material-ui/core/Tooltip";
// import DropDown from "./DropDownTeam";
// import DropDownMonth from "./DropDownMonth";
// import IconButton from "@material-ui/core/IconButton";
// import DropDownYear from "./DropDownYear";

// let counter = 0;
// function createData(employee, workingHours, businessAbsence, publicHoliday, vacation, sickDays, missingEntries) {
// 	counter += 1;
// 	return {
// 		id: counter,
// 		employee,
// 		workingHours,
// 		businessAbsence,
// 		publicHoliday,
// 		vacation,
// 		sickDays,
// 		missingEntries
// 	};
// }

// const desc = (a, b, orderBy) => (b[orderBy] < a[orderBy] ? -1 : b[orderBy] > a[orderBy] ? 1 : 0);

// const stableSort = (array, cmp) => {
// 	const stabilizedThis = array.map((el, index) => [el, index]);
// 	stabilizedThis.sort((a, b) => {
// 		const order = cmp(a[0], b[0]);
// 		if (order !== 0) return order;
// 		return a[1] - b[1];
// 	});
// 	return stabilizedThis.map((el) => el[0]);
// };

// const getSorting = (order, orderBy) =>
// 	order === "desc" ? (a, b) => desc(a, b, orderBy) : (a, b) => -desc(a, b, orderBy);

// const rows = [
// 	{ id: "employee", label: "Employee" },
// 	{ id: "workingHours", label: "Working hours" },
// 	{ id: "businessAbsence", label: "Business Absence" },
// 	{ id: "publicHoliday", label: "Public Holiday" },
// 	{ id: "vacation", label: "Vacation" },
// 	{ id: "sickDays", label: "Sick Days" },
// 	{ id: "missingEntries", label: "Missing entries" },
// 	{ id: "actions", label: "Actions" }
// ];

// class EnhancedTable extends React.Component {
// 	constructor(props) {
// 		super(props);
// 	}

// 	state = {
// 		loading: null,
// 		order: "asc",
// 		orderBy: "employee",
// 		selected: [],
// 		data: [],
// 		selectedTeam: null,
// 		selectedYear: null,
// 		selectedMonth: null,
// 		rowsPerPage: 6,
// 		page: 0
// 	};

// 	onClickV = (teamVal) => {
// 		this.setState({
// 			selectedTeam: teamVal
// 		});
// 	};

// 	onClickMonth = (monthVal) => {
// 		this.setState({
// 			selectedMonth: monthVal
// 		});
// 	};

// 	onClickYear = (yearVal) => {
// 		this.setState({
// 			selectedYear: yearVal
// 		});
// 	};
// 	componentDidUpdate() {
// 		/*     this.setState({ loading: true });
// 		 */

// 		if (this.state.selectedTeam != null && this.state.selectedYear != null && this.state.selectedMonth != null) {
// 			axios(
// 				`http://192.168.60.71/timekeeper/api/calendar/team-time-tracking/${this.state.selectedTeam}/${this.state.selectedYear}/${this.state.selectedMonth}`,
// 				{
// 					headers: {
// 						"Content-Type": "application/json",
// 						Authorization: "Base c2FyYWhldjokY2gwMDE="
// 					}
// 				}
// 			)
// 				.then((res) => {
// 					let fetchedData = res.data.map((r) =>
// 						createData(
// 							r.employee.name,
// 							r.hourTypes.Workday,
// 							r.hourTypes.Busines,
// 							r.hourTypes.Holiday,
// 							r.hourTypes.Vacation,
// 							r.hourTypes.Sick,
// 							r.hourTypes.Other
// 						)
// 					);
// 					this.setState({ data: fetchedData, loading: false });
// 					console.log(this.state.data);
// 				})
// 				.catch((err) => this.setState({ loading: false }));
// 		}
// 	}

// 	handleRequestSort = (property) => {
// 		const orderBy = property;
// 		let order = "desc";

// 		this.state.orderBy === property && this.state.order === "desc" ? (order = "asc") : (order = "desc");

// 		this.setState({ order, orderBy });
// 	};

// 	handleChangePage = (event, page) => this.setState({ page });

// 	isSelected = (id) => this.state.selected.indexOf(id) !== -1;

// 	render() {
// 		const { classes } = this.props;
// 		const { data, order, orderBy, rowsPerPage, page, loading } = this.state;

// 		return (
// 			<React.Fragment>
// 				{loading ? (
// 					<Backdrop open={true}>
// 						<div className={classNames(classes.center)}>
// 							<CircularProgress size={100} className={classNames(classes.loader)} />
// 							<h1 className={classNames(classes.loaderText)}>Loading...</h1>
// 						</div>
// 					</Backdrop>
// 				) : (
// 					<Paper className={classes.root}>
// 						<Toolbar className={classNames(classes.root2, {})}>
// 							<div className={classes.title}>
// 								<Typography variant="h4" id="tableTitle">
// 									Team tracking
// 								</Typography>
// 							</div>

// 							<DropDown onClickDrop={this.onClickV}></DropDown>

// 							<DropDownMonth onClickDrop={this.onClickMonth}></DropDownMonth>

// 							<DropDownYear onClickDrop={this.onClickYear}></DropDownYear>

// 							<div className={classes.spacer} />
// 							<div className={classes.actions}></div>
// 						</Toolbar>
// 						<div className={classes.tableWrapper}>
// 							<Table className={classes.table} aria-labelledby="tableTitle">
// 								<TableHead>
// 									<TableRow>
// 										{rows.map(
// 											(row) => (
// 												<TableCell
// 													className={classNames(classes.tableCell)}
// 													key={row.id}
// 													sortDirection={orderBy === row.id ? order : false}
// 												>
// 													<Tooltip
// 														title="Sort"
// 														placement={row.numeric ? "bottom-end" : "bottom-start"}
// 														enterDelay={150}
// 													>
// 														<TableSortLabel
// 															active={orderBy === row.id}
// 															direction={order}
// 															onClick={() => this.handleRequestSort(row.id)}
// 														>
// 															{row.label}
// 														</TableSortLabel>
// 													</Tooltip>
// 												</TableCell>
// 											),
// 											this
// 										)}
// 									</TableRow>
// 								</TableHead>

// 								<TableBody>
// 									{stableSort(data, getSorting(order, orderBy))
// 										.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
// 										.map((n) => {
// 											const isSelected = this.isSelected(n.id);
// 											return (
// 												<TableRow
// 													hover
// 													onClick={(event) => this.handleClick(event, n.id)}
// 													tabIndex={-1}
// 													key={n.id}
// 													selected={isSelected}
// 												>
// 													<TableCell component="th" scope="row">
// 														{n.employee}
// 													</TableCell>
// 													<TableCell>{n.workingHours}</TableCell>
// 													<TableCell>{n.businessAbsence}</TableCell>
// 													<TableCell>{n.publicHoliday}</TableCell>
// 													<TableCell>{n.vacation}</TableCell>
// 													<TableCell>{n.sickDays}</TableCell>
// 													<TableCell>{n.missingEntries}</TableCell>

// 													<TableCell align="center">
// 														{" "}
// 														<ButtonGroup>
// 															<Button variant="outlined" size="small" color="primary">
// 																View
// 															</Button>
// 															<Button variant="outlined" size="small" color="primary">
// 																Edit
// 															</Button>
// 														</ButtonGroup>
// 													</TableCell>
// 												</TableRow>
// 											);
// 										})}
// 								</TableBody>
// 							</Table>
// 						</div>
// 						<TablePagination
// 							component="div"
// 							count={data.length}
// 							rowsPerPage={rowsPerPage}
// 							page={page}
// 							backIconButtonProps={{
// 								"aria-label": "Previous Page"
// 							}}
// 							nextIconButtonProps={{
// 								"aria-label": "Next Page"
// 							}}
// 							onChangePage={this.handleChangePage}
// 							labelRowsPerPage=""
// 							rowsPerPageOptions={[]}
// 						/>
// 					</Paper>
// 				)}
// 			</React.Fragment>
// 		);
// 	}
// }

// export default withStyles(styles)(EnhancedTable);
