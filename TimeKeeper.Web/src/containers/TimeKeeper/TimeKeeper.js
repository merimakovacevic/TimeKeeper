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
import userManager from "../../utils/userManager";

import ChevronLeftIcon from "@material-ui/icons/ChevronLeft";
import ChevronRightIcon from "@material-ui/icons/ChevronRight";
import StorageIcon from "@material-ui/icons/Storage";
import DescriptionIcon from "@material-ui/icons/Description";
import RestoreIcon from "@material-ui/icons/Restore";
import AppsIcon from "@material-ui/icons/Apps";
import ArrowRightIcon from "@material-ui/icons/ArrowRight";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import ArrowDropDownIcon from "@material-ui/icons/ArrowDropDown";

import EmployeesPage from "../../components/TimeKeeperComponents/EmployeesPage/EmployeesPage";
import CustomersPage from "./CustomersPage/CustomersPage";
import ProjectsPage from "./ProjectsPage/ProjectsPage";
import TeamTimeTracking from "./TeamTimeTracking/TeamTimeTracking";
import TeamsPage from "./TeamsPage/TeamsPage";

class TimeKeeper extends React.Component {
	state = {
		database: ["Employees", "Teams", "Customers", "Projects"],
		reports: ["Personal Report", "Monthly Report", "Annual Report", "Project History", "Dashboard"],
		open: false,
		anchorDbEl: null,
		anchorSrEl: null,
		anchorUserEl: null
	};

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

	logout = () => {
		userManager.removeUser();
	};

	render() {
		const { classes, theme, user } = this.props;
		const { open, anchorDbEl, anchorSrEl, anchorUserEl, reports, database } = this.state;
		const { handleDrawerOpen, handleDrawerClose, handleSrClick, handleDbClick, handleClose, handleUserEl } = this;

		return (
			<React.Fragment>
				{user === null ? (
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
										className={classes.hover}
									>
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
										className={classes.menu}
									>
										<MenuItem onClick={handleClose}>Calendar</MenuItem>
										<MenuItem onClick={handleClose}>My Profile</MenuItem>
										<MenuItem onClick={this.logout}>Log Out</MenuItem>
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
							<div className={classes.toolbar} />
							<Route path="/app/employees">
								<EmployeesPage />
							</Route>
							<Route path="/app/teams">
								<TeamsPage />
							</Route>
							<Route path="/app/customers">
								<CustomersPage />
							</Route>
							<Route path="/app/projects">
								<ProjectsPage />
							</Route>
							<Route path="/app/team-tracking">
								<TeamTimeTracking />
							</Route>
						</main>
					</div>
				)}
			</React.Fragment>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		user: state.user.user
	};
};

export default connect(mapStateToProps)(withStyles(styles, { withTheme: true })(withRouter(TimeKeeper)));
