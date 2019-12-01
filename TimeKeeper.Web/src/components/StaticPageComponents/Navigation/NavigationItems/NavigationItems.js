import React from "react";

import classes from "./NavigationItems.module.css";
import NavigationItem from "./NavigationItem/NavigationItem";

const navigationItems = (props) => (
	<ul id="navBar" className={classes.NavigationItems}>
		<NavigationItem passedId="aboutButtonStatic" click={props.clicked} link="#about">
			About
		</NavigationItem>
		<NavigationItem passedId="servicesButtonStatic" click={props.clicked} link="#services">
			Services
		</NavigationItem>
		<NavigationItem passedId="teamButton" click={props.clicked} link="#team">
			Team
		</NavigationItem>
		<NavigationItem passedId="contactButton" click={props.clicked} link="#contact">
			Contact
		</NavigationItem>
	</ul>
);

export default navigationItems;
