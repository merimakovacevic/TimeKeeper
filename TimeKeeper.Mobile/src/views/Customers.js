import React, { Component } from "react";

import { TabHeader } from "../components";

const DATA = [
	{
		id: "1",
		title: "Customertest",
		description: "custtt@gmail.com"
	},
	{
		id: "2",
		title: "CUSTOMERMMR Crnovrsanin",
		description: "hamzic@gmail.com"
	},
	{
		id: "3",
		title: "AjdinTUT Zorlak",
		description: "zoka@gmail.com"
	},
	{
		id: "4",
		title: "CUSTOMERRR Muzurovic",
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
	}
];

export default class Customers extends Component {
	constructor(props) {
		super(props);
		this.state = {
			data: DATA
		};
	}

	sideDrawer = () => this.props.navigation.openDrawer();

	render() {
		return <TabHeader title={"CUSTOMERS"} data={this.state.data} onClick={this.sideDrawer}/>;
	}
}
