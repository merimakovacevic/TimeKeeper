import React from "react";
import axios from "axios";
import * as Yup from "yup";
import { Button } from "@material-ui/core";
import { Formik, Form, Field } from "formik";

import classes from "./ContactForm.module.css";

const ContactSchema = Yup.object().shape({
    email: Yup.string()
        .email("Invalid email")
        .required("E-mail is Required"),
    name: Yup.string()
        .min(2, "Too Short!")
        .max(50, "Too Long!")
        .required("Name isRequired"),
    message: Yup.string()
        .max(250, "Invalid email")
        .required("Message is Required")
});

const contactForm = () => (
    <Formik
        initialValues={{
            email: "",
            name: "",
            message: ""
        }}
        validationSchema={ContactSchema}
        onSubmit={values => {
            axios
                .post("http://192.168.60.73/timekeeper/api/contact", values)
                .then(res => console.log(res));
        }}
    >
        {({ errors, touched }) => (
            <div className={classes.Container}>
                <Form className={classes.Form}>
                    {errors.email && touched.email ? (
                        <div className={classes.ErrorMessage}>{errors.email}!</div>
                    ) : null}
                    <Field
                        name="email"
                        type="email"
                        placeholder="Your e-mail"
                        className={classes.Input}
                    />
                    {errors.name && touched.name ? (
                        <div className={classes.ErrorMessage}>{errors.name}!</div>
                    ) : null}
                    <Field name="name" placeholder="Your name" className={classes.Input} />
                    {errors.message && touched.message ? (
                        <div className={classes.ErrorMessage}>{errors.message}!</div>
                    ) : null}
                    <Field
                        placeholder="Your message"
                        name="message"
                        as="textarea"
                        cols="30"
                        rows="10"
                        className={classes.Textarea}
                    />
                    <Button variant="contained" color="primary" fullWidth type="submit">
                        Send
                    </Button>
                </Form>
            </div>
        )}
    </Formik>
);

export default contactForm;
