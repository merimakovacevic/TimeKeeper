import React from "react";

import classes from "./ContactSection.module.css";
import mailIcon from "../../../assets/svgIcons/mail.svg";
import ContactForm from "./ContactForm/ContactForm";

const contactSection = props => (
    <section id={props.passedId} className={classes.ContactSection}>
        <span className={classes.ClipPath} />
        <div className={classes.ContactInformation}>
            <h1>Contact Us</h1>
            <p>
                If you have any questions about our product feel free to e-mail us via our contact
                form and we'll get back to you as soon as we can.
            </p>
        </div>

        <div className={classes.Wrapper}>
            <img src={mailIcon} className={classes.Icon} alt="mail" />

            <ContactForm
                sending={props.sending}
                sendSuccess={props.sendSuccess}
                sendStart={props.sendStart}
                failedSend={props.failedSend}
                successfullSend={props.successfullSend}
                sendFail={props.sendFail}
            />
        </div>
    </section>
);

export default contactSection;
