import React from "react";
import { withStyles } from "@material-ui/core/styles";
import { Formik, Field, Form } from "formik";
import * as Yup from "yup";

import moment from "moment";

import Input from "@material-ui/core/Input";
import {
  Dialog,
  DialogContent,
  Button,
  InputLabel,
  Select,
  MenuItem
} from "@material-ui/core";
import axios from "axios";
import config from "../../../../config";

import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";

const styles = theme => ({
  parrent: {},
  wrapper: {
    display: "flex",
    width: "100%",
    height: "100%",
    padding: "2rem"
  },
  container: {
    width: "100%",
    margin: "2rem"
  },
  input: {
    margin: "0 0 1rem 0"
  },
  errorMessage: {
    color: "red",
    fontSize: ".85rem"
  },
  root: {
    width: "100%",
    marginTop: theme.spacing(3)
  },
  table: {
    minWidth: 0
  },
  tableHead: {
    backgroundColor: "#f5f6fa"
  },
  tableCell: {
    fontSize: "1.1rem",
    fontWeight: "bold"
  },
  img: {
    height: 200,
    width: 220,
    margin: "0 0 2rem 0",
    objectFit: "contain"
  },

  buttons: {
    display: "flex",
    justifyContent: "space-between",
    marginTop: "6rem"
  }
});

const test = membersData => {
  let index = membersData.indexOf(",");
  let team = membersData.substr(index + 1);

  return team;
};

const positions = [
  { id: 1, name: "Chief Executive Officer" },
  { id: 2, name: "Chief Technical Officer" },
  { id: 3, name: "Chief Operations Officer" },
  { id: 4, name: "Manager" },
  { id: 5, name: "HR Manager" },
  { id: 6, name: "Developer" },
  { id: 7, name: "UI/UX Designer" },
  { id: 8, name: "QA Enginee" }
];

const statuses = [
  { id: 1, name: "Waiting for the task" },
  { id: 2, name: "Active" },
  { id: 3, name: "On hold" },
  { id: 4, name: "Leaver" }
];

const Schema = Yup.object().shape({
  salary: Yup.number().required("Salary can't be empty!"),
  firstName: Yup.string()
    .min(2, "First Name too short!")
    .max(32, "First Name too long!")
    .required("First Name can't be empty!"),
  lastName: Yup.string()
    .min(2, "Last Name too short!")
    .max(32, "Last Name too long!")
    .required("Last Name can't be empty!"),
  email: Yup.string().required("Email can't be empty!"),
  phone: Yup.string().required("Phone Number can't be empty!"),
  birthday: Yup.string().required("Birth Date can't be empty!"),
  employmentBeginDate: Yup.string().required(
    "Employment Begin Date can't be empty!"
  ),
  employmentEndDate: Yup.string().required(
    "Employment End Date can't be empty!"
  ),
  position: Yup.string().required("Job Title can't be empty!"),
  status: Yup.string().required("Status can't be empty!")
});

class Inputs extends React.Component {
  state = { employee: null, finish: false, rows: [] };

