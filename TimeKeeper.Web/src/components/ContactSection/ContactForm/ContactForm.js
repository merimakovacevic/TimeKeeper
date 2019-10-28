import React from "react";
import { Button } from "@material-ui/core";

import classes from "./ContactForm.module.css";

const contactForm = () => (
    <div className={classes.Container}>
        <form action="" method="">
            <input placeholder="Your e-mail" type="email" tabIndex="1" />
            <input placeholder="Your name" type="text" tabIndex="2" />
            <textarea placeholder="Your message" name="message" cols="30" rows="10"></textarea>
            <Button variant="contained" color="primary" fullWidth>
                Send
            </Button>
        </form>
    </div>
);

export default contactForm;
