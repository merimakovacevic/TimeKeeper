import React from "react";
import { Switch, Route, withRouter } from "react-router-dom";

import config from './config'
import StaticPage from "./containers/StaticPage/StaticPage";
import TimeKeeper from "./containers/TimeKeeper/TimeKeeper";

// import TeamsPage from './containers/TimeKeeper/TeamsPage/TeamsPage'
class App extends React.Component {
  state = {};

  // componentDidMount() {
  //   console.log(config.token);
  //   return config.token === "" ? this.props.history.push("/") : null;
  // }

  render() {
    return (
      <Switch>
        <Route exact path="/">
          {/* <TeamsPage/> */}
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
