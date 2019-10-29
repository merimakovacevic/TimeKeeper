import React from "react";
import * as Yup from "yup";
import { Formik, Form, Field } from "formik";
import { Button } from "@material-ui/core";
import classes from "./Login.module.css";

const LoginSchema = Yup.object().shape({
    username: Yup.string()
        //  .username("Invalid username")
        .min(6, "Username too short")
        .max(22, "Username too long")
        .required("Required"),
    password: Yup.string()
        .min(8, "Too Short!")
        .max(32, "Too Long!")
        .required("Required")
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
                        {errors.username && touched.username ? <div>{errors.username}</div> : null}
                        <Field
                            placeholder="Password"
                            name="password"
                            className={classes.Input}
                            type="password"
                        />
                        {errors.password && touched.password ? <div>{errors.password}</div> : null}
                        <Button variant="contained" color="primary" fullWidth type="submit">
                            Send
                        </Button>
                    </Form>
                </div>
            ) : null
        }
    </Formik>
);
export default login;

/* 
       {({ errors, touched }) => (
           <div className={classes.Container}>
               <Form className={classes.Form}>
                   <Field
                       name="email"
                       type="email"
                       placeholder="Your e-mail"
                       className={classes.Input}
                   />
                   {errors.email && touched.email ? <div>{errors.email}</div> : null}
                   <Field name="name" placeholder="Your name" className={classes.Input} />
                   {errors.name && touched.name ? <div>{errors.name}</div> : null}
                   <Field
                       placeholder="Your message"
                       name="message"
                       as="textarea"
                       cols="30"
                       rows="10"
                       className={classes.Textarea}
                   />
                   {errors.message && touched.message ? <div>{errors.message}</div> : null}
                   <Button variant="contained" color="primary" fullWidth type="submit">
                       Send
                   </Button>
               </Form>
           </div>
       )}
   </Formik>
);
export default contactForm; */
