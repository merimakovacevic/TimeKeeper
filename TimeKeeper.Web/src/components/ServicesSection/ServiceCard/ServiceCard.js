import React from "react";

import classes from "../ServiceCard/ServiceCard.module.css";

const serviceCard = props => (
  <div className={classes.Card}>
    <img
      src={props.serviceIcon}
      alt={props.serviceTitle}
      className={classes.CardImage}
    />
    <h2 className={classes.CardTitle}>{props.serviceTitle}</h2>
    <p className={classes.CardParagraph}>{props.serviceDescription}</p>
  </div>
);
export default serviceCard;
