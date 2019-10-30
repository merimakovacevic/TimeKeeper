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
const login = props => (
    <Formik
        initialValues={{
            username: "",
            password: ""
        }}
        validationSchema={LoginSchema}
    >
        {({ errors, touched }) =>
            props.show ? (
                <div className={classes.Container}>
                    <Form className={classes.Form}>
                        <Field name="username" placeholder="Username" className={classes.Input} />
                        {errors.username && touched.username ? (
                            <div className={classes.ErrorMessage}>{errors.username}</div>
                        ) : null}
                        <Field
                            placeholder="Password"
                            name="password"
                            className={classes.Input}
                            type="password"
                        />
                        {errors.password && touched.password ? (
                            <div className={classes.ErrorMessage}>{errors.password}</div>
                        ) : null}
                        <ButtonGroup fullWidth>
                            <Button variant="contained" color="primary" type="submit">
                                Login
                            </Button>
                        </ButtonGroup>
                    </Form>
                </div>
            ) : null
        }
    </Formik>
);
export default login;
