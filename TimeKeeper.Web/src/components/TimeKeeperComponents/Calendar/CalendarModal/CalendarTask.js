import React, { Fragment, useEffect, useState } from "react";
import { connect } from "react-redux";
import {
  Grid,
  Select,
  MenuItem,
  TextField,
  Input,
  IconButton
} from "@material-ui/core";
import { Formik, Form, Field } from "formik";

import { tasksUrl, apiPutRequest } from "../../../../utils/api";
import { editTask } from "../../../../store/actions/index";

import AddIcon from "@material-ui/icons/Add";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import TodayIcon from "@material-ui/icons/Today";

import moment from "moment";

const CustomSelectComponent = (props) => {
  return (
    <Select fullWidth {...props}>
      {props.data.map((p) => (
        <MenuItem value={p.id} key={p.id}>
          {p.name}
        </MenuItem>
      ))}
    </Select>
  );
};

const CustomeTextFieldComponent = (props) => <TextField {...props} />;

const CustomInputComponent = (props) => <Input {...props} fullWidth />;

function CalendarTask(props) {
  return (
    <Formik
      enableReinitialize
      onSubmit={(values) => {
        if (props.data) {
          let data = {
            id: props.data.id,
            day: {
              id: props.day.id
            },
            project: {
              id: values.project,
              name: props.projects[values.project - 1].name
            },
            description: values.description,
            hours: values.hours
          };

          props.editTask(props.data.id, data);
        }
      }}
      initialValues={{
        hours: props.data ? props.data.hours : "",
        project: props.data ? props.data.project.id : 1,
        description: props.data ? props.data.description : ""
      }}
    >
      <Form>
        <Grid container alignItems="center" className="mb-1-5">
          <span>{moment(props.day.date).format("YYYY-MM-DD")}</span>

          <TodayIcon />
        </Grid>
        <Fragment>
          <Grid container spacing={4} alignItems="center">
            <Grid item xs={3}>
              <Grid>
                <Field
                  name={"project"}
                  id={"project-select-"}
                  label="Project"
                  data={props.projects}
                  as={CustomSelectComponent}
                />
              </Grid>
              <Grid
                item
                style={{
                  padding: "1rem 0"
                }}
              >
                <Field
                  as={CustomInputComponent}
                  name={"hours"}
                  placeholder="Hours Worked"
                />
              </Grid>
            </Grid>
            <Grid item xs={8}>
              <Field
                as={CustomeTextFieldComponent}
                name={"description"}
                label="Description"
                multiline={true}
                rows={2}
                variant="outlined"
                fullWidth
              />
            </Grid>
            <Grid item xs={1} className="text-right">
              <Grid>
                <IconButton
                  className="align-adjust-margin"
                  aria-label="delete-working-hours"
                  color="primary"
                  type="submit"
                >
                  {props.data ? <EditIcon /> : <AddIcon />}
                </IconButton>
              </Grid>
            </Grid>
          </Grid>
        </Fragment>
      </Form>
    </Formik>
  );
}

export default connect(null, { editTask })(CalendarTask);
