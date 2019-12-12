import React, { Component } from "react";
import { connect } from "react-redux";
import { View, ActivityIndicator, StyleSheet, Text } from "react-native";

import { fetchEmployees } from "../redux/actions/employeesActions";

import List from "../components/List";
import { Icon, Header, Left } from "native-base";

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
	}
];

class People extends Component {
	static navigationOptions = {
		header: null
	};

	constructor(props) {
		super(props);
		this.state = {
			data: DATA
		};
	}

	componentDidMount() {
		this.props.fetchEmployees();
	}

	_onSelectUser = (id) => {
		this.props.navigation.navigate("EmployeeProfile", {
			id: id,
			type: "employee"
		});
	};

	sideDrawer = () => this.props.navigation.openDrawer();

	render() {
		let PeopleRender = () => {
			if (this.props.loading) {
				return <ActivityIndicator style={styles.loader} size={100} color="#32aedc" />;
			} else {
				return (
					<View style={styles.container}>
						<Header style={styles.head}>
							<Left>
								<Icon
									style={styles.icon}
									name="ios-menu"
									onPress={() => this.props.navigation.openDrawer()}
								/>
							</Left>
							<Text style={styles.header}>EMPLOYEES</Text>
						</Header>
						<List data={this.props.people} onPress={this._onSelectUser} />
					</View>
				);
			}
		};

		return PeopleRender();
	}
}

const styles = StyleSheet.create({
	loader: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	},
	container: {
		flex: 1,
		marginTop: 15
	},
	head: {
		backgroundColor: "white",
		marginTop: 15,
		display: "flex",
		justifyContent: "space-between",
		alignItems: "center"
	},
	icon: {
		marginLeft: 0
	},
	button: {
		height: 40,
		width: 40,
		backgroundColor: "#eee",
		borderRadius: 5,
		justifyContent: "center",
		alignItems: "center"
	},
	header: {
		fontSize: 30,
		fontWeight: "bold",
		marginRight: 100,
		marginTop: 5
	}
});

const mapStateToProps = (state) => {
	return {
		people: state.employees.data,
		loading: state.employees.loading
	};
};

export default connect(mapStateToProps, { fetchEmployees })(People);
