import React from "react";

import "./TeamCard.module.css";
import classes from "./TeamCard.module.css";

import LnIcon from "../../../../assets/socialMediaISvgIcons/linkedin.svg";
import GitIcon from "../../../../assets/socialMediaISvgIcons/github-sign.svg";
import FbIcon from "../../../../assets/socialMediaISvgIcons/facebook.svg";

const teamCard = props => (
    <div className={classes.TeamCard}>
        <img src={props.picture} alt={props.name} className={classes.Img} />
        <span
            className={classes.Label}
            style={{
                backgroundImage:
                    props.r === "FE"
                        ? "radial-gradient(circle, #32aedc, #00bade, #00c5db, #00cfd5, #00d9ca)"
                        : props.r === "BE"
                        ? "radial-gradient(circle, #c25e5e, #b76064, #ac6169, #a0636c, #93656e)"
                        : props.r === "QA"
                        ? "radial-gradient(circle, #00b465, #24a460, #31945c, #398457, #3e7552)"
                        : "radial-gradient(circle, #2f4858, #496070, #637988, #7f93a2, #9baebc)"
            }}
        ></span>
        <h2 className={classes.Name}>{props.name}</h2>
        <p
            className={classes.Role}
            style={{
                color:
                    props.r === "FE"
                        ? "#32aedc"
                        : props.r === "BE"
                        ? "#c25e5e"
                        : props.r === "QA"
                        ? "#00b465"
                        : "#2f4858"
            }}
        >
            {props.role}
        </p>

        <p className={classes.About}>{props.about}</p>

        <hr />

        <div className={classes.ScIcons}>
            <a href={props.lnLink} target="_blank" rel="noopener noreferrer" className={props.Link}>
                <img src={LnIcon} alt="ln" className={classes.Icon} />
            </a>
            <a href={props.gitLink} target="_blank" rel="noopener noreferrer">
                <img src={GitIcon} alt="git" className={classes.Icon} />
            </a>
            <a href={props.fbLink} target="_blank" rel="noopener noreferrer">
                <img src={FbIcon} alt="fb" className={classes.Icon} />
            </a>
        </div>
    </div>
);

export default teamCard;
