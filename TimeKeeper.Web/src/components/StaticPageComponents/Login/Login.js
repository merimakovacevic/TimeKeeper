import React from "react";
import { connect } from "react-redux";
import { Formik, Form, Field } from "formik";
import { Button } from "@material-ui/core";
import * as Yup from "yup";

import CircularProgress from "@material-ui/core/CircularProgress";

import classes from "./Login.module.css";
import { auth } from "../../../store/actions/index";

const LoginSchema = Yup.object().shape({
	username: Yup.string()
		.min(6, "Username too short")
		.max(22, "Username too long")
		.required("Username can't be empty!"),
	password: Yup.string()
		.min(2, "Password too short!")
		.max(32, "Password too long!")
		.required("Password can't be empty!")
});
const login = (props) => {
	const { show, loading, auth } = props;

	return (
		<Formik
			initialValues={{
				username: "",
				password: ""
			}}
			validationSchema={LoginSchema}
			onSubmit={(values) => {
				auth(values);
			}}
		>
			{({ errors, touched }) =>
				show ? (
					<div className={classes.Container}>
						<Form className={classes.Form}>
							<h1>Login</h1>
							{errors.username && touched.username ? (
								<div className={classes.ErrorMessage}>{errors.username}</div>
							) : (
								<div className={classes.ErrorMessage}> &nbsp; </div>
							)}
							<Field
								id="usernameLogin"
								name="username"
								autoComplete="off"
								placeholder="Username"
								className={classes.Input}
							/>
							{errors.password && touched.password ? (
								<div className={classes.ErrorMessage}>{errors.password}</div>
							) : (
								<div className={classes.ErrorMessage}> &nbsp; </div>
							)}
							<Field
								id="passwordLogin"
								placeholder="Password"
								autoComplete="off"
								name="password"
								className={classes.Input}
								type="password"
							/>

							<Button
								id="staticLoginButton"
								variant="contained"
								color="primary"
								fullWidth
								type="submit"
								className={classes.Button}
								id="loginButton"
								disabled={loading ? true : false}
							>
								{loading ? (
									<CircularProgress
										color="secondary"
										size={24}
										thickness={4}
										style={{ color: "white" }}
									/>
								) : (
									"Log in"
								)}
							</Button>
						</Form>
					</div>
				) : null
			}
		</Formik>
	);
};

const mapStateToProps = (state) => {
	return {
		loading: state.user.loading
	};
};

export default connect(mapStateToProps, { auth })(login);
