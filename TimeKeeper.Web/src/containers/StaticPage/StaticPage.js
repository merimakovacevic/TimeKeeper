import React from "react";

import Navigation from "../../components/Navigation/Navigation";
import SideDrawer from "../../components/Navigation/SideDrawer/SideDrawer";
import AboutSection from "../../components/AboutSection/AboutSection";
import ServicesSection from "../../components/ServicesSection/ServicesSection";
import TeamSection from "../../components/TeamSection/TeamSection";
import ContactSection from "../../components/ContactSection/ContactSection";
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
                    <AboutSection passedId="about" />
                    <ServicesSection passedId="services" />
                    <TeamSection passedId="team" />
                    <ContactSection passedId="contact" />
                </main>
                <footer>
                    <Footer />
                </footer>
            </React.Fragment>
        );
    }
}

export default StaticPage;
