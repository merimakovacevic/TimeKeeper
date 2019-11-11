import React from "react";
import { withRouter, Route } from "react-router-dom";

import classNames from "classnames";
import { withStyles } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import List from "@material-ui/core/List";
import CssBaseline from "@material-ui/core/CssBaseline";
import Typography from "@material-ui/core/Typography";
import Divider from "@material-ui/core/Divider";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import ChevronLeftIcon from "@material-ui/icons/ChevronLeft";
import ChevronRightIcon from "@material-ui/icons/ChevronRight";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";

import StorageIcon from "@material-ui/icons/Storage";
import DescriptionIcon from "@material-ui/icons/Description";
import RestoreIcon from "@material-ui/icons/Restore";

import EmployeesPage from "./EmployeesPage/EmployeesPage";
import CustomersPage from "./CustomersPage/CustomersPage";
import ProjectsPage from "./ProjectsPage/ProjectsPage";

const drawerWidth = 240;

const styles = theme => ({
    root: {
        display: "flex"
    },
    appBar: {
        zIndex: theme.zIndex.drawer + 1,
        transition: theme.transitions.create(["width", "margin"], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen
        }),
        backgroundColor: "#525A65"
    },
    appBarShift: {
        marginLeft: drawerWidth,
        width: `calc(100% - ${drawerWidth}px)`,
        transition: theme.transitions.create(["width", "margin"], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen
        })
    },
    menuButton: {
        marginLeft: 12,
        marginRight: 36
    },
    hide: {
        display: "none"
    },
    drawer: {
        width: drawerWidth,
        flexShrink: 0,
        whiteSpace: "nowrap"
    },
    drawerOpen: {
        width: drawerWidth,
        transition: theme.transitions.create("width", {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen
        })
    },
    drawerClose: {
        transition: theme.transitions.create("width", {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen
        }),
        overflowX: "hidden",
        width: theme.spacing(7) + 1,
        [theme.breakpoints.up("sm")]: {
            width: theme.spacing(9) + 1
        }
    },
    toolbar: {
        display: "flex",
        alignItems: "center",
        justifyContent: "flex-end",
        padding: "0 8px",
        ...theme.mixins.toolbar
    },
    content: {
        flexGrow: 1
        // padding: theme.spacing(3)
    }
});

class TimeKeeper extends React.Component {
    state = {
        database: ["Employees", "Teams", "Customers", "Projects"],
        reports: [
            "Personal Report",
            "Monthly Report",
            "Annual Report",
            "Project History",
            "Dashboard"
        ],
        open: false,
        anchorDbEl: null,
        anchorSrEl: null,
        test: ""
    };

    handleDrawerOpen = () => {
        this.setState({ open: true });
    };

    handleDrawerClose = () => {
        this.setState({ open: false });
    };

    handleDbClick = event => {
        this.setState({ anchorDbEl: event.currentTarget });
    };
    handleSrClick = event => {
        this.setState({ anchorSrEl: event.currentTarget });
    };

    handleClose = event => {
        this.setState({
            anchorDbEl: null,
            anchorSrEl: null,
            test: event.currentTarget.id.toLowerCase()
        });
        this.props.history.push(`/app/${event.currentTarget.id.toLowerCase()}`);
    };

    render() {
        const { classes, theme } = this.props;
        const { open, anchorDbEl, anchorSrEl, reports, database } = this.state;
        const {
            handleDrawerOpen,
            handleDrawerClose,
            handleSrClick,
            handleDbClick,
            handleClose
        } = this;

        return (
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
                            className={classNames(classes.menuButton, {
                                [classes.hide]: open
                            })}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography variant="h6" color="inherit" noWrap>
                            {this.state.test + " Page"}
                        </Typography>
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
                        <IconButton onClick={handleDrawerClose}>
                            {theme.direction === "rtl" ? <ChevronRightIcon /> : <ChevronLeftIcon />}
                        </IconButton>
                    </div>
                    <Divider />
                    <List>
                        <ListItem button aria-haspopup="true" onClick={handleDbClick}>
                            <ListItemIcon>
                                <StorageIcon />
                            </ListItemIcon>
                            <ListItemText primary={"Database"} />
                        </ListItem>
                        <Menu
                            id="simple-menu"
                            anchorEl={anchorDbEl}
                            open={Boolean(anchorDbEl)}
                            style={{ left: open ? 150 : 35 }}
                        >
                            {" "}
                            {database.map((m, i) => (
                                <MenuItem id={m} key={i} onClick={handleClose}>
                                    {m}
                                </MenuItem>
                            ))}
                        </Menu>
                    </List>
                    <Divider />
                    <List>
                        <ListItem button>
                            <ListItemIcon>
                                <RestoreIcon />
                            </ListItemIcon>
                            <ListItemText primary={"Time Tracking"} />
                        </ListItem>
                    </List>
                    <Divider />
                    <List>
                        <ListItem button aria-haspopup="true" onClick={handleSrClick}>
                            <ListItemIcon>
                                <DescriptionIcon />
                            </ListItemIcon>
                            <ListItemText primary={"Reports"} />
                        </ListItem>
                        <Menu
                            id="simple-menu"
                            anchorEl={anchorSrEl}
                            open={Boolean(anchorSrEl)}
                            onClose={handleClose}
                            style={{ left: open ? 150 : 35 }}
                        >
                            {reports.map((m, i) => (
                                <MenuItem id={m.replace(" ", "-")} key={i} onClick={handleClose}>
                                    {m}
                                </MenuItem>
                            ))}
                        </Menu>
                    </List>
                </Drawer>
                <main className={classes.content}>
                    <div className={classes.toolbar} />
                    <Route path="/app/employees">
                        <EmployeesPage />
                    </Route>
                    <Route path="/app/customers">
                        <CustomersPage />
                    </Route>
                    <Route path="/app/projects">
                        <ProjectsPage />
                    </Route>
                </main>
            </div>
        );
    }
}

export default withStyles(styles, { withTheme: true })(withRouter(TimeKeeper));
