import React, { Fragment, useState, useEffect } from "react";
import TableView from "../../TimeKeeperComponents/TableView/TableView";
import {
  MenuItem,
  TextField,
  CircularProgress,
  Backdrop
} from "@material-ui/core";
import { withStyles } from "@material-ui/core/styles";

import {
  getMonthlyReport,
  startLoading
} from "../../../store/actions/monthlyReportActions";
import { connect } from "react-redux";
import styles from "../../../styles/EmployeesPageStyles";

function MonthlyReport(props) {
  const [selectedYear, setSelectedYear] = useState(2019);
  const [selectedMonth, setSelectedMonth] = useState(1);
  const { isLoading, classes } = props;
  const title = "Monthly Overview";
  const backgroundImage =
    "https://images.pexels.com/photos/1629236/pexels-photo-1629236.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940";

  useEffect(() => {
    props.getMonthlyReport(selectedYear, selectedMonth);
  }, [selectedYear, selectedMonth]);

  const handleSelectedYear = (e) => {
    setSelectedYear(e.target.value);
  };
  const handleSelectedMonth = (e) => {
    setSelectedMonth(e.target.value);
  };

  const YearDropdown = () => (
    <TextField
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

  const MonthDropdown = () => (
    <TextField
      variant="outlined"
      id="Selected Month"
      select
      label="Selected Month"
      value={selectedMonth}
      onChange={(e) => {
        props.startLoading();
        setSelectedMonth(e.target.value);
      }}
      margin="normal"
    >
      {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12].map((x) => {
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
      {!props.monthlyReport.isLoading ? (
        <Fragment>
          <YearDropdown />
          <MonthDropdown />
          <TableView
            title={title}
            backgroundImage={backgroundImage}
            table={props.monthlyReport.table}
            selectedYear={selectedYear}
            handleSelectedYear={handleSelectedYear}
            selectedMonth={selectedMonth}
            handleSelectedMonth={handleSelectedMonth}
            hasOptions
            optionSubmit={true}
            sumTotals={true}
          />
        </Fragment>
      ) : (
        <Backdrop open={isLoading}>
          <div className={classes.center}>
            <CircularProgress size={100} className={classes.loader} />
            <h1 className={classes.loaderText}>Loading...</h1>
          </div>
        </Backdrop>
      )}
      {props.monthlyReport.isLoading}
    </Fragment>
  );
}

const mapStateToProps = (state) => {
  return {
    monthlyReport: state.monthlyReport,
    isLoading: state.monthlyReport.isLoading
  };
};

export default connect(mapStateToProps, { getMonthlyReport, startLoading })(
  withStyles(styles)(MonthlyReport)
);
