import React from "react";

import classes from "./FooterSection.module.css";

const footerSeciton = props => (
    <div id={props.passedId} className={classes.Background}>
        <div className={classes.Footer}>
            <div className={classes.Paragraph}>
                <p1> Copyright (c) by Gigi School of Coding 2019. All rights reserved </p1>
            </div>
        </div>
    </div>
);

export default footerSeciton;
