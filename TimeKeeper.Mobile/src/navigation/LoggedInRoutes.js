import React from "react";
import { createBottomTabNavigator } from "react-navigation-tabs";
import {} from "@expo/vector-icons";
import { createStackNavigator } from "react-navigation-stack";
import People from "../views/People";
import Profile from "../views/Profile";
import Projects from "../views/Projects";
import Teams from "../views/Teams";
import Customers from "../views/Customers.js";
import Calendar from "../views/Calendar";
import { createDrawerNavigator } from "react-navigation-drawer";
import EmployeeProfile from "../views/EmployeeProfile";
import { Ionicons } from "@expo/vector-icons";

const StackNavigator = createStackNavigator(
	{
		Profile: {
			screen: Profile
		},
		Calendar: {
			screen: Calendar
		}
	},
	{
		drawerStyle: {
			backgroundColor: "#c6cbef",
			width: 240
		}
	}
);

const StackNavigatorEmployee = createStackNavigator({
	EMPLOYEES: {
		screen: People
	},
	EmployeeProfile: {
		screen: EmployeeProfile
	}
});

const DrawerNavigator = createDrawerNavigator({
	EMPLOYEES: {
		screen: StackNavigatorEmployee
	},
	CUSTOMERS: {
		screen: Customers
	},
	PROJECTS: {
		screen: Projects
	},
	// CALENDAR: {
	// 	screen: Calendar
	// },
	TEAMS: {
		screen: Teams
	}
});

const LoggedInRoutes = createBottomTabNavigator(
	{
		People: {
			screen: DrawerNavigator,
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
		tabBarOptions: {
			showLabel: false,
			activeTintColor: "#32aedc",
			inactiveTintColor: "gray"
		}
	}
);

export default LoggedInRoutes;
