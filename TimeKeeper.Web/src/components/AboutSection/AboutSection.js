import arrowDown from "../../assets/down-arrow.svg";

import React from "react";

import classes from "./AboutSection.module.css";

const aboutSection = props => (
    <div id={props.passedId} className={classes.Background}>
        <h1 className={classes.HeadText}>
            Time tracking. <br />
            Reports. <br />
            Made easy.
        </h1>
        <div className={classes.DownArrow}>
            {" "}
            <img src={arrowDown} />{" "}
        </div>
    </div>
);

export default aboutSection;
