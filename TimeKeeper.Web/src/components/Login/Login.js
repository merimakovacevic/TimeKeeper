import React from "react";

import { Button } from "@material-ui/core";
import classes from "./Login.module.css";

const login = props =>
    props.show ? (
        <div className={classes.Container}>
            <form action="" method="" className={classes.Form}>
                <h1>Login</h1>
                <input
                    placeholder="Your username"
                    type="text"
                    tabIndex="1"
                    className={classes.Input}
                />
                <input
                    placeholder="Your password"
                    type="password"
                    tabIndex="2"
                    className={classes.Input}
                />
                <Button variant="contained" color="primary" fullWidth className={classes.Button}>
                    LOGIN
                </Button>
            </form>
        </div>
    ) : null;

export default login;
