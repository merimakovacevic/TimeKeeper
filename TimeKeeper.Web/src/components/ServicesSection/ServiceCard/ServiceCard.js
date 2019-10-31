import React from "react";

import classes from "../ServiceCard/ServiceCard.module.css";

const serviceCard = props => (
    <div className={classes.Card}>
        <div className={classes.ClipPath}></div>
        <div className={classes.Wrapper}>
            <img src={props.serviceIcon} alt={props.serviceTitle} className={classes.CardImage} />
            <h2 className={classes.CardTitle}>{props.serviceTitle}</h2>
            <p className={classes.CardParagraph}>{props.serviceDescription}</p>
        </div>
    </div>
);
export default serviceCard;
