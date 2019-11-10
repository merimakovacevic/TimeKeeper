import React from "react";
import { Switch, Route, withRouter } from "react-router-dom";

import StaticPage from "./containers/StaticPage/StaticPage";
import TimeKeeper from "./containers/TimeKeeper/TimeKeeper";

import config from "./config";
class App extends React.Component {
    state = {};

    componentDidMount() {
        return config.token === "" ? this.props.history.push("/") : null;
    }

    render() {
        console.log(config.token);
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

export default withRouter(App);
