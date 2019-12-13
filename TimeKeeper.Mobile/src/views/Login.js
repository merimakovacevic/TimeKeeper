import React, { Component } from "react";
import { connect } from "react-redux";
import { View, Image, KeyboardAvoidingView, StyleSheet, ActivityIndicator } from "react-native";

import { auth } from "../redux/actions/index";
import { isLoggedIn } from "../redux/actions/authActions";
import Button from "../components/Button";
import Input from "../components/Input";
import logo from "../../assets/logo.png";

class Login extends Component {
	static navigationOptions = {
		header: null
	};

	state = {
		username: "",
		password: ""
	};

	componentDidMount() {
		this.props.isLoggedIn();
	}

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

	loginRenderHandler = () => {
		// console.log(this.props.user);
		if (this.props.loading) {
			return <ActivityIndicator style={styles.loader} size={80} color="#00ff00" />;
		} else {
			console.log(this.props.user);
			if (this.props.user.token !== undefined) {
				return this.props.navigation.navigate("People");
			} else {
				return (
					<KeyboardAvoidingView style={styles.container} behavior="padding">
						<Image resizeMode="contain" style={styles.logo} source={logo} />

						<View style={styles.form}>
							<Input
								value={this.state.email}
								onChangeText={this.handleUsernameChange}
								placeholder={"Username"}
								autoCorrect={false}
								keyboardType="email-address"
								returnKeyType="next"
							/>
							<Input
								ref={this.passwordInputRef}
								value={this.state.password}
								onChangeText={this.handlePasswordChange}
								placeholder={"Password"}
								secureTextEntry={true}
								returnKeyType="done"
							/>
							<Button label={"Login"} onPress={this.handleLoginPress} />
						</View>
					</KeyboardAvoidingView>
				);
			}
		}
	};

	render() {
		return this.loginRenderHandler();
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
		//justifyContent: "center",
		width: "80%",
		marginBottom: 25
	},
	loader: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	}
});

const mapStateToProps = (state) => {
	return {
		loading: state.user.loading,
		user: state.user.user
	};
};

export default connect(mapStateToProps, { auth, isLoggedIn })(Login);
