import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { customerSelect } from "../../../store/actions/index";
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

import AddIcon from "@material-ui/icons/Add";
import VisibilityIcon from "@material-ui/icons/Visibility";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import { fetchCustomers } from "../../../store/actions/index";

import CustomersModal from "./CustomersModal/CustomersModal";

const CustomersPage = (props) => {
  const { classes } = props;
  const { data, loading, error, selected, user, reload } = props;
  const { fetchCustomers, customerSelect, customerDelete } = props;
  const [searchCustomers, setSearchedEmp] = useState([]);
  const [search, setSearch] = useState("");

  let customers = data;

  useEffect(() => {
    fetchCustomers();
    setSearch("");
    customers = data;
  }, [reload]);

  const handleSearchInput = (event) => {
    setSearch(event.target.value);
    searchMessages();
  };

  const searchMessages = () => {
    const regex = new RegExp(search, "gi");
    const searchResults = customers.reduce((accumulator, event) => {
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

  const AllCustomers = (customers) =>
    customers.map((e, i) => (
      <TableRow key={e.id}>
        <CustomTableCell>{i + 1}</CustomTableCell>
        <CustomTableCell>{e.name}</CustomTableCell>
        <CustomTableCell>{e.contactName}</CustomTableCell>
        <CustomTableCell>{e.emailAddress}</CustomTableCell>
        <CustomTableCell>{e.homeAddress.city}</CustomTableCell>
        <CustomTableCell>{e.status.name}</CustomTableCell>

        {user.role === "admin" ? (
          <CustomTableCell align="center">
            <IconButton
              aria-label="Edit"
              className={classes.editButton}
              onClick={() => customerSelect(e.id, "edit")}
            >
              <EditIcon style={{ fill: "green" }} />
            </IconButton>
            <IconButton
              aria-label="Delete"
              className={classes.deleteButton}
              onClick={() => customerDelete(e.id)}
            >
              <DeleteIcon color="error" />
            </IconButton>
          </CustomTableCell>
        ) : (
          <CustomTableCell align="center">
            <IconButton
              aria-label="View"
              onClick={() => customerSelect(e.id, "view")}
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
          {selected ? <CustomersModal selected={selected} open={true} /> : null}
          <Toolbar className={classes.toolbar}>
            <div>
              <Typography
                variant="h4"
                id="tableTitle"
                style={{ color: "white" }}
              >
                Customers
              </Typography>
            </div>

            <TextField
              id="standard-basic"
              className={classes.textField}
              label="Standard"
              onChange={handleSearchInput}
            />

            {user.role === "admin" ? (
              <div>
                <Tooltip title="Add">
                  <IconButton
                    aria-label="Add"
                    onClick={() => customerSelect(null, "add")}
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
                  Business Name{" "}
                </CustomTableCell>
                <CustomTableCell className={classes.tableHeadFontsize}>
                  Contact Name
                </CustomTableCell>
                <CustomTableCell className={classes.tableHeadFontsize}>
                  E-mail
                </CustomTableCell>
                <CustomTableCell
                  className={classes.tableHeadFontsize}
                  style={{ width: "16%" }}
                >
                  City
                </CustomTableCell>
                <CustomTableCell className={classes.tableHeadFontsize}>
                  Status
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
                ? AllCustomers(customers)
                : AllCustomers(searchCustomers)}
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
    data: state.customers.data,
    loading: state.customers.loading,
    error: state.customers.error,
    selected: state.customers.selected,
    user: state.user.user,
    reload: state.customers.reload
  };
};

export default connect(mapStateToProps, {
  fetchCustomers,
  customerSelect
})(withStyles(styles)(CustomersPage));
