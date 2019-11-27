import React from "react";
import axios from "axios";

import { Button } from "@material-ui/core";

import classes from "./Navigation.module.css";
import Logo from "../Logo/Logo";
import NavigationItems from "./NavigationItems/NavigationItems";
import ToggleButton from "./ToggleButton/ToggleButton";
import AuthService from "../../AuthService";

const navigation = (props) => (
	<header className={classes.Navigation}>
		<Logo />
		<ToggleButton clicked={props.ToggleButtonClicked} />
		<nav className={classes.DesktopOnly}>
			<NavigationItems />
		</nav>
		<Button
			id="toggleButtonStatus"
			variant="contained"
			color="primary"
			className={classes.Button}
			// onClick={props.clicked}
			onClick={() => {
				let authService = new AuthService();
				authService.signinRedirect();
			}}
		>
			LOGIN
		</Button>
	</header>
);

export default navigation;
