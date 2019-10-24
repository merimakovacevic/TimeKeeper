import React from "react";
import Slider from "react-slick";

import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import classes from "./TeamPage.module.css";
import pictures from "../../data/profilePictures";
import teamData from "../../data/teamMembers.json";
import TeamCard from "./TeamCard/TeamCard";

const teamPage = props => {
    let settings = {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1
    };

    return (
        <div id={props.passedId} className={classes.TeamPage}>
            <h1>Team Page</h1>
            <p className={classes.TeamPageSlogan}>
                Lorem ipsum dolor sit amet, consectetur adipisicing elit. Non, quo doloribus
                delectus dolore expedita, repellendus testtes as
            </p>
            <ul style={{ margin: "0 2rem", listStyleType: "none" }}>
                <Slider {...settings}>
                    {teamData.map((d, i) => (
                        <TeamCard key={i} picture={pictures[i]} />
                    ))}
                </Slider>
            </ul>
        </div>
    );
};

export default teamPage;
