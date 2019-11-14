import React from "react";

import classes from "./ServicesSection.module.css";
import serviceIcons from "../../../data/serviceIcons";
import serviceData from "../../../data/services";
import ServiceCard from "./ServiceCard/ServiceCard";

const servicesSection = props => (
    <div id={props.passedId} className={classes.Services}>
        <h1>Services</h1>
        <p className={classes.ServiceParagraph}>
            Leveraging our cross-industrial experience and adept business analysis competencies, we
            tailor solutions to our customers’ individual needs.
        </p>

        <div className={classes.ServicesCards}>
            {serviceData.map((s, i) => (
                <ServiceCard
                    key={i}
                    serviceIcon={serviceIcons[i]}
                    serviceTitle={s.serviceTitle}
                    serviceDescription={s.serviceDescription}
                />
            ))}
        </div>
    </div>
);

export default servicesSection;
