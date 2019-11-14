import React from "react";

import { Button } from "@material-ui/core";

import classes from "./SideDrawer.module.css";
import NavigationItems from "../NavigationItems/NavigationItems";

const sideDrawer = props => {
    let attachedClasses = [classes.SideDrawer, classes.Close];
    if (props.open) {
        attachedClasses = [classes.SideDrawer, classes.Open];
    }

    return (
        <React.Fragment>
            <div className={attachedClasses.join(" ")}>
                <nav>
                    <NavigationItems clicked={props.closed} />
                </nav>
                <Button
                    variant="contained"
                id="loginButtonStatic"
                    className={classes.Button}
                    onClick={props.clicked}
                >
                    LOGIN
                </Button>
            </div>
        </React.Fragment>
    );
};

export default sideDrawer;
