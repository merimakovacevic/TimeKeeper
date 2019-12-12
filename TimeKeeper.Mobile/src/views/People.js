import React, { Component } from "react";
import { connect } from "react-redux";
import { View, ActivityIndicator, StyleSheet, Text, TouchableOpacity } from "react-native";

import { fetchEmployees } from "../redux/actions/employeesActions";

import List from "../components/List";
import { Icon, Header, Left } from "native-base";

class People extends Component {
	componentDidMount() {
		this.props.fetchEmployees();
	}

  _onSelectUser = (id) => {
    this.props.navigation.navigate("EmployeeProfile", {
      id: id,
      type: "employee"
    });
  };

  sideDrawer = () => this.props.navigation.openDrawer();

	render() {
		let PeopleRender = () => {
			if (this.props.loading) {
				return <ActivityIndicator style={styles.loader} size={100} color="#32aedc" />;
			} else {
				return (
					<View style={styles.container}>
						<Header style={styles.head}>
							<Left>
								<TouchableOpacity>
									<Icon
										style={styles.icon}
										name="ios-menu"
										onPress={() => this.props.navigation.openDrawer()}
									/>
								</TouchableOpacity>
							</Left>
							<Text style={styles.header}>EMPLOYEES</Text>
						</Header>
						<List data={this.props.people} onPress={this._onSelectUser} />
					</View>
				);
			}
		};

    return PeopleRender();
  }
}

const styles = StyleSheet.create({
	loader: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	},
	container: {
		flex: 1,
		marginTop: 0
	},
	head: {
		backgroundColor: "white",
		// marginTop: 15,
		display: "flex",
		justifyContent: "space-between",
		alignItems: "center"
	},
	icon: {
		marginLeft: 10
	},
	button: {
		height: 40,
		width: 40,
		backgroundColor: "#eee",
		borderRadius: 5,
		justifyContent: "center",
		alignItems: "center"
	},
	header: {
		fontSize: 30,
		fontWeight: "bold",
		marginRight: 100,
		marginTop: 5
	}
});

const mapStateToProps = (state) => {
  return {
    people: state.employees.data,
    loading: state.employees.loading
  };
};

export default connect(mapStateToProps, { fetchEmployees })(People);
