import React from "react";
import { withRouter, Route } from "react-router-dom";
import { connect } from "react-redux";

import classNames from "classnames";
import { withStyles } from "@material-ui/core/styles";
import {
	Drawer,
	AppBar,
	Toolbar,
	List,
	CssBaseline,
	Typography,
	Divider,
	IconButton,
	ListItem,
	ListItemIcon,
	ListItemText,
	Menu,
	MenuItem
} from "@material-ui/core";
import styles from "../../styles/NavigationStyles";

import ChevronLeftIcon from "@material-ui/icons/ChevronLeft";
import ChevronRightIcon from "@material-ui/icons/ChevronRight";
import StorageIcon from "@material-ui/icons/Storage";
import DescriptionIcon from "@material-ui/icons/Description";
import RestoreIcon from "@material-ui/icons/Restore";
import AppsIcon from "@material-ui/icons/Apps";
import ArrowRightIcon from "@material-ui/icons/ArrowRight";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";

import { logout } from "../../store/actions/index";
import Calendar from "../../components/TimeKeeperComponents/Calendar/Calendar";
import EmployeesPage from "../../components/TimeKeeperComponents/EmployeesPage/EmployeesPage";
import CustomersPage from "../../components/TimeKeeperComponents/CustomersPage/CustomersPage";
import ProjectsPage from "../../components/TimeKeeperComponents/ProjectsPage/ProjectsPage";
import TeamTimeTracking from "../../containers/TimeKeeper/TeamTimeTracking/TeamTimeTracking";
import CompanyDashboard from "../../components/TimeKeeperComponents/CompanyDashboardReport/CompanyDashboard";
import PersonalReport from "../../components/TimeKeeperComponents/PersonalReport/PersonalReport";
import MonthlyReport from "../../components/TimeKeeperComponents/ReportsMonthly/ReportsMonthly";
import AnnualReport from "../../components/TimeKeeperComponents/ReportsAnnual/ReportsAnnual";

class TimeKeeper extends React.Component {
	state = {
		database: [],
		reports: [],
		open: false,
		anchorDbEl: null,
		anchorSrEl: null,
		anchorUserEl: null
	};

	componentDidMount() {
		const { user } = this.props;
		if (!user) {
			return this.props.history.replace("/");
		} else {
			let role = user.role;
			if (role === "user") {
				this.setState({ database: ["Employees", "Teams"] });
				this.setState({ reports: ["Personal Report"] });
			} else if (role === "admin") {
				this.setState({ database: ["Employees", "Projects", "Customers", "Teams"] });
				this.setState({
					reports: ["Personal Report", "Annual Report", "Dashboard", "Monthly Report"]
				});
			} else {
				this.setState({ database: ["Employees", "Teams", "Projects"] });
				this.setState({
					reports: ["Personal Report", "Annual Report",  "Monthly Report"]
				});
			}
		}
	}

	handleDrawerOpen = () => this.setState({ open: true });
	handleDrawerClose = () => this.setState({ open: false });

	handleDbClick = (event) => this.setState({ anchorDbEl: event.currentTarget });
	handleSrClick = (event) => this.setState({ anchorSrEl: event.currentTarget });
	handleUserEl = (event) => this.setState({ anchorUserEl: event.currentTarget });

	handleClose = (event) => {
		this.setState({
			anchorDbEl: null,
			anchorSrEl: null,
			anchorUserEl: null
		});
		this.props.history.push(`/app/${event.currentTarget.id.toLowerCase()}`);
	};

