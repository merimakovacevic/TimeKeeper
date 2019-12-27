import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { fetchProjects, projectSelect, projectDelete } from "../../../store/actions/index";
// import { fetchProjects, projectSelect, projectDelete } from "../../../store/actions/index";
import { withStyles } from "@material-ui/core/styles";
import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableRow,
	Paper,
	Tooltip,
	IconButton,
	Button,
	CircularProgress,
	Backdrop,
	Toolbar,
	Typography,
	TextField
} from "@material-ui/core";
import styles from "../../../styles/ProjectsPageStyles";

import AddIcon from "@material-ui/icons/Add";
import VisibilityIcon from "@material-ui/icons/Visibility";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";

import ProjectsModal from "./ProjectsModal/ProjectsModal";

const ProjectsPage = (props) => {
	const { classes } = props;
	const { data, loading, error, selected, user, reload } = props;
	const { fetchProjects, projectSelect, projectDelete } = props;
	const [searchProjects, setSearchedEmp] = useState([]);
	const [search, setSearch] = useState("");

	let projects = data;

	useEffect(() => {
		fetchProjects();
		setSearch("");
		projects = data;
	}, [reload]);

	const handleSearchInput = (event) => {
		setSearch(event.target.value);
		searchMessages();
	};

	const searchMessages = () => {
		const regex = new RegExp(search, "gi");
		const searchResults = projects.reduce((accumulator, event) => {
			if (
				(event.customerName && event.customerName.match(regex)) ||
				(event.projectName && event.projectName.match(regex))
			) {
				accumulator.push(event);
			}
			return accumulator;
		}, []);

		setSearchedEmp(searchResults);
	};

	const AllProjects = (projects) =>
		projects.map((e, i) => (
			<TableRow key={e.id}>
				<CustomTableCell>{i + 1}</CustomTableCell>
				<CustomTableCell>{e.name}</CustomTableCell>
				<CustomTableCell>{e.customer.name}</CustomTableCell>
				<CustomTableCell>{e.team.name}</CustomTableCell>
				<CustomTableCell>{e.status.name}</CustomTableCell>

				{user.role === "admin" ? (
					<CustomTableCell align="center">
						<IconButton
							aria-label="Edit"
							className={classes.editButton}
							onClick={() => projectSelect(e.id, "edit")}
						>
							<EditIcon style={{ fill: "green" }} />
						</IconButton>
						<IconButton
							aria-label="Delete"
							className={classes.deleteButton}
							onClick={() => projectDelete(e.id)}
						>
							<DeleteIcon color="error" />
						</IconButton>
					</CustomTableCell>
				) : (
					<CustomTableCell align="center">
						<IconButton aria-label="View" onClick={() => projectSelect(e.id, "view")}>
							<VisibilityIcon />
						</IconButton>
					</CustomTableCell>
				)}
			</TableRow>
		));

	return (
		<React.Fragment>
			{loading ? (
				<Backdrop open={loading}>
					<div className={classes.center}>
						<CircularProgress size={100} className={classes.loader} />
						<h1 className={classes.loaderText}>Loading...</h1>
					</div>
				</Backdrop>
			) : error ? (
				<Backdrop open={true}>
					<div className={classes.center}>
						<h1 className={classes.loaderText}>{error.message}</h1>
						<h2 className={classes.loaderText}>Please reload the application</h2>
						<Button variant="outlined" size="large" className={classes.loaderText}>
							Reload
						</Button>
					</div>
				</Backdrop>
			) : (
				<Paper className={classes.root}>
					{selected ? <ProjectsModal selected={selected} open={true} /> : null}
					<Toolbar className={classes.toolbar}>
						<div>
							<Typography variant="h4" id="tableTitle" style={{ color: "white" }}>
								Projects
							</Typography>
						</div>

						<TextField id="standard-basic" label="Standard" onChange={handleSearchInput} />

						{user.role === "admin" ? (
							<div>
								<Tooltip title="Add">
									<IconButton
										aria-label="Add"
										onClick={() => projectSelect(null, "add")}
										className={classes.hover}
									>
										<AddIcon fontSize="large" style={{ fill: "white" }} />
									</IconButton>
								</Tooltip>
							</div>
						) : null}
					</Toolbar>
					<Table className={classes.table}>
						<TableHead>
							<TableRow>
								<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "9%" }}>
									No.
								</CustomTableCell>
								<CustomTableCell className={classes.tableHeadFontsize}>Project</CustomTableCell>
								<CustomTableCell className={classes.tableHeadFontsize}>Customer Name</CustomTableCell>
								<CustomTableCell className={classes.tableHeadFontsize}>Team</CustomTableCell>
								<CustomTableCell className={classes.tableHeadFontsize} style={{ width: "16%" }}>
									Status
								</CustomTableCell>

								<CustomTableCell className={classes.tableHeadFontsize} align="center">
									Actions
								</CustomTableCell>
							</TableRow>
						</TableHead>
						<TableBody>{search === "" ? AllProjects(projects) : AllProjects(searchProjects)}</TableBody>
					</Table>
				</Paper>
			)}
		</React.Fragment>
	);
};

const CustomTableCell = withStyles((theme) => ({
	head: {
		backgroundColor: "#40454F",
		color: "white",
		width: "20%"
	},
	body: {
		fontSize: 14
	}
}))(TableCell);

const mapStateToProps = (state) => {
	return {
		data: state.projects.data,
		loading: state.projects.loading,
		error: state.projects.error,
		selected: state.projects.selected,
		user: state.user.user,
		reload: state.projects.reload
	};
};

export default connect(mapStateToProps, {
	fetchProjects,
	projectSelect,
	projectDelete
})(withStyles(styles)(ProjectsPage));
