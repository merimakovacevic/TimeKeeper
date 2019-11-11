import React from "react";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import Input from "@material-ui/core/Input";

import axios from "axios";
import config from "../../../config";

const styles = theme => ({
    wrapper: {
        display: "flex",
        width: "100%",
        height: "100%",
        padding: "2rem"
    },
    container: {
        width: "100%",
        textAlign: "center",
        display: "flex",
        flexDirection: "column",
        justifyContent: "space-between"
    },
    input: {
        margin: theme.spacing()
    }
});

class Inputs extends React.Component {
    state = {
        salary: undefined,
        firstName: undefined,
        lastName: undefined,
        email: undefined,
        phoneNumber: undefined,
        birthDate: undefined,
        EBD: undefined,
        EED: undefined,
        status: undefined,
        jobTitle: undefined,
        fetchedData: null
    };

    componentDidMount() {
        axios(`${config.apiUrl}employees/${this.props.id}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: config.token
            }
        }).then(res =>
            this.setState({
                fetchedData: true,
                firstName: res.data.firstName,
                salary: res.data.salary,
                email: res.data.email,
                lastName: res.data.lastName,
                birthDate: res.data.birthdate,
                EDB: res.data.beginDate,
                EED: res.data.endDate,
                phoneNumber: res.data.phone
            })
        );
    }

    inputHandler = event => {
        this.setState({ [event.target.name]: event.target.value });
    };

    render() {
        const { inputHandler } = this;
        const {
            salary,
            firstName,
            lastName,
            email,
            phoneNumber,
            birthDate,
            EDB,
            EED,
            status,
            jobTitle
        } = this.state;
        const { classes } = this.props;
        return (
            <div>
                {this.state.fetchedData ? (
                    <div className={classes.wrapper}>
                        <div className={classes.container}>
                            <Input
                                onChange={inputHandler}
                                name="salary"
                                placeholder="Salary"
                                className={classes.input}
                                value={salary}
                                inputProps={{
                                    "aria-label": "Contracted Salary"
                                }}
                            />
                        </div>
                        <div className={classes.container}>
                            <Input
                                onChange={inputHandler}
                                name="firstName"
                                value={firstName}
                                placeholder="First name"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "First name"
                                }}
                            />
                            <Input
                                onChange={inputHandler}
                                name="lastName"
                                value={lastName}
                                placeholder="Last Name"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "Last name"
                                }}
                            />
                            <Input
                                onChange={inputHandler}
                                name="email"
                                value={email}
                                placeholder="E-mail"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "E-Mail"
                                }}
                            />
                            <Input
                                onChange={inputHandler}
                                name="phoneNumber"
                                value={phoneNumber}
                                placeholder="Phone Number"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "Phone Number"
                                }}
                            />
                            <Input
                                onChange={inputHandler}
                                name="birthDate"
                                value={birthDate}
                                placeholder="Birth Date"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "Birth Date"
                                }}
                            />
                        </div>

                        <div className={classes.container}>
                            <Input
                                onChange={inputHandler}
                                name="EBD"
                                value={EDB}
                                placeholder="Employment Begin Date"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "Employment Begin Date"
                                }}
                            />
                            <Input
                                onChange={inputHandler}
                                name="status"
                                value={status}
                                placeholder="Status"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "Status"
                                }}
                            />
                            <Input
                                onChange={inputHandler}
                                name="jobTitle"
                                value={jobTitle}
                                placeholder="Job Title"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "Job Title"
                                }}
                            />
                            <Input
                                onChange={inputHandler}
                                name="EED"
                                value={EED}
                                placeholder="Employment End Date"
                                className={classes.input}
                                inputProps={{
                                    "aria-label": "Employment End Date"
                                }}
                            />
                        </div>
                    </div>
                ) : null}
            </div>
        );
    }
}

export default withStyles(styles)(Inputs);
