import React from "react";
// import axios from "axios";

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
        modalOpen: false,
        isLoggedIn: false
    };

    // componentDidMount() {
    //     axios("http://192.168.60.73/TimeKeeper/api/members").then(res => console.log(res));
    // }

    successfulLogin = value => {
        this.setState({ isLoggedIn: value });

        // console.log(value);
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
        const { showSideDrawer, modalOpen, isLoggedIn } = this.state;
        const {
            sideDrawerClosedHandler,
            drawerToggleClicked,
            toggleBackdrop,
            successfulLogin
        } = this;

        return (
            <React.Fragment>
                <Backdrop show={modalOpen} clicked={toggleBackdrop}></Backdrop>
                <Login isLoggedIn={isLoggedIn} successfulLogin={successfulLogin} show={modalOpen} />
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
