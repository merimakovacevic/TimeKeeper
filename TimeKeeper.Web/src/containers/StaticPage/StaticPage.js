import React from "react";

import Backdrop from "../../components/UI/Backdrop/Backdrop";
import Navigation from "../../components/Navigation/Navigation";
import SideDrawer from "../../components/Navigation/SideDrawer/SideDrawer";
import AboutSection from "../../components/AboutSection/AboutSection";
import ServicesSection from "../../components/ServicesSection/ServicesSection";
import TeamSection from "../../components/TeamSection/TeamSection";
import ContactSection from "../../components/ContactSection/ContactSection";
import Footer from "../../components/Footer/Footer.js";
import Login from "../../components/Login/Login";

class StaticPage extends React.Component {
    state = {
        showSideDrawer: false,
        modalOpen: false
    };

    sideDrawerClosedHandler = () => this.setState({ showSideDrawer: false });

    drawerToggleClicked = () =>
        this.setState(prevState => {
            return { showSideDrawer: !prevState.showSideDrawer };
        });

    toggleBackdrop = () => {
        this.setState(prevState => {
            return { modalOpen: !prevState.modalOpen, showSideDrawer: false };
        });
    };

    render() {
        const { showSideDrawer, modalOpen } = this.state;
        const { sideDrawerClosedHandler, drawerToggleClicked, toggleBackdrop } = this;

        return (
            <React.Fragment>
                <Backdrop show={modalOpen} clicked={toggleBackdrop}></Backdrop>
                <Login show={modalOpen} />
                <Navigation ToggleButtonClicked={drawerToggleClicked} clicked={toggleBackdrop} />
                <SideDrawer
                    open={showSideDrawer}
                    closed={sideDrawerClosedHandler}
                    clicked={toggleBackdrop}
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
