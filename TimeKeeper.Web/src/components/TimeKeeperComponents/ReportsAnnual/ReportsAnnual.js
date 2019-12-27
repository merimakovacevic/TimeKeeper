import React, { Fragment, useState, useEffect } from "react";
import styles from "../../../styles/EmployeesPageStyles";
import { withStyles } from "@material-ui/core/styles";

import {
  MenuItem,
  TextField,
  CircularProgress,
  Backdrop
} from "@material-ui/core";
import {
  getAnnualReport,
  startLoading
} from "../../../store/actions/annualReportActions";
import { connect } from "react-redux";
import TableView from "../../TimeKeeperComponents/TableView/TableView";

function AnnualReport(props) {
  const [selectedYear, setSelectedYear] = useState(2019);
  const { classes, loading, isLoading } = props;
  const title = "Annual Overview";
  /*  const backgroundImage =
    "https://images.pexels.com/photos/1629236/pexels-photo-1629236.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940";
  console.log("props", props); */

  useEffect(() => {
    props.getAnnualReport(selectedYear);
  }, [selectedYear]);

  const YearDropdown = () => (
    <TextField
    className={classes.dropdown}
      variant="outlined"
      id="Selected Year"
      select
      label="Selected Year"
      value={selectedYear}
      onChange={(e) => {
        props.startLoading();
        setSelectedYear(e.target.value);
      }}
      margin="normal"
    >
      {[2019, 2018, 2017].map((x) => {
        return (
          <MenuItem value={x} key={x}>
            {x}
          </MenuItem>
        );
      })}
    </TextField>
  );

  return (
    <Fragment>
      {!props.annualReport.isLoading && (
        <Fragment>
          <YearDropdown  />
          <TableView title={title} table={props.annualReport.table} />
        </Fragment>
      )}
      {props.annualReport.isLoading && (
        <Backdrop open={isLoading}>
          <div className={classes.center}>
            <CircularProgress size={100} className={classes.loader} />
            <h1 className={classes.loaderText}>Loading...</h1>
          </div>
        </Backdrop>
      )}
    </Fragment>
  );
}

const mapStateToProps = (state) => {
  return {
    annualReport: state.annualReport,
    loading: state.annualReport.loading,
    isLoading: state.annualReport.isLoading
  };
};

export default connect(mapStateToProps, { getAnnualReport, startLoading })(
  withStyles(styles)(AnnualReport)
);