	render() {
		const { classes, theme, user, token, logout } = this.props;

		const { open, anchorDbEl, anchorSrEl, anchorUserEl, reports, database } = this.state;
		const { handleDrawerOpen, handleDrawerClose, handleSrClick, handleDbClick, handleClose, handleUserEl } = this;

		return (
			<React.Fragment>
				{token === null ? (
					this.props.history.replace("/")
				) : (
					<div className={classes.root}>
						<CssBaseline />
						<AppBar
							position="fixed"
							className={classNames(classes.appBar, {
								[classes.appBarShift]: open
							})}
						>
							<Toolbar disableGutters={!open}>
								<IconButton
									color="inherit"
									aria-label="Open drawer"
									onClick={handleDrawerOpen}
									className={classNames(classes.hover, classes.menuButton, {
										[classes.hide]: open
									})}
								>
									<AppsIcon fontSize="large" />
								</IconButton>

								<Typography
									variant="h6"
									color="inherit"
									noWrap
									className={classes.header}
									onClick={() => this.props.history.replace("/app")}
								>
									Time Keeper
								</Typography>
								<div style={{ position: "absolute", right: 10 }}>
									<IconButton
										aria-label="account of current user"
										aria-controls="menu-appbar"
										aria-haspopup="true"
										onClick={handleUserEl}
										color="inherit"
										className={classNames(classes.hover, classes.borderRadius)}
									>
										<p style={{ fontSize: ".9rem", paddingRight: ".8rem" }}>
											{user.name} ({user.role})
										</p>
										<AccountCircleIcon fontSize="large" />
									</IconButton>
									<Menu
										id="menu-appbar"
										anchorEl={anchorUserEl}
										anchorOrigin={{
											vertical: "top",
											horizontal: "right"
										}}
										keepMounted
										transformOrigin={{
											vertical: "top",
											horizontal: "right"
										}}
										open={anchorUserEl ? true : false}
										onClose={handleClose}
										style={{ top: "40px" }}
										className={classes.menu}
									>
										<MenuItem onClick={() => this.props.history.replace("/app/personal-report")}>
											Calendar
										</MenuItem>

										<MenuItem onClick={logout}>Log Out</MenuItem>
									</Menu>
								</div>
							</Toolbar>
						</AppBar>
						<Drawer
							variant="permanent"
							className={classNames(classes.drawer, {
								[classes.drawerOpen]: open,
								[classes.drawerClose]: !open
							})}
							classes={{
								paper: classNames({
									[classes.drawerOpen]: open,
									[classes.drawerClose]: !open
								})
							}}
							open={open}
						>
							<div className={classes.toolbar}>
								<IconButton onClick={handleDrawerClose} className={classes.hover}>
									{theme.direction === "rtl" ? (
										<ChevronRightIcon />
									) : (
										<ChevronLeftIcon style={{ fill: "white" }} />
									)}
								</IconButton>
							</div>

							<List>
								<ListItem button aria-haspopup="true" onClick={handleDbClick} className={classes.hover}>
									<ListItemIcon>
										<StorageIcon style={{ fill: "white" }} />
										{!open ? <ArrowRightIcon style={{ fill: "white" }} /> : null}
									</ListItemIcon>

									<ListItemText style={{ color: "white" }}>Database</ListItemText>
									<ListItemIcon>
										<ArrowRightIcon style={{ fill: "white" }} />
									</ListItemIcon>
								</ListItem>
								<Menu
									id="simple-menu"
									onClose={handleClose}
									anchorEl={anchorDbEl}
									open={Boolean(anchorDbEl)}
									style={{ left: open ? 170 : 45 }}
									className={classes.menu}
								>
									{" "}
									{database.map((m, i) => (
										<MenuItem id={m} key={i} onClick={handleClose}>
											{m}
										</MenuItem>
									))}
								</Menu>
							</List>
							<Divider style={{ backgroundColor: "grey" }} />
							<List>
								<ListItem
									button
									aria-haspopup="true"
									onClick={() => this.props.history.push("/app/team-tracking")}
									className={classes.hover}
								>
									<ListItemIcon>
										<RestoreIcon style={{ fill: "white" }} />
									</ListItemIcon>
									<ListItemText style={{ color: "white" }}>Team Tracking</ListItemText>
								</ListItem>
							</List>
							<Divider style={{ backgroundColor: "grey" }} />
							<List>
								<ListItem button aria-haspopup="true" onClick={handleSrClick} className={classes.hover}>
									<ListItemIcon>
										<DescriptionIcon style={{ fill: "white" }} />
										{!open ? <ArrowRightIcon style={{ fill: "white" }} /> : null}
									</ListItemIcon>
									<ListItemText style={{ color: "white" }}>Reports</ListItemText>
									<ListItemIcon>
										<ArrowRightIcon style={{ fill: "white" }} />
									</ListItemIcon>
								</ListItem>
								<Menu
									id="simple-menu"
									anchorEl={anchorSrEl}
									open={Boolean(anchorSrEl)}
									onClose={handleClose}
									style={{ left: open ? 170 : 45 }}
									className={classes.menu}
								>
									{reports.map((m, i) => (
										<MenuItem id={m.replace(" ", "-")} key={i} onClick={handleClose}>
											{m}
										</MenuItem>
									))}
								</Menu>
							</List>
							<Divider style={{ backgroundColor: "grey" }} />
						</Drawer>
						<main className={classes.content}>
							{/* <div className={classes.toolbar}> */}
							<div style={{ margin: "4rem 0", display: "flex" }}>
								<Route exact={true} path="/app">
									{/* <div
										style={{
											position: "absolute",
											top: "50%",
											left: "50%",
											transform: "translate(-50%, -50%)",
											textAlign: "center"
										}}
									>
										<Typography variant="h3" gutterBottom>
											Welcome back <b>{user.name}</b>
										</Typography>
									</div> */}
								</Route>
								<Route exact path="/app/personal-report">
									<Calendar />
								</Route>
								{user.role === "admin" || user.role === "lead" ? (
									<React.Fragment>
										<Route exact={true} path="/app/employees">
											<EmployeesPage />
										</Route>
										<Route exact={true} path="/app/customers">
											<CustomersPage />
										</Route>
										<Route exact={true} path="/app/projects">
											<ProjectsPage />
										</Route>
										<Route path="/app/team-tracking">
											<TeamTimeTracking />
										</Route>
										<Route path="/app/dashboard">
											<CompanyDashboard />
										</Route>
										<Route path="/app/monthly-report">
                      <MonthlyReport/>
                    </Route>
					<Route path="/app/annual-report">
                      <AnnualReport/>
                    </Route>
									</React.Fragment>
								) : (
									<React.Fragment>
										<Route exact={true} path="/app/employees">
											<EmployeesPage />
										</Route>
										<Route exact={true} path="/app/customers">
											<CustomersPage />
										</Route>
										<Route exact={true} path="/app/projects">
											<ProjectsPage />
										</Route>
									</React.Fragment>

								)}
							</div>
						</main>
					</div>
				)}
			</React.Fragment>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		user: state.user.user,
		token: state.user.token
	};
};

export default connect(mapStateToProps, { logout })(withStyles(styles, { withTheme: true })(withRouter(TimeKeeper)));
