import React from "react";

import Backdrop from "../../components/StaticPageComponents/UI/Backdrop/Backdrop";
import Navigation from "../../components/StaticPageComponents/Navigation/Navigation";
import SideDrawer from "../../components/StaticPageComponents/Navigation/SideDrawer/SideDrawer";
import AboutSection from "../../components/StaticPageComponents/AboutSection/AboutSection";
import ServicesSection from "../../components/StaticPageComponents/ServicesSection/ServicesSection";
import TeamSection from "../../components/StaticPageComponents/TeamSection/TeamSection";
import ContactSection from "../../components/StaticPageComponents/ContactSection/ContactSection";
import Footer from "../../components/StaticPageComponents/Footer/Footer.js";
import Login from "../../components/StaticPageComponents/Login/Login";

class StaticPage extends React.Component {
    state = {
        showSideDrawer: false,
        modalOpen: false,
        loading: false,
        isLoggedIn: false,
        logInError: null,
        sending: false,
        sendSuccess: null,
        sendFail: null
    };

    loginHandler = value => {
        this.setState({ isLoggedIn: value });
    };
    loginLoadingHandler = value => this.setState({ loading: value });

    successfullSend = () => this.setState({ sendSuccess: true, sending: false });
    failedSend = () => this.setState({ sendFail: true, sending: false });
    sendStart = () => {
        this.setState({ sending: true });
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
        const {
            showSideDrawer,
            modalOpen,
            sendSuccess,
            sending,
            sendFail,
            loading,
            isLoggedIn,
            logInError
        } = this.state;
        const {
            sideDrawerClosedHandler,
            drawerToggleClicked,
            toggleBackdrop,
            loginHandler,
            sendStart,
            failedSend,
            successfullSend,
            loginLoadingHandler
        } = this;

        return (
            <React.Fragment>
                <Backdrop show={modalOpen} clicked={toggleBackdrop}></Backdrop>
                <Login
                    loginHandler={loginHandler}
                    loginLoadingHandler={loginLoadingHandler}
                    show={modalOpen}
                    loading={loading}
                    isLoggedIn={isLoggedIn}
                    logInError={logInError}
                />
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
                    <ContactSection
                        passedId="contact"
                        sending={sending}
                        sendSuccess={sendSuccess}
                        sendFail={sendFail}
                        sendStart={sendStart}
                        failedSend={failedSend}
                        successfullSend={successfullSend}
                    />
                </main>
                <footer>
                    <Footer />
                </footer>
            </React.Fragment>
        );
    }
}

export default StaticPage;
