import React, { Component } from "react";
import { View } from "react-native";

import { TabHeader } from "../components";

const DATA = [
	{
		id: "1",
		title: "Berina Omerasevic",
		description: "berkica@gmail.com"
	},
	{
		id: "2",
		title: "Hamza Crnovrsanin",
		description: "hamzic@gmail.com"
	},
	{
		id: "3",
		title: "Ajdin Zorlak",
		description: "zoka@gmail.com"
	},
	{
		id: "4",
		title: "Amina Muzurovic",
		description: "muzi@gmail.com"
	},
	{
		id: "5",
		title: "Faris Spica",
		description: "spica_u_vodi@gmail.com"
	},
	{
		id: "6",
		title: "Tajib Smajlovic",
		description: "tajci_rajif@gmail.com"
	},
	{
		id: "7",
		title: "Ferhat Avdic",
		description: "wannabe_rajif@gmail.com"
	},
	{
		id: "9",
		title: "Amra Rovcanin",
		description: "duck_whisperer@gmail.com"
	},
	{
		id: "11",
		title: "Berina Omerasevic",
		description: "berkica@gmail.com"
	},
	{
		id: "21",
		title: "Hamza Crnovrsanin",
		description: "hamzic@gmail.com"
	},
	{
		id: "31",
		title: "Ajdin Zorlak",
		description: "zoka@gmail.com"
	},
	{
		id: "44",
		title: "Amina Muzurovic",
		description: "muzi@gmail.com"
	}
];

export default class People extends Component {
	constructor(props) {
		super(props);
		this.state = {
			data: DATA
		};
	}

	sideDrawer = () => this.props.navigation.openDrawer();

	render() {
		return <TabHeader title={"EMPLOYEES"} data={this.state.data} onClick={this.sideDrawer} />;
	}
}
