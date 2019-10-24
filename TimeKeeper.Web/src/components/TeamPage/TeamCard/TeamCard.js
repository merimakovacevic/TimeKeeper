import React from "react";

import classes from "./TeamCard.module.css";
import "./TeamCard.module.css";

import LnIcon from "../../../assets/servicesSVGIcons/linkedin.svg";
import GitIcon from "../../../assets/servicesSVGIcons/github-sign.svg";
import FbIcon from "../../../assets/servicesSVGIcons/facebook.svg";

const teamCard = props => (
    <div className={classes.TeamCard}>
        <img src={props.picture} alt="" className={classes.Img} />
        <h2 className={classes.Name}>Ime i Prezime</h2>
        <p className={classes.Role}>Lorem</p>

        <p className={classes.About}>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Unde eos aliquid adipisci eius
            nemo libero vitae quibusdam recusandae.
        </p>

        <hr />

        <div className={classes.ScIcons}>
            <a href={props.link}>
                <img src={LnIcon} alt="" className={classes.Icon} />
            </a>
            <a href={props.link}>
                <img src={GitIcon} alt="" className={classes.Icon} />
            </a>
            <a href={props.link}>
                <img src={FbIcon} alt="" className={classes.Icon} />
            </a>
        </div>
    </div>
);

export default teamCard;
