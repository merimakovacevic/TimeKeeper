import React, { Component } from "react";
import { View, Text, ActivityIndicator } from "react-native";
import List from "../components/List";
import Constants from "expo-constants";
import { Icon, Header, Left } from "native-base";

import { apiGetAllRequest, teamsUrl } from "../utils/api";

export default class Teams extends Component {
	state = {
		loading: false,
		teams: []
	};

	componentDidMount() {
		this.setState({ loading: true });
		apiGetAllRequest(teamsUrl)
			.then((res) => {
				//console.log("customers");
				//console.log(res.data.data);
				this.setState({ teams: res.data.data, loading: false });
			})
			.catch((err) => console.log(err));
	}

	render() {
		//console.log(this.state.customers);
		let teamsRender = () => {
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
							<Text style={styles.header}>TEAMS</Text>
						</Header>
						<List data={this.state.teams} type="teams" onPress={() => console.log("hehe")} />
					</View>
				);
			}
		};

		return teamsRender();
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
		marginLeft: -100
	},
	loader: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	}
};
