const drawerWidth = 240;

const styles = (theme) => ({
	root: {
		display: "flex"
	},
	appBar: {
		zIndex: theme.zIndex.drawer + 1,
		transition: theme.transitions.create(["width", "margin"], {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.leavingScreen
		}),
		backgroundColor: "#24292e"
	},
	appBarShift: {
		backgroundColor: "#24292e",
		marginLeft: drawerWidth,
		width: `calc(100% - ${drawerWidth}px)`,
		transition: theme.transitions.create(["width", "margin"], {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.enteringScreen
		})
	},
	menu: {
		"& ul": {
			background: "#24292e"
		},
		"& li": {
			color: "white"
		},
		"& li:hover": {
			backgroundColor: "#424656 !important"
		}
	},
	menuButton: {
		marginLeft: 12,
		marginRight: 36
	},
	hide: {
		display: "none"
	},
	drawer: {
		backgroundColor: "#24292e",
		width: drawerWidth,
		flexShrink: 0,
		whiteSpace: "nowrap"
	},
	drawerOpen: {
		backgroundColor: "#24292e",
		width: drawerWidth,
		transition: theme.transitions.create("width", {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.enteringScreen
		})
	},
	drawerClose: {
		backgroundColor: "#24292e",
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
	},
	hover: {
		"&:hover": {
			backgroundColor: "#424656 !important"
		}
	},
	header: {
		padding: "1rem",
		position: "fixed",
		cursor: "pointer",
		left: "50%",
		transform: "translateX(-50%)"
		// "&:hover": {
		// 	backgroundColor: "#424656 !important"
		// }
	},
	borderRadius: {
		borderRadius: "5px !important"
	}
});

export default styles;
