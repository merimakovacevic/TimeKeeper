import React from "react";

import Navigation from "../../components/Navigation/Navigation";
import SideDrawer from "../../components/Navigation/SideDrawer/SideDrawer";
import AboutPage from "../../components/AboutPage/AboutPage";
import ServicesPage from "../../components/ServicesPage/ServicesPage";
import TeamPage from "../../components/TeamPage/TeamPage";

class StaticPage extends React.Component {
  state = {
    showSideDrawer: false
    // screenWidth: document.body.offsetWidth
  };

  /* componentDidMount() {
        this.updateWindowDimensions();
        window.addEventListener("resize", this.updateWindowDimensions);
    }
    componentWillUnmount() {
        window.removeEventListener("resize", this.updateWindowDimensions);
    }
    updateWindowDimensions = () => {
        this.setState({ screenWidth: window.innerWidth });
    };*/

  sideDrawerClosedHandler = () => this.setState({ showSideDrawer: false });

  drawerToggleClicked = () =>
    this.setState(prevState => {
      return { showSideDrawer: !prevState.showSideDrawer };
    });

  render() {
    return (
      <React.Fragment>
        <Navigation ToggleButtonClicked={this.drawerToggleClicked} />
        <SideDrawer
          open={this.state.showSideDrawer}
          closed={this.sideDrawerClosedHandler}
        />
        <main>
          <AboutPage passedId={"about"} />
          <ServicesPage passedId={"services"} />
          <TeamPage passedId={"team"} />
          <div
            id="contact"
            style={{ height: "100vh", backgroundColor: "blue" }}
          />
        </main>
      </React.Fragment>
    );
  }
}

export default StaticPage;
