import React, { Component } from "react";
import { View, Text, ActivityIndicator } from "react-native";
import List from "../components/List";
import Constants from "expo-constants";
import { Icon, Header, Left } from "native-base";

import { projectsUrl, apiGetAllRequest } from "../utils/api";

export default class People extends Component {
	state = {
		loading: false,
		projects: []
	};

	componentDidMount() {
		this.setState({ loading: true });
		apiGetAllRequest(projectsUrl)
			.then((res) => {
				// console.log(res.data.data);
				this.setState({ projects: res.data.data, loading: false });
			})
			.catch((err) => console.log(err));
	}

	render() {
		let projectsRender = () => {
			if (this.state.loading) {
				return <ActivityIndicator style={styles.loader} size={100} color="#32aedc" />;
			} else {
				return (
					<View style={styles.container}>
						<Header style={styles.head}>
							<Left>
								<Icon
									style={styles.icon}
									name="ios-menu"
									onPress={() => this.props.navigation.openDrawer()}
								/>
							</Left>
							<Text style={styles.header}>PROJECTS</Text>
						</Header>
						<List data={this.state.projects} onPress={() => console.log("hehe")} type="projects" />
					</View>
				);
			}
		};

		return projectsRender();
	}
}

const styles = {
	container: {
		flex: 1,
		marginTop: Constants.statusBarHeight
	},
	list: {
		flex: 1
	},
	header: {
		fontSize: 30,
		fontWeight: "bold",
		marginLeft: -80,
		color: "black",
		marginTop: 10
	},
	head: {
		backgroundColor: "white"
	},
	icon: {
		marginLeft: -85
	},
	loader: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	}
};
