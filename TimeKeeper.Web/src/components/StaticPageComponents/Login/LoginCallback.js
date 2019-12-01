import React from "react";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { withRouter } from "react-router-dom";
import { CircularProgress, Backdrop } from "@material-ui/core";

import userManager from "../../../utils/userManager";

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
				<Backdrop open={true}>
					<div
						style={{
							display: "flex",
							flexDirection: "column",
							justifyContent: "center",
							alignItems: "center"
						}}
					>
						<CircularProgress size={100} style={{ color: "white", textAlign: "center" }} />
						<h1 style={{ color: "white", marginTop: "1rem" }}>Preparing TimeKeeper Application</h1>
					</div>
				</Backdrop>
			</CallbackComponent>
		);
	}
}

export default connect()(withRouter(CallbackPage));
