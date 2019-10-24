import React from "react";

import classes from "./TeamPage.module.css";

import TeamCard from "./TeamCard/TeamCard";

const teamPage = props => (
    <div id={props.passedId} className={classes.TeamPage}>
        <h1>Team Page</h1>
        <p>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Non, quo doloribus delectus
            dolore expedita, repellendus dignissimos necessitatibus
        </p>
        <ul style={{ margin: "0 2rem", listStyleType: "none" }}>
            <li>
                {" "}
                <TeamCard />{" "}
            </li>
        </ul>
    </div>
);

export default teamPage;
