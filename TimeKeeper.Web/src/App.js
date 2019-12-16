import React from "react";
import { connect } from "react-redux";
import { Switch, Route, withRouter } from "react-router-dom";

import StaticPage from "./containers/StaticPage/StaticPage";
import TimeKeeper from "./containers/TimeKeeper/TimeKeeper";
// import Callback from "./components/StaticPageComponents/Login/LoginCallback";

class App extends React.Component {
	componentDidMount() {
		this.handleLogin();
	}

	componentDidUpdate(prevProps) {
		if (prevProps.user !== this.props.user) {
			this.handleLogin();
		}
	}

	handleLogin = () => {
		const { user, history } = this.props;

		if (user) {
			history.push("/app");
		} else {
			history.push("/");
		}
	};

	render() {
		// console.log(this.props.user);
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
		user: state.user.user
	};
};

export default connect(mapStateToProps)(withRouter(App));
