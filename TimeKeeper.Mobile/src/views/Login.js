import React, { Component } from "react";
import { connect } from "react-redux";
import axios from "axios";
import { View, Image, Text, ActivityIndicator } from "react-native";

import theme from "../assets/Theme";
import { Button, Input } from "../components";

class Login extends Component {
	componentDidMount() {
		this.authorize();
	}

	authorize = () => {
		console.log("Usaoo");
		var data = {
			username: "michaeljo",
			password: "$ch00l",
			client: "tk2019"
		};
		let axiosConfig = {
			headers: {
				"Content-Type": "application/json;charset=UTF-8"
			}
		};
		// axios
		// 	.post(`http://192.168.44.7:44300/login`, data, axiosConfig)
		// 	.then((res) => {
		// 		// AsyncStorage.setItem(TOKEN, res.data.token);
		// 		// return cb(200);
		// 		console.log(res);
		// 	})
		// 	.catch((err) => {
		// 		// return cb(401);
		// 		console.log(err);
		// 	});

		axios("https://facebook.github.io/react-native/movies.json")
			.then((res) => console.log(res))
			.catch((err) => console.log(err));

		fetch("http://192.168.44.7:44350/login", {
			method: "POST",
			headers: {
				Accept: "application/json",
				"Content-Type": "application/json;charset=UTF-8"
			},
			body: JSON.stringify(data)
		})
			.then((res) => {
				console.log(res);
			})
			.catch((err) => console.log(err));
	};

	render() {
		const { loading } = this.props;
		// console.log(loading);
		return (
			<View style={styles.container}>
				<Image style={styles.logo} />
				<Text style={styles.mainTitle}>Login to your account</Text>
				<Text style={styles.title}> Save time for doing great work.</Text>
				<Input name="name" placeholder="Name" autoCompleteType={false} keyboardType="default" />
				<Input name="password" placeholder="Password" autoCompleteType={false} keyboardType="default" />
				<Button onPress={this.login}>
					{loading ? (
						<ActivityIndicator size="large" color={theme.COLORS.DEFAULT} />
					) : (
						<Text style={styles.title}>Login</Text>
					)}
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
		marginTop: 10
	},
	title: {
		color: theme.COLORS.WHITE,
		alignSelf: "center"
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

const mapStateToProps = (state) => {
	return {
		loading: state.user.loading
	};
};

export default connect(mapStateToProps)(Login);
