import React from "react";

import Navigation from "../../components/Navigation/Navigation";
import SideDrawer from "../../components/Navigation/SideDrawer/SideDrawer";
import AboutPage from "../../components/AboutSection/AboutSection";
import ServicesPage from "../../components/ServicesSection/ServicesSection";
import TeamPage from "../../components/TeamSection/TeamSection";
import Footer from "../../components/Footer/Footer.js";

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
                    <div id="contact" style={{ height: "91vh", backgroundColor: "blue" }} />
                </main>
                <footer>
                    <Footer />
                </footer>
            </React.Fragment>
        );
    }
}

export default StaticPage;
