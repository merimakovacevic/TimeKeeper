import React from "react";
import { Switch, Route, withRouter } from "react-router-dom";

import StaticPage from "./containers/StaticPage/StaticPage";
import TimeKeeper from "./containers/TimeKeeper/TimeKeeper";
import Callback from "./components/StaticPageComponents/Login/LoginCallback";

class App extends React.Component {
	render() {
		return (
			<Switch>
				<Route exact path="/auth-callback" component={Callback} />
				<Route path="/app" component={TimeKeeper} />
				<Route exact path="/" component={StaticPage} />
			</Switch>
		);
	}
}

export default withRouter(App);
