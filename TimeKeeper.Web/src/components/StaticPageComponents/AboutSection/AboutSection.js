import React from "react";

import classes from "./AboutSection.module.css";

import arrowDown from "../../../assets/svgIcons/down-arrow.svg";

const aboutSection = props => (
    <div id={props.passedId} className={classes.Background}>
        <h1 className={classes.HeadText}>
            Time tracking. <br />
            Reports. <br />
            Made easy.
        </h1>
        <a href="#services" className={classes.DownArrow}>
            {" "}
            <img src={arrowDown} alt="arrow" />{" "}
        </a>
    </div>
);

export default aboutSection;
