import React, { Fragment, useState, useEffect } from "react";
// import Config from "../Config";
import BlockElementSpinner from "../../components/BlockElementSpinner/BlockElementSpinner";
// import { connect } from "react-redux";
import data from "./data";
import PieChart from "../../components/PieChart/PieChart";
import BarChart from "../../components/BarChart/BarChart";
import { Container, Grid, Paper, TextField, MenuItem } from "@material-ui/core";
import axios from "axios";
import Config from "../../Config";

function TeamDashboard(props) {
  const [selectedYear, setSelectedYear] = useState(2019);
  const [selectedMonth, setSelectedMonth] = useState(1);
  const [selectedTeam, setSelectedTeam] = useState(null);
  const [teams, setTeams] = useState([]);

  useEffect(() => {
    axios
      .get(Config.apiUrl + "teams", Config.authHeader)
      .then(res => {
        // console.log("teams", res);
        const tempTeams = res.data.map(x => {
          return {
            id: x.id,
            name: x.name,
            description: x.description,
            members: x.members
          };
        });
        setTeams(tempTeams);
      })
      .catch(err => {
        console.log("teams err", err);
        alert(err);
      });
  }, [selectedTeam]);

  const handleSelectedYear = e => {
    setSelectedYear(e.target.value);
  };
  const handleSelectedMonth = e => {
    setSelectedMonth(e.target.value);
  };
  const handleSelectedTeam = e => {
    setSelectedTeam(e.target.value);
  };

  const TotalHoursChart = ({ data }) => {
    // console.log(data);
    const modifiedData = [
      { x: "Working Hours", y: parseInt(data.totalWorkingHours) },
      { x: "Remaining Hours", y: Math.abs(parseInt(data.totalHours - data.totalWorkingHours)) }
    ];
    return <PieChart padAngle={1} data={modifiedData} title={"Total Hours"} />;
  };
  const RevenueChart = ({ data }) => {
    const modifiedData = data.map((x, i) => {
      return { name: x.project.name, value: parseInt(x.revenue) };
    });
    return (
      <BarChart data={modifiedData} height={350} width={350} xLabel="Projects" yLabel="Revenue" />
    );
  };
  const PtoChart = ({ data }) => {
    const modifiedData = data.map((x, i) => {
      return { name: x.employee.name, value: parseInt(x.paidTimeOff) };
    });
    return (
      <BarChart
        data={modifiedData}
        height={350}
        width={350}
        xLabel="Team Members"
        yLabel="Paid Time Off"
      />
    );
  };
  const OvertimeChart = ({ data }) => {
    const modifiedData = data.map((x, i) => {
      return { name: x.employee.name, value: parseInt(x.overtime) };
    });
    return (
      <BarChart
        data={modifiedData}
        height={350}
        width={350}
        xLabel="Team Members"
        yLabel="Overtime"
      />
    );
  };
  const MissingEntriesChart = ({ data }) => {
    const modifiedData = data.map(x => {
      return { name: x.employee.name, value: parseInt(x.missingEntries) };
    });
    return (
      <BarChart
        data={modifiedData}
        height={250}
        width={250}
        horizontal={true}
        angle={0}
        labelPadding={0}
        fontSize={7}
      />
    );
  };
  const UtilizationCharts = ({ data }) => {
    return data.map((x, i) => {
      const modifiedData = [
        { x: "Working Hours", y: parseInt(x.workingHours) },
        { x: "Remaining Hours", y: Math.abs(parseInt(x.totalHours - x.workingHours)) }
      ];
      return (
        <Grid item md={3} key={i}>
          <PieChart height={250} padAngle={1} data={modifiedData} title={x.employee.name} />
        </Grid>
      );
    });
  };
  const YearDropdown = () => (
    <TextField
      variant="outlined"
      id="selected-year"
      select
      label="Selected Year"
      value={selectedYear}
      onChange={handleSelectedYear}
      margin="normal"
      fullWidth={true}
    >
      {[2019, 2018, 2017].map(x => {
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
      id="selected-month"
      select
      label="Selected Month"
      value={selectedMonth}
      onChange={handleSelectedMonth}
      margin="normal"
      fullWidth={true}
    >
      {[
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
      ].map((x, i) => {
        return (
          <MenuItem value={i + 1} key={x}>
            {x}
          </MenuItem>
        );
      })}
    </TextField>
  );

  const TeamDropdown = () => (
    <TextField
      variant="outlined"
      id="selected-team"
      select
      label="Selected Team"
      value={selectedTeam}
      onChange={handleSelectedTeam}
      margin="normal"
      fullWidth={true}
    >
      {teams.map(x => {
        return (
          <MenuItem value={x.id} key={x.id}>
            {x.name}
          </MenuItem>
        );
      })}
    </TextField>
  );

  return (
    <Fragment>
      {/* {!props.annualReport.isLoading && ( */}
      <Container className="mt-3 mb-5">
        <Grid spacing={4} container alignItems="center" justify="space-between" className="mb-1-5">
          <Grid item sm={6}>
            <h3 className="mb-0 mt-0">Teams Dashboard</h3>
          </Grid>
          <Grid item sm={2}>
            <MonthDropdown />
          </Grid>
          <Grid item sm={2}>
            <YearDropdown />
          </Grid>
          <Grid item sm={2}>
            <TeamDropdown />
          </Grid>
        </Grid>
        <Grid container spacing={4} justify="space-between" alignItems="center" className="mb-1-5">
          <Grid item md={4}>
            <Paper elevation={3} className="company-totals">
              <div className="flex flex-column align-items-center justify-content-center">
                <h4>TOTAL HOURS</h4> <h1 className="mb-0 mt-0">{data.totalHours}</h1>
              </div>
            </Paper>
          </Grid>
          <Grid item md={4}>
            <Paper elevation={3} className="company-totals">
              <div className="flex flex-column align-items-center justify-content-center">
                <h4>EMPLOYEES</h4> <h1 className="mb-0 mt-0">{data.numberOfEmployees}</h1>
              </div>
            </Paper>
          </Grid>
          <Grid item md={4}>
            <Paper elevation={3} className="company-totals">
              <div className="flex flex-column align-items-center justify-content-center">
                <h4>PROJECTS </h4> <h1 className="mb-0 mt-0">{data.numberOfProjects}</h1>
              </div>
            </Paper>
          </Grid>
        </Grid>
        <Paper elevation={3} className="company-totals" className="mb-3">
          <Grid container spacing={4} justify="center" alignItems="center">
            <Grid item md={3}>
              <TotalHoursChart data={data} />
            </Grid>
            <Grid item md={3}>
              <MissingEntriesChart data={data.employeeTimes} />
            </Grid>
            <Grid item md={3}>
              <PtoChart data={data.employeeTimes} />
            </Grid>
            <Grid item md={3}>
              <OvertimeChart data={data.employeeTimes} />
            </Grid>
          </Grid>
        </Paper>
        <Paper elevation={3} className="company-totals">
          <Grid container spacing={4} alignItems="center">
            <UtilizationCharts data={data.employeeTimes} />
          </Grid>
        </Paper>
      </Container>
      {/* )} */}
      {/* {props.annualReport.isLoading && <BlockElementSpinner />} */}
    </Fragment>
  );
}

// const mapStateToProps = state => {
//   return {
//     annualReport: state.annualReport
//   };
// };

// export default connect(mapStateToProps, { getAnnualReport, startLoading })(AnnualReport);
export default TeamDashboard;
