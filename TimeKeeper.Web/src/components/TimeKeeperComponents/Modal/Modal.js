import React from "react";

import axios from "axios";
import config from "../../../config";

import classes from "./Modal.module.css";
import Form from "../../../containers/TimeKeeper/Form/Form";

class modal extends React.Component {
    render() {
        return this.props.show ? (
            <div className={classes.Wrapper}>
                <Form id={this.props.id} />
            </div>
        ) : null;
    }
}

export default modal;
