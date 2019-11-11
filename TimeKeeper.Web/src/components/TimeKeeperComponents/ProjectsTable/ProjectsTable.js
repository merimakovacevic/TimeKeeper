import React from "react";
import classNames from "classnames";

import { withStyles } from "@material-ui/core/styles";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TablePagination from "@material-ui/core/TablePagination";
import TableRow from "@material-ui/core/TableRow";
import TableSortLabel from "@material-ui/core/TableSortLabel";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Paper from "@material-ui/core/Paper";
import Button from "@material-ui/core/Button";
import { ButtonGroup } from "@material-ui/core";

import IconButton from "@material-ui/core/IconButton";
import Tooltip from "@material-ui/core/Tooltip";

import { lighten } from "@material-ui/core/styles/colorManipulator";
import AddIcon from "@material-ui/icons/Add";

let counter = 0;
function createData(projectName, customerName, teamName, status) {
    counter += 1;
    return { id: counter, projectName, customerName, teamName, status };
}

function desc(a, b, orderBy) {
    if (b[orderBy] < a[orderBy]) {
        return -1;
    }
    if (b[orderBy] > a[orderBy]) {
        return 1;
    }
    return 0;
}

function stableSort(array, cmp) {
    const stabilizedThis = array.map((el, index) => [el, index]);
    stabilizedThis.sort((a, b) => {
        const order = cmp(a[0], b[0]);
        if (order !== 0) return order;
        return a[1] - b[1];
    });
    return stabilizedThis.map(el => el[0]);
}

function getSorting(order, orderBy) {
    return order === "desc" ? (a, b) => desc(a, b, orderBy) : (a, b) => -desc(a, b, orderBy);
}

const rows = [
    { id: "projectName", label: "Project name" },
    { id: "customerName ", label: "Customer name " },
    { id: "teamName", label: "teamName" },
    { id: "status", label: "Status" },
    { id: "actions", label: "" }
];

const styles = theme => ({
    root: {
        width: "90%",
        margin: "0 auto",
        padding: "1rem"
    },
    table: {
        minWidth: 1020
    },
    tableWrapper: {
        overflowX: "auto",
        padding: "2rem"
    },
    highlight:
        theme.palette.type === "light"
            ? {
                  color: theme.palette.secondary.main,
                  backgroundColor: lighten(theme.palette.secondary.light, 0.85)
              }
            : {
                  color: theme.palette.text.primary,
                  backgroundColor: theme.palette.secondary.dark
              },
    spacer: {
        flex: "1 1 100%"
    },
    actions: {
        color: theme.palette.text.secondary
    },
    title: {
        flex: "0 0 auto"
    },
    tableCell: {
        fontSize: "1.1rem",
        fontWeight: "bold",
        backgroundColor: "#f5f6fa"
    }
});

class EnhancedTable extends React.Component {
    state = {
        order: "asc",
        orderBy: "projectName",
        selected: [],
        data: [
            createData("Tajib", "Smajlovic", "maiiil@mail.com", "+33351531531"),
            createData("Ajdin", "Zorlak", "maiiil@mail.com", "+33351531531"),
            createData("Armin", "Odob", "maiiil@mail.com", "+33351531531"),
            createData("Amila", "Test", "maiiil@mail.com", "+33351531531")
        ],

        page: 0,
        rowsPerPage: 5
    };

    handleRequestSort = property => {
        const orderBy = property;

        let order = "desc";

        if (this.state.orderBy === property && this.state.order === "desc") {
            order = "asc";
        } else {
            order = "desc";
        }

        this.setState({ order, orderBy });
    };

    handleChangePage = (event, page) => {
        this.setState({ page });
    };

    handleChangeRowsPerPage = event => {
        this.setState({ rowsPerPage: event.target.value });
    };

    isSelected = id => this.state.selected.indexOf(id) !== -1;

    render() {
        const { classes } = this.props;
        const { data, order, orderBy, selected, rowsPerPage, page } = this.state;
        const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);

        return (
            <Paper className={classes.root}>
                <Toolbar className={classNames(classes.root, {})}>
                    <div className={classes.title}>
                        <Typography variant="h4" id="tableTitle">
                            Projects
                        </Typography>
                    </div>
                    <div className={classes.spacer} />
                    <div className={classes.actions}>
                        <Tooltip title="Add">
                            <IconButton aria-label="Add">
                                <AddIcon />
                            </IconButton>
                        </Tooltip>
                    </div>
                </Toolbar>
                <div className={classes.tableWrapper}>
                    <Table className={classes.table} aria-labelledby="tableTitle">
                        <TableHead>
                            <TableRow>
                                {rows.map(
                                    row => (
                                        <TableCell
                                            className={classNames(classes.tableCell)}
                                            key={row.id}
                                            sortDirection={orderBy === row.id ? order : false}
                                        >
                                            <Tooltip
                                                title="Sort"
                                                placement={
                                                    row.numeric ? "bottom-end" : "bottom-start"
                                                }
                                                enterDelay={150}
                                            >
                                                <TableSortLabel
                                                    active={orderBy === row.id}
                                                    direction={order}
                                                    onClick={() => this.handleRequestSort(row.id)}
                                                >
                                                    {row.label}
                                                </TableSortLabel>
                                            </Tooltip>
                                        </TableCell>
                                    ),
                                    this
                                )}
                            </TableRow>
                        </TableHead>

                        <TableBody>
                            {stableSort(data, getSorting(order, orderBy))
                                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                .map(n => {
                                    const isSelected = this.isSelected(n.id);
                                    return (
                                        <TableRow
                                            hover
                                            onClick={event => this.handleClick(event, n.id)}
                                            tabIndex={-1}
                                            key={n.id}
                                            selected={isSelected}
                                        >
                                            <TableCell component="th" scope="row">
                                                {n.projectName}
                                            </TableCell>
                                            <TableCell>{n.customerName}</TableCell>
                                            <TableCell>{n.teamName}</TableCell>
                                            <TableCell>{n.status}</TableCell>
                                            <TableCell align="center">
                                                {" "}
                                                <ButtonGroup>
                                                    <Button
                                                        variant="outlined"
                                                        size="small"
                                                        color="primary"
                                                    >
                                                        View
                                                    </Button>
                                                    <Button
                                                        variant="outlined"
                                                        size="small"
                                                        color="primary"
                                                    >
                                                        Edit
                                                    </Button>
                                                </ButtonGroup>
                                            </TableCell>
                                        </TableRow>
                                    );
                                })}
                        </TableBody>
                    </Table>
                </div>
                <TablePagination
                    rowsPerPageOptions={[5, 10, 25]}
                    component="div"
                    count={data.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    backIconButtonProps={{
                        "aria-label": "Previous Page"
                    }}
                    nextIconButtonProps={{
                        "aria-label": "Next Page"
                    }}
                    onChangePage={this.handleChangePage}
                    labelRowsPerPage=""
                    rowsPerPageOptions=""
                />
            </Paper>
        );
    }
}

export default withStyles(styles)(EnhancedTable);
