import React from "react";
import { Formik, Form, Field } from "formik";
import { Button } from "@material-ui/core";
import * as Yup from "yup";

import classes from "./Login.module.css";

const LoginSchema = Yup.object().shape({
    username: Yup.string()

        .min(6, "Username too short")
        .max(22, "Username too long")
        .required("Username can't be empty!"),
    password: Yup.string()
        .min(8, "Password too short!")
        .max(32, "Password too long!")
        .required("Password can't be empty!")
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
                            <h1>Login</h1>
                            {errors.username && touched.username ? (
                                <div className={classes.ErrorMessage}>{errors.username}</div>
                            ) : (
                                <div className={classes.ErrorMessage}> &nbsp; </div>
                            )}
                            <Field
                                name="username"
                                placeholder="Username"
                                className={classes.Input}
                            />
                            {errors.password && touched.password ? (
                                <div className={classes.ErrorMessage}>{errors.password}</div>
                            ) : (
                                <div className={classes.ErrorMessage}> &nbsp; </div>
                            )}
                            <Field
                                placeholder="Password"
                                name="password"
                                className={classes.Input}
                                type="password"
                            />

                            <Button
                                variant="contained"
                                color="primary"
                                fullWidth
                                type="submit"
                                className={classes.Button}
                            >
                                SIGN IN
                            </Button>
                        </Form>
                    </div>
                ) : null
            }
        </Formik>
    );
};
export default login;
