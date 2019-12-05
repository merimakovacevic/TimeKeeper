import React, { Component } from "react";
import List from "../components/List";
import SafeAreaView from "react-native-safe-area-view";
import { Header } from "react-native-elements";

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
  },
  {
    id: "11",
    title: "Berina Omerasevic",
    description: "berkica@gmail.com"
  },
  {
    id: "21",
    title: "Hamza Crnovrsanin",
    description: "hamzic@gmail.com"
  },
  {
    id: "31",
    title: "Ajdin Zorlak",
    description: "zoka@gmail.com"
  },
  {
    id: "44",
    title: "Amina Muzurovic",
    description: "muzi@gmail.com"
  }
];

export default class People extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: DATA
    };
  }

  render() {
    return (
      <SafeAreaView>
        <Header
          centerComponent={{ text: "People", style: { color: "#fff" } }}
        />

        <List data={this.state.data} />
      </SafeAreaView>
    );
  }
}
