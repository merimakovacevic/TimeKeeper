import React from "react";

import classes from "./NavigationItems.module.css";
import NavigationItem from "./NavigationItem/NavigationItem";

const navigationItems = props => (
    <ul className={classes.NavigationItems}>
        <NavigationItem id="aboutButton" click={props.clicked} link="#about">
            About
        </NavigationItem>
        <NavigationItem id="servicesButton" click={props.clicked} link="#services">
            Services
        </NavigationItem>
        <NavigationItem id="teamButton"  click={props.clicked} link="#team">
            Team
        </NavigationItem>
        <NavigationItem  id="contactButton" click={props.clicked} link="#contact">
            Contact
        </NavigationItem>
    </ul>
);

export default navigationItems;
