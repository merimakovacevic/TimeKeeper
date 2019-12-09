import React, { Component } from "react";
import { View, Image, Text } from "react-native";
import { Button } from "../components";
import { Input } from "../components";
import { List } from "../components";
import logo from "../../assets/logo.png";

class Login extends Component {
	_login = () => {
		console.log("I pressed Login Button");
	};

	render() {
		return (
			<View style={styles.container}>
				<Image source={logo} style={styles.logo} />
				<Text style={styles.mainTitle}>Welome to TimeKeeper</Text>
				<Text style={styles.title}> Save time for doing great work.</Text>
				<Button onPress={this._login}>
					<Text style={styles.buttonTitle}>Login</Text>
				</Button>
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
		marginTop: 10,
		height: 85,
		width: 85
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
	},
	buttonTitle: {
		alignSelf: "center"
	}
};

// const mapStateToProps = (state) => {
// 	return {
// 		loading: state.user.loading
// 	};
// };

export default Login;
