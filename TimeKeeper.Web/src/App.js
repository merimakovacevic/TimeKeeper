import React from "react";
import { Switch, Route, withRouter } from "react-router-dom";
import { connect } from "react-redux";

import config from "./config";
import StaticPage from "./containers/StaticPage/StaticPage";
import TimeKeeper from "./containers/TimeKeeper/TimeKeeper";

class App extends React.Component {
	// componentDidMount() {
	// 	const { user } = this.props;

	// 	return !user.token ? this.props.history.push("/") : null;
	// }

	render() {
		return (
			<Switch>
				<Route exact path="/">
					<StaticPage />
				</Route>
				<Route path="/app">
					<TimeKeeper />
				</Route>
			</Switch>
		);
	}
}

const mapStateToProps = (state) => {
	return { user: state.user };
};

export default connect(mapStateToProps)(withRouter(App));
