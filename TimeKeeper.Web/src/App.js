import React from "react";
import { connect } from "react-redux";
import { Switch, Route, withRouter } from "react-router-dom";

import { authCheckState } from "./store/actions/index";
import StaticPage from "./containers/StaticPage/StaticPage";
import TimeKeeper from "./containers/TimeKeeper/TimeKeeper";
// import Callback from "./components/StaticPageComponents/Login/LoginCallback";

class App extends React.Component {
	componentDidMount() {
		this.props.authCheckState();
		this.handleLogin();

		// console.log("DID MOUNT");
	}

	componentDidUpdate(prevProps) {
		if (prevProps.token !== this.props.token) {
			this.handleLogin();
		}
	}

	handleLogin = () => {
		const { token, history } = this.props;

		if (token) {
			history.push("/app/personal-report");
		} else {
			history.push("/");
		}
	};

	render() {
		return (
			<Switch>
				{/* <Route exact path="/auth-callback" component={Callback} /> */}
				<Route path="/app" component={TimeKeeper} />
				<Route exact path="/" component={StaticPage} />
			</Switch>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		token: state.user.token
	};
};

export default connect(mapStateToProps, { authCheckState })(withRouter(App));
