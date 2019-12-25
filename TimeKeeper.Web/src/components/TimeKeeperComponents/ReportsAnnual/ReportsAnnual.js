import React, { Fragment, useState, useEffect } from "react";

import { MenuItem, TextField } from "@material-ui/core";
import {
  getAnnualReport,
  startLoading
} from "../../../store/actions/annualReportActions";
import { connect } from "react-redux";
import TableView from "../../TimeKeeperComponents/TableView/TableView";

function AnnualReport(props) {
  const [selectedYear, setSelectedYear] = useState(2019);
  const title = "Annual Overview";
  /*  const backgroundImage =
    "https://images.pexels.com/photos/1629236/pexels-photo-1629236.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940";
  console.log("props", props); */

  useEffect(() => {
    props.getAnnualReport(selectedYear);
  }, [selectedYear]);

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

  return (
    <Fragment>
      {!props.annualReport.isLoading && (
        <Fragment>
          <YearDropdown />
          <TableView
            title={title}
            //  backgroundImage={backgroundImage}
            table={props.annualReport.table}
          />
        </Fragment>
      )}
      {props.annualReport.isLoading && <div>is loading </div>}
    </Fragment>
  );
}

const mapStateToProps = (state) => {
  return {
    annualReport: state.annualReport
  };
};

export default connect(mapStateToProps, { getAnnualReport, startLoading })(
  AnnualReport
);
