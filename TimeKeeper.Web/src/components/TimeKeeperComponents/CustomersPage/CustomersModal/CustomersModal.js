/* import React from "react";
import { withStyles } from "@material-ui/core/styles";
import { Formik, Field, Form } from "formik";
import * as Yup from "yup";

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

import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";

const styles = (theme) => ({
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

const statuses = [
  { id: 1, name: "Prospect" },
  { id: 2, name: "Client" }
];

const Schema = Yup.object().shape({
  name: Yup.string().required("Bussines Name can't be empty!"),
  contactName: Yup.string().required("Contact Name can't be empty!"),
  homeAddress: Yup.string().required("Home Address can't be empty!"),
  emailAddress: Yup.string().required("Email can't be empty!"),
  city: Yup.string().required("City can't be empty!"),
  phone: Yup.string().required("Phone Number can't be empty!"),
  status: Yup.string().required("Status can't be empty!")
});

class Inputs extends React.Component {
  state = { customer: null, finish: false, rows: [] };

  componentDidMount() {
    this.fetchCustomers(this.props.id);
  }

  fetchCustomers = (id) => {
    if (id === 666) {
      this.setState({ finish: true });
    } else {
      axios(`${config.apiUrl}customers/${id}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: config.token
        }
      })
        .then((res) => {
          console.log(res.data);
          this.setState({
            customer: res.data,
            rows: res.data.projects,
            finish: true
          });
        })
        .catch(() => this.setState({ finish: true }));
    }
  };

  render() {
    const CustomInputComponent = (props) => (
      <Input
        disabled={this.props.readOnly}
        fullWidth={true}
        className={classes.input}
        {...props}
      />
    );

    const CustomStatusComponent = (props) => {
      return (
        <Select
          fullWidth
          {...props}
          className={classes.input}
          disabled={this.props.readOnly}
        >
          <MenuItem value={1}>Prospect</MenuItem>
          <MenuItem value={2}>Client</MenuItem>
        </Select>
      );
    };

    const { classes, open, handleClose, id } = this.props;
    const { customer, finish } = this.state;

    return (
      <React.Fragment>
        {finish ? (
          <Formik
            validationSchema={Schema}
            initialValues={{
              name: customer ? customer.name : "",
              contactName: customer ? customer.contactName : "",
              homeAddress: customer ? customer.homeAddress.street : "",
              emailAddress: customer ? customer.emailAddress : "",
              status: customer ? customer.status.id : "",
              city: customer ? customer.homeAddress.city : ""
            }}
            onSubmit={(values) => {
              let newStatus = statuses.filter((s) => values.status === s.id);
              values.status = newStatus[0];

              let address = {
                street: values.homeAddress,
                city: values.city
              };
              values.homeAddress = address;
              delete values.city;

              if (customer) {
                values.id = customer.id;
                console.log(values);
                axios
                  .put(
                    `${config.apiUrl}customers/${id}`,
                    values,
                    config.authHeader
                  )
                  .then((res) => {
                    handleClose();
                    console.log(res);
                  })
                  .catch((err) => {
                    this.setState({ loading: false });
                    console.log("error");
                  });
              } else {
                console.log(values);
                axios
                  .post(`${config.apiUrl}customers`, values, config.authHeader)
                  .then((res) => {
                    handleClose();
                  })
                  .catch((err) => {
                    this.setState({ loading: false });
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
                          src="https:middle.pngfans.com/20190620/er/avatar-icon-png-computer-icons-avatar-clipart-e1c00a5950d1849e.jpg"
                          alt="imasda"
                          className={classes.img}
                        />

                        <Paper className={classes.root}>
                          <Table className={classes.table}>
                            <TableHead className={classes.tableHead}>
                              <TableRow>
                                <TableCell className={classes.tableCell}>
                                  Projects
                                </TableCell>
                                <TableCell
                                  className={classes.tableCell}
                                  align="right"
                                >
                                  Team
                                </TableCell>
                              </TableRow>
                            </TableHead>
                            <TableBody>
                              {this.state.rows.map((row) => (
                                <TableRow key={row.id}>
                                  <TableCell component="th" scope="row">
                                    {row.name}
                                  </TableCell>
                                  <TableCell align="right">Charlie</TableCell>
                                </TableRow>
                              ))}
                            </TableBody>
                          </Table>
                        </Paper>
                      </div>

                      <div className={classes.container}>
                        <InputLabel>Bussiness Name</InputLabel>
                        {errors.name && touched.name ? (
                          <div className={classes.errorMessage}>
                            {errors.name}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="name"
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                        <InputLabel>Contact Name</InputLabel>
                        {errors.contactName && touched.contactName ? (
                          <div className={classes.errorMessage}>
                            {errors.contactName}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="contactName"
                          as={CustomInputComponent}
                        />
                        <InputLabel>E-Mail</InputLabel>
                        {errors.emailAddress && touched.emailAddress ? (
                          <div className={classes.errorMessage}>
                            {errors.emailAddress}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          name="emailAddress"
                          type="email"
                          autoComplete="off"
                          as={CustomInputComponent}
                        />
                      </div>
                      <div className={classes.container}>
                        <InputLabel>Home Address</InputLabel>
                        {errors.homeAddress && touched.homeAddress ? (
                          <div className={classes.errorMessage}>
                            {errors.homeAddress}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="homeAddress"
                          as={CustomInputComponent}
                        />
                        <InputLabel>City</InputLabel>
                        {errors.city && touched.city ? (
                          <div className={classes.errorMessage}>
                            {errors.city}
                          </div>
                        ) : (
                          <div className={classes.errorMessage}> &nbsp; </div>
                        )}
                        <Field
                          autoComplete="off"
                          name="city"
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
                              this.setState({ customer: null, finish: false });
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
 */
