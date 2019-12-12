import React, { Component } from "react";
import { connect } from "react-redux";
import { View, Image, KeyboardAvoidingView, StyleSheet } from "react-native";

import { auth } from "../redux/actions/index";
import Button from "../components/Button";
import Input from "../components/Input";
import logo from "../../assets/logo.png";

class Login extends Component {
	state = {
		username: "",
		password: ""
	};

	handleUsernameChange = (username) => {
		this.setState({ username: username });
	};

	handlePasswordChange = (password) => {
		this.setState({ password: password });
	};

	handleLoginPress = () => {
		let credentials = {
			username: this.state.username,
			password: this.state.password
		};

		this.props.auth(credentials);
		setTimeout(() => this.props.navigation.navigate("People"), 1000);
	};

	render() {
		return (
			<KeyboardAvoidingView style={styles.container} behavior="padding">
				<Image resizeMode="contain" style={styles.logo} source={logo} />

				<View style={styles.form}>
					<Input
						value={this.state.email}
						onChangeText={this.handleUsernameChange}
						placeholder={"username"}
						autoCorrect={false}
						keyboardType="email-address"
						returnKeyType="next"
					/>
					<Input
						ref={this.passwordInputRef}
						value={this.state.password}
						onChangeText={this.handlePasswordChange}
						placeholder={"Pass"}
						secureTextEntry={true}
						returnKeyType="done"
					/>
					<Button label={"Login"} onPress={this.handleLoginPress} />
				</View>
			</KeyboardAvoidingView>
		);
	}
}

const styles = StyleSheet.create({
	container: {
		flex: 1,
		backgroundColor: "white",
		alignItems: "center"
		//justifyContent: "flex-start"
	},
	logo: {
		flex: 1,
		width: "35%",
		resizeMode: "contain",
		alignSelf: "center"
	},
	form: {
		flex: 1,
		justifyContent: "center",
		width: "80%"
	}
});

const mapStateToProps = (state) => {
	return {
		loading: state.user.loading
	};
};

export default connect(mapStateToProps, { auth })(Login);