  fetchEmployee = id => {
    if (id === 666) {
      this.setState({ finish: true });
    } else {
      axios(`${config.apiUrl}employees/${id}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: config.token
        }
      })
        .then(res => {
          //console.log(res.data.members);
          let fetchedName = [];
          res.data.members.forEach(r => {
            let team = test(r.name);
            let id = r.id;
            let data = { id, team };
            fetchedName.push(data);
          });
          console.log(fetchedName);

          this.setState({
            employee: res.data,
            rows: fetchedName,
            finish: true
          });
        })
        .catch(() => this.setState({ finish: true }));
    }
  };

  componentDidMount() {
    this.fetchEmployee(this.props.id);
  }

  render() {
    const CustomInputComponent = props => (
      <Input
        // required={true}
        fullWidth={true}
        className={classes.input}
        {...props}
      />
    );

    const CustomSelectComponent = props => {
      return (
        <Select fullWidth {...props} className={classes.input}>
          <MenuItem value={1}>Chief Executive Officer</MenuItem>
          <MenuItem value={2}>Chief Technical Officer</MenuItem>
          <MenuItem value={3}>Chief Operations Officer</MenuItem>
          <MenuItem value={4}>Manager</MenuItem>
          <MenuItem value={5}>HR Manager</MenuItem>
          <MenuItem value={6}>Developer</MenuItem>
          <MenuItem value={7}>UI/UX Designer</MenuItem>
          <MenuItem value={8}>QA Engineer</MenuItem>
        </Select>
      );
    };

    const CustomStatusComponent = props => {
      return (
        <Select fullWidth {...props} className={classes.input}>
          <MenuItem value={1}>Waiting for the task</MenuItem>
          <MenuItem value={2}>Active</MenuItem>
          <MenuItem value={3}>On hold</MenuItem>
          <MenuItem value={4}>Leaver</MenuItem>
        </Select>
      );
    };

    const { classes, open, handleClose, id } = this.props;
    const { employee, finish } = this.state;

    // if (!finish && id) {
    //   this.fetchEmployee(id);
    // }

    return (
      <React.Fragment>
        {finish ? (
          <Formik
            validationSchema={Schema}
            initialValues={{
              salary: employee ? employee.salary : "",
              firstName: employee ? employee.firstName : "",
              lastName: employee ? employee.lastName : "",
              email: employee ? employee.email : "",
              phone: employee ? employee.phone : "",
              birthday: employee
                ? moment(employee.birthday).format("YYYY-MM-DD")
                : "",
              employmentBeginDate: employee
                ? moment(employee.beginDate).format("YYYY-MM-DD")
                : "",
              employmentEndDate: employee
                ? moment(employee.endDate).format("YYYY-MM-DD")
                : "",
              position: employee ? employee.position.id : "",
              status: employee ? employee.status.id : ""
            }}
            onSubmit={values => {
              values.birthday = moment(values.birthday).format(
                "YYYY-MM-DD HH:mm:ss"
              );
              values.endDate = moment(values.endDate).format(
                "YYYY-MM-DD HH:mm:ss"
              );
              values.beginDate = moment(values.beginDate).format(
                "YYYY-MM-DD HH:mm:ss"
              );

              let newPosition = positions.filter(p => values.position === p.id);
              let newStatus = statuses.filter(s => values.status === s.id);

              values.position = newPosition[0];
              values.status = newStatus[0];

              if (employee) {
                values.id = employee.id;
                axios
                  .put(
                    `${config.apiUrl}employees/${id}`,
                    values,
                    config.authHeader
                  )
                  .then(res => {
                    handleClose();
                  })
                  .catch(err => {
                    this.setState({ loading: false });
                    console.log("error");
                  });
              } else {
                console.log(values);
                axios
                  .post(`${config.apiUrl}employees`, values, config.authHeader)
                  .then(res => {
                    handleClose();
                  })
                  .catch(err => {
                    this.setState({ loading: false });
                    console.log("error");
                  });
              }
            }}
          >
            {({ errors, touched }) => (
              <Dialog
                open={open}
                aria-labelledby="form-dialog-title"
                maxWidth="lg"
              >
                <DialogContent>
                  <Form>
                    <div className={classes.wrapper}>
                      <div className={classes.container}>
                        <img
                          src="https://middle.pngfans.com/20190620/er/avatar-icon-png-computer-icons-avatar-clipart-e1c00a5950d1849e.jpg"
                          alt="imasda"
                          className={classes.img}
                        />
                        <InputLabel>Salary</InputLabel>
                        {errors.salary && touched.salary ? (
                          <div className={classes.errorMessage}>
                            {errors.salary}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="salary"
                          type="number"
                          autoComplete="off"
                          as={CustomInputComponent}
                        />

                        <Paper className={classes.root}>
                          <Table className={classes.table}>
                            <TableHead className={classes.tableHead}>
                              <TableRow>
                                <TableCell className={classes.tableCell}>
                                  Team
                                </TableCell>
                                <TableCell
                                  className={classes.tableCell}
                                  align="right"
                                >
                                  Role
                                </TableCell>
                              </TableRow>
                            </TableHead>
                            <TableBody>
                              {this.state.rows.map(row => (
                                <TableRow key={row.id}>
                                  <TableCell component="th" scope="row">
                                    {row.team}
                                  </TableCell>
                                  <TableCell align="right">
                                    {this.state.employee.position.name}
                                  </TableCell>
                                </TableRow>
                              ))}
                            </TableBody>
                          </Table>
                        </Paper>
                      </div>

                      <div className={classes.container}>
                        <InputLabel>First Name</InputLabel>
                        {errors.firstName && touched.firstName ? (
                          <div className={classes.errorMessage}>
                            {errors.firstName}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="firstName"
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Last Name</InputLabel>
                        {errors.lastName && touched.lastName ? (
                          <div className={classes.errorMessage}>
                            {errors.lastName}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="lastName"
                          as={CustomInputComponent}
                        />
                        <InputLabel>E-Mail</InputLabel>
                        {errors.email && touched.email ? (
                          <div className={classes.errorMessage}>
                            {errors.email}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="email"
                          type="email"
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Phone Number</InputLabel>
                        {errors.phone && touched.phone ? (
                          <div className={classes.errorMessage}>
                            {errors.phone}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="phone"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Birth Date</InputLabel>
                        {errors.birthday && touched.birthday ? (
                          <div className={classes.errorMessage}>
                            {errors.birthday}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          type="date"
                          name="birthday"
                          as={CustomInputComponent}
                        />
                      </div>
                      <div className={classes.container}>
                        <InputLabel>Employment Begin Date</InputLabel>
                        {errors.employmentBeginDate &&
                        touched.employmentBeginDate ? (
                          <div className={classes.errorMessage}>
                            {errors.employmentBeginDate}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="employmentBeginDate"
                          autoComplete="off"
                          type="date"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Status</InputLabel>
                        {errors.status && touched.status ? (
                          <div className={classes.errorMessage}>
                            {errors.status}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="status"
                          as={CustomStatusComponent}
                        />
                        <InputLabel>Job Title</InputLabel>
                        {errors.position && touched.position ? (
                          <div className={classes.errorMessage}>
                            {errors.position}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="position"
                          autoComplete="off"
                          as={CustomSelectComponent}
                        />
                        <InputLabel>Employement End Date</InputLabel>
                        {errors.employementEndDate &&
                        touched.employementEndDate ? (
                          <div className={classes.errorMessage}>
                            {errors.employementEndDate}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="employmentEndDate"
                          type="date"
                          as={CustomInputComponent}
                        />
                        <div className={classes.buttons}>
                          <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                          >
                            Submit
                          </Button>
                          <Button
                            variant="contained"
                            color="secondary"
                            onClick={() => {
                              handleClose();
                              this.setState({ employee: null, finish: false });
                            }}
                          >
                            Cancle
                          </Button>
                        </div>
                      </div>
                    </div>
                  </Form>
                </DialogContent>
              </Dialog>
            )}
          </Formik>
        ) : null}
      </React.Fragment>
    );
  }
}

export default withStyles(styles)(Inputs);
