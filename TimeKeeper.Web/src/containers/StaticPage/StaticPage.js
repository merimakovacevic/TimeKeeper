import React from "react";
// import axios from "axios";

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
        isLoggedIn: false,
        sending: false,
        sendSuccess: null,
        sendFail: null
    };

    // componentDidMount() {
    //     axios("http://192.168.60.73/TimeKeeper/api/members").then(res => console.log(res));
    // }

    successfullLogin = value => {
        this.setState({ isLoggedIn: value });
    };

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
            isLoggedIn,
            sendSuccess,
            sending,
            sendFail
        } = this.state;
        const {
            sideDrawerClosedHandler,
            drawerToggleClicked,
            toggleBackdrop,
            successfullLogin,
            sendStart,
            failedSend,
            successfullSend
        } = this;

        return (
            <React.Fragment>
                <Backdrop show={modalOpen} clicked={toggleBackdrop}></Backdrop>
                <Login
                    isLoggedIn={isLoggedIn}
                    successfulLogin={successfullLogin}
                    show={modalOpen}
                    sending={this.sending}
                    sendSuccess={this.sendSuccess}
                    sendStart={this.sendStart}
                    failedSend={this.failedSend}
                    successfullSend={this.successfullSend}
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
