import React, { Component } from "react";
import List from "../components/List";
import SafeAreaView from "react-native-safe-area-view";
import { Text } from "react-native";
// import { Header } from "react-native-elements";
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

	render() {
		return (
			<SafeAreaView style={styles.container}>
				<Header style={styles.head}>
					<Left>
						<Icon style={styles.icon} name="ios-menu" onPress={() => this.props.navigation.openDrawer()} />
					</Left>
					<Text style={styles.header}>EMPLOYEES</Text>
				</Header>

				<List data={this.state.data} />
			</SafeAreaView>
		);
	}
}

const styles = {
	container: {
		flex: 1,
		marginTop: 10
		//backgroundColor: theme.COLORS.LISTCOLOR,
	},
	list: {
		flex: 1
		//backgroundColor: theme.COLORS.LISTCOLOR,
	},
	header: {
		fontSize: 30,
		fontWeight: "bold",
		marginLeft: -80,
		//color: theme.COLORS.LISTCOLOR,
		marginTop: 10
	},
	head: {
		backgroundColor: "white",
		marginTop: 15
	},
	icon: {
		marginLeft: -65
	}
};
