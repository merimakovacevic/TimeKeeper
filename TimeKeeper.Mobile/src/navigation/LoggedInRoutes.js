import React from "react";
import { createBottomTabNavigator } from "react-navigation-tabs";
import { createStackNavigator } from "react-navigation-stack";
import { createDrawerNavigator } from "react-navigation-drawer";
import { Ionicons } from "@expo/vector-icons";

import Profile from "../views/Profile";
import Calendar from "../views/Calendar";
import People from "../views/People";
import Customers from "../views/Customers";
import Projects from "../views/Projects";
import Agenda from "../views/Agenda";

const StackNavigator = createStackNavigator({
  Profile: {
    screen: Profile
  },
  Calendar: {
    screen: Calendar
  }
});

const DrawerNavigator = createDrawerNavigator({
  People: {
    screen: People
  },
  Customers: {
    screen: Customers
  },
  Projects: {
    screen: Projects
  },
  Agenda: {
    screen: Agenda
  }
});

const LoggedInRoutes = createBottomTabNavigator(
  {
    People: {
      screen: DrawerNavigator,
      navigationOptions: {
        tabBarIcon: ({ tintColor }) => (
          <Ionicons name="ios-people" size={40} color={tintColor} />
        )
      }
    },
    Profile: {
      screen: StackNavigator,
      navigationOptions: {
        tabBarIcon: ({ tintColor }) => (
          <Ionicons name="ios-person" size={32} color={tintColor} />
        )
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
