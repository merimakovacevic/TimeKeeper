import React from "react";

import classes from "./ContactSection.module.css";
//import messageIcon from "../../assets/message.svg";
import ContactForm from "./ContactForm/ContactForm";

const contactSection = props => (
    <section id={props.passedId} className={classes.ContactSection}>
        <div className={classes.ContactInformation}>
            <h1>Contact Us</h1>
            <p>
                If you have any questions about our product feel free to e-mail us via our contact
                form and we'll get back to you as soon as we can.
            </p>
            {/* <img src={messageIcon} alt="svgIcon" className={classes.Icon}></img> */}
        </div>

        <ContactForm
            sending={props.sending}
            sendSuccess={props.sendSuccess}
            sendStart={props.sendStart}
            failedSend={props.failedSend}
            successfullSend={props.successfullSend}
        />
    </section>
);

export default contactSection;
