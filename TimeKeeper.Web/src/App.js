import React from "react";
import { Switch, Route, withRouter } from "react-router-dom";
import { connect } from "react-redux";

import config from "./config";
import StaticPage from "./containers/StaticPage/StaticPage";
import TimeKeeper from "./containers/TimeKeeper/TimeKeeper";
import { PrivateRoute } from "./components/PrivateRoute";
import { Callback } from "./components/Callback";
import { Logout } from "./components/Logout";
import { LogoutCallback } from "./components/LogoutCallback";
import { SilentRenew } from "./components/SilentRenew";

class App extends React.Component {
	// componentDidMount() {
	// 	const { user } = this.props;

	// 	return !user.token ? this.props.history.push("/") : null;
	// }

	render() {
		return (
			<Switch>
				<Route exact={true} path="/auth-callback" component={Callback} />
				<Route exact={true} path="/logout" component={Logout} />
				<Route exact={true} path="/logout/callback" component={LogoutCallback} />
				<Route exact={true} path="/silentrenew" component={SilentRenew} />
				<PrivateRoute path="/app" component={TimeKeeper} />
				<Route exact path="/" component={StaticPage} />
			</Switch>
		);
	}
}

const mapStateToProps = (state) => {
	return { user: state.user };
};

export default connect(mapStateToProps)(withRouter(App));
