import React, { Component } from "react";
import { View, Image, Text } from "react-native";
import { Button } from "../components";
import { Input } from "../components";
import { List } from "../components";

class Login extends Component {
	_login = () => {
		console.log("I pressed Login Button");
	};

	render() {
		return (
			<View style={styles.container}>
				<Image style={styles.logo} />
				<Text style={styles.mainTitle}>Login to your account</Text>
				<Text style={styles.title}> Save time for doing great work.</Text>
				{/* <Input style={styles.username} name="name" placeholder="Name" autoCompleteType="username" />
				<Input style={styles.password} name="password" placeholder="Password" autoCompleteType="password" /> */}
				<Button onPress={this._login}>Login</Button>
			</View>
		);
	}
}

const styles = {
	container: {
		flex: 1,
		backgroundColor: "white",
		justifyContent: "flex-start",
		alignItems: "center",
		padding: 10
	},
	logo: {
		marginTop: 10
	},
	mainTitle: {
		fontFamily: "Roboto",
		fontSize: 25,
		fontWeight: "bold",
		marginTop: 10
	},
	title: {
		marginTop: 10,
		marginBottom: 30,
		fontSize: 20
	}
};

export default Login;
