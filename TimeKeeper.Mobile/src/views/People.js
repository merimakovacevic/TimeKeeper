import React, { Component } from "react";
import { connect } from "react-redux";
import { View } from "react-native";

import { fetchEmployees } from "../redux/actions/employeesActions";

import { TabHeader } from "../components/TabHeader";

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

class People extends Component {
	constructor(props) {
		super(props);
		this.state = {
			data: DATA
		};
	}

	componentDidMount() {
		// console.log(this.props.fetchEmployees());
	}

	sideDrawer = () => this.props.navigation.openDrawer();

	render() {
		return <TabHeader title={"EMPLOYEES"} data={this.state.data} onClick={this.sideDrawer} />;
	}
}

export default connect(null, { fetchEmployees })(People);
