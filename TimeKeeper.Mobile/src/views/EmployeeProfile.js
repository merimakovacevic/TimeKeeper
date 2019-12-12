import React, { Component } from "react";
import { connect } from "react-redux";
import { Text, View, StyleSheet, Image, TouchableOpacity } from "react-native";
import RNModal from "../components/Modal";

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

class EmployeeProfile extends Component {
	state = {
		open: false
	};
	handleOpen = () => {
		this.setState({ open: true });
	};
	handleClose = () => {
		this.setState({ open: false });
	};
	render() {
		const id = this.props.navigation.getParam("id", "0");
		const employeeData = this.props.people.find((e) => e.id === id);
		return (
			<View style={styles.container}>
				{/* <Text style={styles.header}> Employee profile </Text> */}
				<Image style={styles.avatar} source={{ uri: "https://bootdey.com/img/Content/avatar/avatar6.png" }} />
				<View style={styles.body}>
					<View style={styles.bodyContent}>
						<Text style={styles.name}>Name: {employeeData.firstName}</Text>
						<Text style={styles.description}>Email: {employeeData.email}</Text>
						<TouchableOpacity onPress={this.handleOpen} style={styles.buttonContainer}>
							<Text>Calendar</Text>
						</TouchableOpacity>
						<RNModal visible={this.state.open} onClose={this.handleClose} />
					</View>
				</View>
			</View>
		);
	}
}

const styles = StyleSheet.create({
	header: {
		backgroundColor: "#00BFFF",
		height: 200
	},
	avatar: {
		width: 130,
		height: 130,
		borderRadius: 63,
		borderWidth: 4,
		borderColor: "white",
		marginBottom: 10,
		alignSelf: "center",
		position: "absolute",
		marginTop: 130
	},
	name: {
		fontSize: 22,
		color: "#696969",
		fontWeight: "600"
	},
	body: {
		marginTop: 40
	},
	bodyContent: {
		flex: 1,
		alignItems: "center",
		padding: 30
	},
	description: {
		fontSize: 16,
		color: "#696969",
		marginTop: 10,
		textAlign: "center"
	},
	buttonContainer: {
		marginTop: 10,
		height: 45,
		flexDirection: "row",
		justifyContent: "center",
		alignItems: "center",
		marginBottom: 20,
		width: 250,
		borderRadius: 30,
		backgroundColor: "#00BFFF"
	}
});

const mapStateToProps = (state) => {
	return {
		people: state.employees.data
	};
};

export default connect(mapStateToProps)(EmployeeProfile);
