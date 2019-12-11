import React, { Component } from "react";
import { connect } from "react-redux";
import { View, Image, Text, KeyboardAvoidingView } from "react-native";

import { auth } from "../redux/actions/index";
import { Button } from "../components/Button";
import { Input } from "../components/Input";
import logo from "../../assets/logo.png";

class Login extends Component {
	state = {
		username: "",
		password: ""
	};

	render() {
		let credentials = {
			username: this.state.username,
			password: this.state.password
		};
		return (
			<View style={styles.container}>
				<Image source={logo} style={styles.logo} />
				<Text style={styles.mainTitle}>Welome to TimeKeeper</Text>
				<Text style={styles.title}> Save time for doing great work.</Text>
				<KeyboardAvoidingView style={styles.container} behavior="padding" enabled>
					<Input
						placeholder="username"
						name="username"
						onChangeText={(username) => this.setState({ username })}
						value={this.state.username}
					/>
					<Input
						placeholder="password"
						name="password"
						secureTextEntry={true}
						onChangeText={(password) => this.setState({ password })}
						value={this.state.password}
					/>
				</KeyboardAvoidingView>
				<Button
					onPress={() => {
						// this.props.auth(credentials);
						this.props.navigation.navigate("LoggedInRoutes");
					}}
				>
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
		padding: 10,
		backgroundColor: "rgb(204, 243, 255)"
	},
	logo: {
		marginTop: 100,
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

const mapStateToProps = (state) => {
	return {
		loading: state.user.loading
	};
};

export default connect(mapStateToProps, { auth })(Login);
