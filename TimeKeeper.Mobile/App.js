import React, { Component } from "react";
import axios from "axios";
import { StyleSheet, SafeAreaView, Button } from "react-native";
import { createAppContainer } from "react-navigation";
import { Provider } from "react-redux";
import { Linking } from "expo";
import * as AppAuth from "expo-app-auth";

import configureStore from "./src/redux/configureStore";
import { getRootNavigator } from "./src/navigation/index";
import { config } from "./src/utils/config";

const store = configureStore();

async function signInAsync() {
	console.log("test");
	const authState = await AppAuth.authAsync(config);
	await cacheAuthAsync(authState);
	console.log("signInAsync", authState);
	return authState;
}

export default class App extends Component {
	// componentDidMount() {
	// 	axios("http://192.168.30.145:8080/api/employees")
	// 		.then((res) => console.log(res))
	// 		.catch((err) => console.log(err));
	// 	// Linking.openURL("https://192.168.1.100:44300")}
	// }

	render() {
		const RootNavigator = createAppContainer(getRootNavigator(false));

		return (
			<Provider store={store}>
				<SafeAreaView style={styles.container}>
					{/* <RootNavigator /> */}
					<Button title="click" onPress={signInAsync} />
				</SafeAreaView>
			</Provider>
		);
	}
}

const styles = StyleSheet.create({
	container: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	}
});
