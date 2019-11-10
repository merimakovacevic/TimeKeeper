import React from "react";

import classes from "./NavigationItems.module.css";
import NavigationItem from "./NavigationItem/NavigationItem";

const navigationItems = props => (
    <ul className={classes.NavigationItems}>
        <NavigationItem click={props.clicked} link="#about">
            About
        </NavigationItem>
        <NavigationItem click={props.clicked} link="#services">
            Services
        </NavigationItem>
        <NavigationItem click={props.clicked} link="#team">
            Team
        </NavigationItem>
        <NavigationItem click={props.clicked} link="#contact">
            Contact
        </NavigationItem>
    </ul>
);

export default navigationItems;
