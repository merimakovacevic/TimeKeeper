import React from "react";

import classes from "./NavigationItems.module.css";
import NavigationItem from "./NavigationItem/NavigationItem";

const navigationItems = props => (
  <ul id="navBar" className={classes.NavigationItems}>
    <li id="aboutButtonStatic">
      {" "}
      <NavigationItem click={props.clicked} link="#about">
        About
      </NavigationItem>
    </li>
    <li id="servicesButtonStatic">
      {" "}
      <NavigationItem
        id="servicesButton"
        click={props.clicked}
        link="#services"
      >
        Services
      </NavigationItem>
    </li>
    <li>
      <NavigationItem id="teamButton" click={props.clicked} link="#team">
        Team
      </NavigationItem>{" "}
    </li>
    <li>
      {" "}
      <NavigationItem id="contactButton" click={props.clicked} link="#contact">
        Contact
      </NavigationItem>{" "}
    </li>
  </ul>
);

export default navigationItems;
