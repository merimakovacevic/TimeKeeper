import React, { Component } from "react";

import { TabHeader } from "../components/TabHeader";

const DATA = [
	{
		id: "1",
		title: "Project11",
		description: "berkica@gmail.com"
	},
	{
		id: "2",
		title: "proroeo",
		description: "hamzic@gmail.com"
	},
	{
		id: "3",
		title: "proororor",
		description: "zoka@gmail.com"
	},
	{
		id: "4",
		title: "Amina prprpprpr",
		description: "muzi@gmail.com"
	},
	{
		id: "5",
		title: "Faris teteetet",
		description: "spica_u_vodi@gmail.com"
	},
	{
		id: "6",
		title: "Tajib tesatsta",
		description: "tajci_rajif@gmail.com"
	},
	{
		id: "7",
		title: "Ferhat Avteeatedic",
		description: "wannabe_rajif@gmail.com"
	},
	{
		id: "9",
		title: "AmrTESTRovcanin",
		description: "duck_whisperer@gmail.com"
	}
];

export default class Projects extends Component {
	constructor(props) {
		super(props);
		this.state = {
			data: DATA
		};
	}

	sideDrawer = () => this.props.navigation.openDrawer();

	render() {
		return <TabHeader title="PROJECTS" data={this.state.data} onClick={this.sideDrawer} />;
	}
}
