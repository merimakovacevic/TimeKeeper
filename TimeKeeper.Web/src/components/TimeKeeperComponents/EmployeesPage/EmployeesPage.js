import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import {
  fetchEmployees,
  employeeSelect,
  employeeDelete
} from "../../../store/actions/index";
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
import styles from "../../../styles/EmployeesPageStyles";

import './myStyles.css'

import AddIcon from "@material-ui/icons/Add";
import VisibilityIcon from "@material-ui/icons/Visibility";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";

import EmployeesModal from "./EmployeesModal/EmployeesModal";

const EmployeesPage = (props) => {
  const { classes } = props;
  const { data, loading, error, selected, user, reload } = props;
  const { fetchEmployees, employeeSelect, employeeDelete } = props;
  const [searchEmployees, setSearchedEmp] = useState([]);
  const [search, setSearch] = useState("");

  let employees = data;

  useEffect(() => {
    fetchEmployees();
    setSearch("");
    employees = data;
  }, [reload]);

  const handleSearchInput = (event) => {
    setSearch(event.target.value);
    searchMessages();
  };

  const searchMessages = () => {
    const regex = new RegExp(search, "gi");
    const searchResults = employees.reduce((accumulator, event) => {
      if (
        (event.firstName && event.firstName.match(regex)) ||
        (event.lastName && event.lastName.match(regex))
      ) {
        accumulator.push(event);
      }
      return accumulator;
    }, []);

    setSearchedEmp(searchResults);
  };

  const AllEmployees = (employees) =>
    employees.map((e, i) => (
      <TableRow key={e.id}>
        <CustomTableCell>{i + 1}</CustomTableCell>
        <CustomTableCell>{e.firstName}</CustomTableCell>
        <CustomTableCell>{e.lastName}</CustomTableCell>
        <CustomTableCell>{e.email}</CustomTableCell>
        <CustomTableCell>{e.phone}</CustomTableCell>

        {user.role === "admin" ? (
          <CustomTableCell align="center">
            <IconButton
              aria-label="Edit"
              className={classes.editButton}
              onClick={() => employeeSelect(e.id, "edit")}
            >
              <EditIcon style={{ fill: "green" }} />
            </IconButton>
            <IconButton
              aria-label="Delete"
              className={classes.deleteButton}
              onClick={() => employeeDelete(e.id)}
            >
              <DeleteIcon color="error" />
            </IconButton>
          </CustomTableCell>
        ) : (
          <CustomTableCell align="center">
            <IconButton
              aria-label="View"
              onClick={() => employeeSelect(e.id, "view")}
            >
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
            <h2 className={classes.loaderText}>
              Please reload the application
            </h2>
            <Button
              variant="outlined"
              size="large"
              className={classes.loaderText}
            >
              Reload
            </Button>
          </div>
        </Backdrop>
      ) : (
        <Paper className={classes.root}>
          <TextField
            className='myLabel'
            style={{color: 'white'}}
              id="standard-basic"
              label="Search"
              color='secondary'
              className={classes.textField}
              onChange={handleSearchInput}
              InputProps={{
                color: 'white'
            }}
            />
          {selected ? <EmployeesModal selected={selected} open={true} /> : null}
          <Toolbar className={classes.toolbar}>
            
            <div>
              <Typography
                variant="h4"
                id="tableTitle"
                style={{ color: "white" }}
              >
                Employees
              </Typography>
            </div>

            

            {user.role === "admin" ? (
              <div>
                <Tooltip title="Add">
                  <IconButton
                    aria-label="Add"
                    onClick={() => employeeSelect(null, "add")}
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
                <CustomTableCell
                  className={classes.tableHeadFontsize}
                  style={{ width: "9%" }}
                >
                  No.
                </CustomTableCell>
                <CustomTableCell className={classes.tableHeadFontsize}>
                  First Name
                </CustomTableCell>
                <CustomTableCell className={classes.tableHeadFontsize}>
                  Last Name
                </CustomTableCell>
                <CustomTableCell className={classes.tableHeadFontsize}>
                  E-mail
                </CustomTableCell>
                <CustomTableCell
                  className={classes.tableHeadFontsize}
                  style={{ width: "16%" }}
                >
                  Phone
                </CustomTableCell>

                <CustomTableCell
                  className={classes.tableHeadFontsize}
                  align="center"
                >
                  Actions
                </CustomTableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {search === ""
                ? AllEmployees(employees)
                : AllEmployees(searchEmployees)}
            </TableBody>
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
    data: state.employees.data,
    loading: state.employees.loading,
    error: state.employees.error,
    selected: state.employees.selected,
    user: state.user.user,
    reload: state.employees.reload
  };
};

export default connect(mapStateToProps, {
  fetchEmployees,
  employeeSelect,
  employeeDelete
})(withStyles(styles)(EmployeesPage));
