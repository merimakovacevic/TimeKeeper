import React, { Component } from "react";
import { connect } from "react-redux";
import { View, ActivityIndicator, StyleSheet } from "react-native";

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
		this.props.fetchEmployees();
	}

	sideDrawer = () => this.props.navigation.openDrawer();

	render() {
		let PeopleRender = () => {
			if (this.props.loading) {
				return <ActivityIndicator style={styles.container} size={100} color="#32aedc" />;
			} else {
				return <TabHeader title={"EMPLOYEES"} data={this.props.people} onClick={this.sideDrawer} />;
			}
		};

		return PeopleRender();
	}
}

const styles = StyleSheet.create({
	container: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	}
});

const mapStateToProps = (state) => {
	return {
		people: state.employees.data,
		loading: state.employees.loading
	};
};

export default connect(mapStateToProps, { fetchEmployees })(People);
