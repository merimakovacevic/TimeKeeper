import React, { Component } from "react";
import { StyleSheet, SafeAreaView } from "react-native";
import { createAppContainer } from "react-navigation";
import { Provider } from "react-redux";

import configureStore from "./src/redux/configureStore";
import { getRootNavigator } from "./src/navigation/index";

export const store = configureStore();

export default class App extends Component {
	render() {
		const RootNavigator = createAppContainer(getRootNavigator(false));

		return (
			<Provider store={store}>
				<SafeAreaView style={styles.container}>
					<RootNavigator />
				</SafeAreaView>
			</Provider>
		);
	}
}

const styles = StyleSheet.create({
	container: {
		flex: 1
	}
});
