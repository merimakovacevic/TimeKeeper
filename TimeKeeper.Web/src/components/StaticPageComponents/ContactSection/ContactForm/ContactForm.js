import React from "react";
import axios from "axios";
import CircularProgress from "@material-ui/core/CircularProgress";
import { Button } from "@material-ui/core";
import { Formik, Form, Field } from "formik";
import * as Yup from "yup";

import classes from "./ContactForm.module.css";
import Smiley from "../../../../assets/svgIcons/thumbs-up.svg";
import SadSmiley from "../../../../assets/svgIcons/sad.svg";

const ContactSchema = Yup.object().shape({
    email: Yup.string()
        .min(3, "Email is too short")
        .max(254, "Email is too long")
        .email("Invalid email")
        .required("E-mail is required"),
    name: Yup.string()
        .min(3, "Too Short!")
        .max(20, "Too Long!")
        .required("Name is required"),
    phone: Yup.number()
        .typeError("Phone number can only contain numbers")
        .required("Phone is required")
        .min(100000000, "Too short")
        .max(999999999999999, "Too long"),
    message: Yup.string()
        .min(1, "Message is too short")
        .max(400, "Message is too long")
        .required("Message is required")
});

const contactForm = props => (
    <Formik
        initialValues={{
            email: "",
            name: "",
            phone: "",
            message: ""
        }}
        validationSchema={ContactSchema}
        onSubmit={values => {
            props.sendStart();
            axios
                .post("http://192.168.60.73/TimeKeeper/api/contact", values)
                .then(res => props.successfullSend())
                .catch(err => props.failedSend());
        }}
    >
        {({ errors, touched }) => (
            <div className={classes.Container}>
                {props.sendSuccess ? (
                    <div className={classes.ResponseContainer}>
                        <img src={Smiley} alt="smiley" className={classes.ResponseImg} />
                        <h2>Thank you for your feedback!</h2>
                    </div>
                ) : props.sendFail ? (
                    <div className={classes.ResponseContainer}>
                        <img src={SadSmiley} alt="error" className={classes.ResponseImg} />
                        <h2>Unable to perform your request!</h2>
                    </div>
                ) : (
                    <Form className={classes.Form}>
                        {errors.email && touched.email ? (
                            <div className={classes.ErrorMessage}>{errors.email}!</div>
                        ) : (
                            <div className={classes.ErrorMessage}>&nbsp;</div>
                        )}
                        <Field
                            id="emailInputStatic"
                            name="email"
                            type="email"
                            placeholder="Your e-mail"
                            className={classes.Input}
                        />
                        {errors.name && touched.name ? (
                            <div className={classes.ErrorMessage}>{errors.name}!</div>
                        ) : (
                            <div className={classes.ErrorMessage}>&nbsp;</div>
                        )}
                        <Field id="nameInputStatic" name="name" placeholder="Your name" className={classes.Input} />

                        {errors.phone && touched.phone ? (
                            <div className={classes.ErrorMessage}>{errors.phone}!</div>
                        ) : (
                            <div className={classes.ErrorMessage}>&nbsp;</div>
                        )}
                        <Field id="phoneInput" name="phone" placeholder="Your phone" className={classes.Input} />

                        {errors.message && touched.message ? (
                            <div className={classes.ErrorMessage}>{errors.message}!</div>
                        ) : (
                            <div className={classes.ErrorMessage}>&nbsp;</div>
                        )}
                        <Field
                            placeholder="Your message"
                            name="message"
                            as="textarea"
                            cols="30"
                            rows="10"
                            id="messageInputStatic"
                            className={classes.Textarea}
                        />
                        <Button
                        id="buttonSendStatic"
                            variant="contained"
                            color="primary"
                            fullWidth
                            type="submit"
                            className={classes.MyButton}
                        >
                            {props.sending ? (
                                <CircularProgress
                                    color="secondary"
                                    size={24}
                                    thickness={4}
                                    style={{ color: "white" }}
                                />
                            ) : (
                                "Send"
                            )}
                        </Button>
                    </Form>
                )}
            </div>
        )}
    </Formik>
);

export default contactForm;
