import React from "react";

const Wrapper = (props, { children }) => <div>{props.open ? props.children : null}</div>;

export default Wrapper;
