import React from "react";

import classes from "./NavigationItem.module.css";

const navigationItem = (props) => (
	<li id={props.passedId} onClick={props.click} className={classes.NavigationItem}>
		<a href={props.link}>{props.children}</a>
	</li>
);

export default navigationItem;
