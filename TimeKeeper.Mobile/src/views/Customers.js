import React, { Component } from "react";
import { View, Text } from "react-native";
import List from "../components/List";
import Constants from "expo-constants";
import { Icon, Header, Left } from "native-base";

const DATA = [
  {
    id: "1",
    title: "Berina Omerasevic",
    description: "berkica@gmail.com"
  },
  {
    id: "2",
    title: "Hamza Crnovrsanin",
    description: "hamzic@gmail.com"
  },
  {
    id: "3",
    title: "Ajdin Zorlak",
    description: "zoka@gmail.com"
  },
  {
    id: "4",
    title: "Amina Muzurovic",
    description: "muzi@gmail.com"
  },
  {
    id: "5",
    title: "Faris Spica",
    description: "spica_u_vodi@gmail.com"
  },
  {
    id: "6",
    title: "Tajib Smajlovic",
    description: "tajci_rajif@gmail.com"
  },
  {
    id: "7",
    title: "Ferhat Avdic",
    description: "wannabe_rajif@gmail.com"
  },
  {
    id: "9",
    title: "Amra Rovcanin",
    description: "duck_whisperer@gmail.com"
  }
];

export default class People extends Component {
  static navigationOptions = {
    header: null
  };
  state = {
    result: []
  };
  constructor(props) {
    super(props);
    this.state = {
      data: DATA
    };
  }

  _onSelectUser = (id) => {
    this.props.navigation.navigate("EmployeeProfile", {
      id: id,
      type: "employee"
    });
  };

  render() {
    const { navigate } = this.props.navigation;
    const { result } = this.state;
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
          <Text style={styles.header}>CUSTOMERS</Text>
        </Header>
        <List data={this.state.data} onPress={this._onSelectUser} />
      </View>
    );
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
    marginLeft: -65
  }
};
