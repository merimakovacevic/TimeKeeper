import React from "react";

import classes from "./AboutPage.module.css";

const aboutPage = props => (
    <div id={props.passedId} className={classes.Background}>
        <h1 className={classes.HeadText}>
            Time tracking. <br />
            Reports. <br />
            Made easy.
        </h1>
    </div>
);

export default aboutPage;
