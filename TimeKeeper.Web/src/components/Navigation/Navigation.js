import React from "react";

import { Button } from "@material-ui/core";

import classes from "./Navigation.module.css";
import Logo from "../Logo/Logo";
import NavigationItems from "./NavigationItems/NavigationItems";
import ToggleButton from "./ToggleButton/ToggleButton";

const navigation = props => (
    <header className={classes.Navigation}>
        <Logo />
        <ToggleButton clicked={props.ToggleButtonClicked} />
        <nav className={classes.DesktopOnly}>
            <NavigationItems />
        </nav>
        <Button
            variant="contained"
            color="primary"
            className={classes.Button}
            onClick={props.clicked}
        >
            LOGIN
        </Button>
    </header>
);

export default navigation;
