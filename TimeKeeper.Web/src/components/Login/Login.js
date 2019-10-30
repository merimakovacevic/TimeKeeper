import React from "react";
import * as Yup from "yup";
import { Formik, Form, Field } from "formik";
import { Button } from "@material-ui/core";
import { ButtonGroup } from "@material-ui/core";
import SaveIcon from "@material-ui/icons/Save";

import classes from "./Login.module.css";

const LoginSchema = Yup.object().shape({
    username: Yup.string()
        //  .username("Invalid username")
        .min(6, "Username too short")
        .max(22, "Username too long")
        .required("Username can't be empty"),
    password: Yup.string()
        .min(8, "Password too short!")
        .max(32, "Password too long!")
        .required("Password can't be empty")
});
const login = props => {
    const { isLoggedIn } = props;

    let onSubmit = function() {
        props.successfulLogin(true);
    };
    console.log(isLoggedIn);

    return (
        <Formik
            initialValues={{
                username: "",
                password: ""
            }}
            validationSchema={LoginSchema}
            onSubmit={values => onSubmit()}
        >
            {({ errors, touched }) =>
                props.show ? (
                    <div className={classes.Container}>
                        <Form className={classes.Form}>
                            <Field
                                name="username"
                                placeholder="Username"
                                className={classes.Input}
                            />
                            {errors.username && touched.username ? (
                                <div>{errors.username}</div>
                            ) : null}
                            <Field
                                placeholder="Password"
                                name="password"
                                className={classes.Input}
                                type="password"
                            />
                            {errors.password && touched.password ? (
                                <div>{errors.password}</div>
                            ) : null}
                            <Button variant="contained" color="primary" fullWidth type="submit">
                                Send
                            </Button>
                            {props.isLoggedIn ? <div>dsadsadas</div> : null}
                        </Form>
                    </div>
                ) : null
            }
        </Formik>
    );
};
export default login;
