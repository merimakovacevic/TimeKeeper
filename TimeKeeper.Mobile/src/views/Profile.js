import React, { Component } from "react";
import { StyleSheet, Text, View, Image, TouchableOpacity } from "react-native";
import { connect } from "react-redux";

import { logout, logoutToken } from "../redux/actions/authActions";
import Button from "../components/Button";

class Profile extends Component {
	render() {
		return (
			<View style={styles.container}>
				<View style={styles.header}></View>
				<Image style={styles.avatar} source={{ uri: "https://bootdey.com/img/Content/avatar/avatar6.png" }} />
				<View style={styles.body}>
					<View style={styles.bodyContent}>
						<Text style={styles.name}>John Doe</Text>
						<Text style={styles.info}>Administrator</Text>
						<Text style={styles.description}>email: admin@asd.com</Text>
					</View>
					<View style={styles.buttonContainer}>
						<Button
							label="Logout"
							onPress={() => {
								logoutToken();
								this.props.logout();
								//setTimeout(() => this.props.navigation.navigate("Login"), 2500);
								this.props.navigation.navigate("Login");
							}}
						/>
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
		color: "#FFFFFF",
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
	name: {
		fontSize: 28,
		color: "#696969",
		fontWeight: "600"
	},
	info: {
		fontSize: 16,
		color: "#00BFFF",
		marginTop: 10
	},
	description: {
		fontSize: 16,
		color: "#696969",
		marginTop: 10,
		textAlign: "center"
	},
	buttonContainer: {
		marginTop: 100
	}
});

export default connect(null, { logout })(Profile);
