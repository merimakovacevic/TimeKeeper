import React, { useState, useEffect } from "react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import Calendar from "react-calendar";
import moment from "moment";

import CalendarModal from "./CalendarModal/CalendarModal";
import { apiGetAllRequest } from "../../../utils/api";
import { loadCalendar } from "../../../store/actions";

function CalendarDisplay(props) {
  const [date, setDate] = useState(new Date(2019, 5, 6, 10, 33, 30, 0));
  const [year, setYear] = useState(moment(date).format("YYYY"));
  const [month, setMonth] = useState(moment(date).format("MM"));
  const [day, setDay] = useState(moment(date).format("DD"));
  const [employeeId] = useState(props.user.user.id);
  const [projects, setProjects] = useState([]);
  const [selectedTab, setSelectedTab] = useState(0);

  useEffect(() => {
    apiGetAllRequest("http://api-charlie.gigischool.rocks/api/projects").then(
      (res) => {
        setProjects(res.data.data);
      }
    );

    props.loadCalendar(employeeId, year, month);
    if (props.calendarMonth) {
      const selectedYear = moment(date).format("YYYY");
      const selectedMonth = moment(date).format("MM");
      const selectedDay = moment(date).format("DD");
      setDate(date);
      setYear(selectedYear);
      setMonth(selectedMonth);
      setDay(selectedDay);
    }
  }, []);
  const handleSelectedTab = (event, newValue) => {
    setSelectedTab(newValue);
  };
  const changeData = (selectedDate) => {
    const selectedYear = moment(selectedDate).format("YYYY");
    const selectedMonth = moment(selectedDate).format("MM");
    const selectedDay = moment(selectedDate).format("DD");
    if (selectedYear !== year || selectedMonth !== month) {
      props.loadCalendar(employeeId, selectedYear, selectedMonth);
      setDate(selectedDate);
      setYear(selectedYear);
      setMonth(selectedMonth);
      setDay(selectedDay);
    } else if (selectedDay !== day) {
      setDate(selectedDate);
      setDay(selectedDay);
    }
  };
  function onChange(date) {
    changeData(date);
  }

  function a11yProps(index) {
    return {
      id: `tab-${index}`,
      "aria-controls": `tabpanel-${index}`
    };
  }

  return (
    <div>
      <Calendar onChange={onChange} value={date} />
      <div>
        {props.calendarMonth &&
        moment(props.calendarMonth[day - 1].date).format("YYYY-MM-DD") ===
          moment(date).format("YYYY-MM-DD") ? (
          <div>
            <CalendarModal
              selectedTab={selectedTab}
              handleSelectedTab={handleSelectedTab}
              a11yProps={a11yProps}
              calendarMonth={props.calendarMonth}
              projects={projects}
              day={props.calendarMonth[day - 1]}
            />
          </div>
        ) : (
          <h2>No data</h2>
        )}
      </div>
    </div>
  );
}

const mapStateToProps = (state) => {
  return {
    loading: state.calendarMonth.loading,
    calendarMonth: state.calendarMonth.data.data,
    user: state.user
  };
};

export default connect(mapStateToProps, { loadCalendar })(
  withRouter(CalendarDisplay)
);
