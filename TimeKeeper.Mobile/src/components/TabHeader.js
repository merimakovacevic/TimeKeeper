import React from "react";
import SafeAreaView from "react-native-safe-area-view";
import { Text, TouchableOpacity } from "react-native";
import { Icon, Header, Left } from "native-base";

import { List } from "../components/List";

export const TabHeader = ({ title, data, onClick }) => {
	return (
		<SafeAreaView style={styles.container}>
			<Header style={styles.head}>
				<Left>
					<TouchableOpacity style={styles.button} onPress={onClick}>
						<Icon style={styles.icon} name="ios-menu" />
					</TouchableOpacity>
				</Left>
				<Text style={styles.header}>{title}</Text>
			</Header>

			<List data={data} />
		</SafeAreaView>
	);
};

const styles = {
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
};
