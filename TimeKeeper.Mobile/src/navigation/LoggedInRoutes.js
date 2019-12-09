import React from "react";
import { createBottomTabNavigator } from "react-navigation-tabs";
import { createStackNavigator } from "react-navigation-stack";
import { Ionicons } from "@expo/vector-icons";

import Profile from "../views/Profile";
import Calendar from "../views/Calendar";
import People from "../views/People";

const StackNavigator = createStackNavigator({
	Profile: {
		screen: Profile
	},
	Calendar: {
		screen: Calendar
	}
});

const LoggedInRoutes = createBottomTabNavigator(
	{
		People: {
			screen: People,
			navigationOptions: {
				tabBarIcon: ({ tintColor }) => <Ionicons name="ios-people" size={40} color={tintColor} />
			}
		},
		Profile: {
			screen: StackNavigator,
			navigationOptions: {
				tabBarIcon: ({ tintColor }) => <Ionicons name="ios-person" size={32} color={tintColor} />
			}
		}
	},
	{
		tabBarOptions: { showLabel: false, activeTintColor: "#32aedc", inactiveTintColor: "gray" }
	}
);

export default LoggedInRoutes;
