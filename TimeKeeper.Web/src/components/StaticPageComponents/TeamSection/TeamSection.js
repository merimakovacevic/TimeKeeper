import React from "react";
import Slider from "react-slick";

import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./arrowStyle.css";
import classes from "./TeamSection.module.css";
import pictures from "../../../data/profilePictures";
import teamData from "../../../data/teamMembers.json";
import TeamCard from "./TeamCard/TeamCard";

const teamSection = props => {
    let settings = {
        dots: true,
        infinite: true,
        speed: 1000,
        slidesToShow: 3,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    dots: true,
                    arrows: true
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    dots: true,
                    arrows: true
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    };

    const teamMembers = teamData.map((d, i) => (
        <TeamCard
            key={i}
            r={d.r}
            picture={pictures[i]}
            name={d.name}
            role={d.role}
            about={d.about}
            lnLink={d.socialMedia.ln}
            gitLink={d.socialMedia.git}
            fbLink={d.socialMedia.fb}
        />
    ));

    return (
        <div id={props.passedId} className={classes.TeamSection}>
            <h1>Our Team</h1>
            <p className={classes.TeamSectionSlogan}>
                Meet the squad: young, professional, focused and motivated to solve any and all
                problems you might have.
            </p>
            <ul
                style={{ margin: "0 2rem", listStyleType: "none" }}
                className={classes.TeamSectionList}
            >
                <Slider {...settings}>{teamMembers}</Slider>
            </ul>
        </div>
    );
};

export default teamSection;
