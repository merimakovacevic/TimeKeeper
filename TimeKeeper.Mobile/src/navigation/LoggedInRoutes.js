// import React from 'react';

// import { createBottomTabNavigator } from "react-navigation-tabs";
// import {createDrawerNavigator} from "react-navigation-drawer"
// import { createStackNavigator } from "react-navigation-stack";

// import People from "../views/People";
// import Profile from "../views/Profile";
// import Home from "../views/Home"
// import Customers from '../views/Customer'
// import Projects from "../views/Project"
// import Teams from "../views/Team"
// import EmployeeProfile from '../views/EmployeeProfile'

// import Icon from 'react-native-vector-icons/FontAwesome';

// const StackNavigator = createStackNavigator({

//   Profile: {
//     screen: Profile
//   }

// });

// const DrawerNavigator=createDrawerNavigator({

//   Employees: {
//     screen: People
//   },
//   Customers: {
//     screen: Customers
//   },
//   Projects: {
//     screen: Projects
//   },
//   Teams: {
//     screen: Teams
//   },
// })

// const StackNavigatorEmployee = createStackNavigator({
//   EMPLOYEES: {
//     screen: People
//   },
//   EmployeeProfile: {
//     screen: EmployeeProfile
//   }
// });

// const LoggedInRoutes = createBottomTabNavigator({
//   Home: {
//     screen: Home,
//     navigationOptions:{
//       tabBarLabel:'Home',
//       tabBarIcon:({})=>(
//         <Icon name="home" size={25} color="#0C4BB5" />
//       )
//     }
//   },
//   Data: {
//     screen: DrawerNavigator,
//     navigationOptions:{
//       tabBarLabel:'Data',
//       tabBarIcon:({})=>(
//         <Icon name="list" size={25} color="#0C4BB5" />
//       )
//     }
//   },
//   Profile: {
//     screen: StackNavigator,
//     navigationOptions:{
//       tabBarLabel:'Profile',
//       tabBarIcon:({})=>(
//         <Icon name="user" size={25} color="#0C4BB5" />
//       )
//     }
//   }
// });

// export default LoggedInRoutes;

import React from "react";
import { createBottomTabNavigator } from "react-navigation-tabs";
import {} from "@expo/vector-icons";
import { createStackNavigator } from "react-navigation-stack";
import People from "../views/People";
import Profile from "../views/Profile";
import Projects from "../views/Projects";
import Customers from "../views/Customers.js";
import Calendar from "../views/Calendar";
import { createDrawerNavigator } from "react-navigation-drawer";
import Icon from "react-native-vector-icons/FontAwesome";
import EmployeeProfile from "../views/EmployeeProfile";
import { Ionicons } from "@expo/vector-icons";
import Agenda from "../views/Agenda";

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
  AGENDA: {
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
