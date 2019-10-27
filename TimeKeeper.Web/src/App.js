import React from "react";

import StaticPage from "./containers/StaticPage/StaticPage";

class App extends React.Component {
  state = {};

  render() {
    return (
      <React.Fragment>
        <StaticPage />
      </React.Fragment>
    );
  }
}

export default App;
