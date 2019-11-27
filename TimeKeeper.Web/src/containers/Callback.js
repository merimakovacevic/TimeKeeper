import React from "react";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { withRouter } from "react-router-dom";
import userManager from "../utils/userManager";
import config from "../config";

class CallbackPage extends React.Component {
	render() {
		return (
			<CallbackComponent
				userManager={userManager}
				successCallback={(res) => {
					this.props.history.push("/app");
				}}
				errorCallback={(error) => {
					console.error(error);
				}}
			>
				<div>Redirecting...</div>
			</CallbackComponent>
		);
	}
}

export default connect()(withRouter(CallbackPage));
