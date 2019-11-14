import React from "react";
import axios from "axios";
import classNames from "classnames";
import config from "../../../config";

import { ButtonGroup } from "@material-ui/core";
import { Backdrop } from "@material-ui/core";
import { withStyles } from "@material-ui/core/styles";
import styles from "../Styles/TableStyles";
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
import CircularProgress from "@material-ui/core/CircularProgress";
import Tooltip from "@material-ui/core/Tooltip";

import IconButton from "@material-ui/core/IconButton";
import AddIcon from "@material-ui/icons/Add";

import EmployeesModal from "./EmployeesModal/EmployeesModal";

const createData = (id, name, surname, email, phone) => {
  return { id: id, name, surname, email, phone };
};

const desc = (a, b, orderBy) =>
  b[orderBy] < a[orderBy] ? -1 : b[orderBy] > a[orderBy] ? 1 : 0;

const stableSort = (array, cmp) => {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = cmp(a[0], b[0]);
    if (order !== 0) return order;
    return a[1] - b[1];
  });
  return stabilizedThis.map(el => el[0]);
};

const getSorting = (order, orderBy) =>
  order === "desc"
    ? (a, b) => desc(a, b, orderBy)
    : (a, b) => -desc(a, b, orderBy);

const rows = [
  { id: "name", label: "Name" },
  { id: "surname", label: "Surname" },
  { id: "email", label: "Email" },
  { id: "phone", label: "Number" },
  { id: "actions", label: "" }
];

class EnhancedTable extends React.Component {
  state = {
    selectedId: null,
    open: false,
    loading: null,
    order: "asc",
    orderBy: "name",
    selected: [],
    data: [],
    page: 0,
    rowsPerPage: 6,
    new: false
  };

  componentDidMount() {
    this.setState({ loading: true });
    axios(`${config.apiUrl}employees`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: config.token
      }
    })
      .then(res => {
        let fetchedData = res.data.map(r =>
          createData(r.id, r.firstName, r.lastName, r.email, r.phone)
        );
        this.setState({ data: fetchedData, loading: false });
      })
      .catch(err => this.setState({ loading: false }));
  }

  componentDidUpdate(prevProps, prevState) {
    // only update chart if the data has changed
    if (prevState.selectedId && !this.state.selectedId) {
      this.setState({ loading: true });
      axios(`${config.apiUrl}employees`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: config.token
        }
      })
        .then(res => {
          let fetchedData = res.data.map(r =>
            createData(r.id, r.firstName, r.lastName, r.email, r.phone)
          );
          this.setState({ data: fetchedData, loading: false });
        })
        .catch(err => this.setState({ loading: false }));
    }
  }

  deleteEmployee = id => {
    this.setState({ selectedId: id });
    axios
      .delete(`${config.apiUrl}employees/${id}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: config.token
        }
      })
      .then(res => {
        console.log(res);
        this.setState({ selectedId: null });
      });
  };

  handleRequestSort = property => {
    const orderBy = property;
    let order = "desc";

    this.state.orderBy === property && this.state.order === "desc"
      ? (order = "asc")
      : (order = "desc");

    this.setState({ order, orderBy });
  };

  handleChangePage = (event, page) => this.setState({ page });

  handleOpen = value => {
    this.setState({ open: true, selectedId: value });
  };

  handleNewOpen = value => this.setState({ new: value });

  handleClose = () => {
    this.setState({ open: false, selectedId: null });
  };

  render() {
    const { classes } = this.props;
    const {
      data,
      order,
      orderBy,
      rowsPerPage,
      page,
      loading,
      open,
      selectedId
    } = this.state;

    return (
      <React.Fragment>
        {loading ? (
          <Backdrop open={true}>
            <div className={classNames(classes.center)}>
              <CircularProgress
                size={100}
                className={classNames(classes.loader)}
              />
              <h1 className={classNames(classes.loaderText)}>Loading...</h1>
            </div>
          </Backdrop>
        ) : open ? (
          <EmployeesModal
            open={open}
            handleClose={this.handleClose}
            id={selectedId}
            new={this.state.new}
          />
        ) : (
          <Paper className={classes.root}>
            <Toolbar className={classNames(classes.root2, {})}>
              <div className={classes.title}>
                <Typography variant="h5" id="tableTitle">
                  Employees
                </Typography>
              </div>
              <div className={classes.spacer} />
              <div className={classes.actions}>
                <Tooltip title="Add">
                  <IconButton
                    aria-label="Add"
                    onClick={() => this.handleOpen(666)}
                  >
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
                    .map((n, i) => {
                      return (
                        <TableRow hover tabIndex={-1} key={n.id}>
                          <TableCell component="th" scope="row">
                            {n.name}
                          </TableCell>
                          <TableCell>{n.surname}</TableCell>
                          <TableCell>{n.email}</TableCell>
                          <TableCell>{n.phone}</TableCell>
                          <TableCell align="center">
                            {" "}
                            <ButtonGroup size='small' fullWidth>
                              <Button
                                fullWidth
                                onClick={() => this.handleOpen(n.id)}
                                color="primary"
                              >
                                Edit{" "}
                              </Button>
                              <Button
                                fullWidth
                                color="secondary"
                                onClick={() => this.deleteEmployee(n.id)}
                              >
                                Delete
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
              rowsPerPageOptions={[]}
            />
          </Paper>
        )}
      </React.Fragment>
    );
  }
}

export default withStyles(styles)(EnhancedTable);
