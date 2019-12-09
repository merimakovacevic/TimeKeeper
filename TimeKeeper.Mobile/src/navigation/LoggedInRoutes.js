import React from "react";
import { createBottomTabNavigator } from "react-navigation-tabs";
import { createStackNavigator } from "react-navigation-stack";
import { Ionicons, Octicons, AntDesign } from "@expo/vector-icons";
import Profile from "../views/Profile";
import Calendar from "../views/Calendar";
import People from "../views/People";
import Projects from "../views/Projects";
import Customers from "../views/Customers";
import Agenda from "../views/Agenda";

const StackNavigator = createStackNavigator({
  Profile: {
    screen: Profile
  },
  Calendar: {
    screen: Calendar
  },
  Projects: {
    screen: Projects
  },
  Customers: {
    screen: Customers
  },
  Agenda: {
    screen: Agenda
  }
});

const LoggedInRoutes = createBottomTabNavigator(
  {
    People: {
      screen: People,
      navigationOptions: {
        tabBarIcon: ({ tintColor }) => (
          <Ionicons name="ios-people" size={40} color={tintColor} />
        )
      }
    },
    Projects: {
      screen: Projects,
      navigationOptions: {
        tabBarIcon: ({ tintColor }) => (
          <Octicons name="project" size={40} color={tintColor} />
        )
      }
    },
    Customers: {
      screen: Customers,
      navigationOptions: {
        tabBarIcon: ({ tintColor }) => (
          <AntDesign name="customerservice" size={40} color={tintColor} />
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
    },
    Agenda: {
      screen: Agenda,
      navigationOptions: {
        tabBarIcon: ({ tintColor }) => (
          <AntDesign name="calendar" size={40} color={tintColor} />
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
