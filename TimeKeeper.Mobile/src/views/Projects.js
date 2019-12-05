import React, { Component } from "react";
import { Text, View } from "react-native";

import List from "../components/List";
import SafeAreaView from "react-native-safe-area-view";

const DATA = [
  {
    id: "1",
    title: "Project11",
    description: "berkica@gmail.com"
  },
  {
    id: "2",
    title: "proroeo",
    description: "hamzic@gmail.com"
  },
  {
    id: "3",
    title: "proororor",
    description: "zoka@gmail.com"
  },
  {
    id: "4",
    title: "Amina prprpprpr",
    description: "muzi@gmail.com"
  },
  {
    id: "5",
    title: "Faris teteetet",
    description: "spica_u_vodi@gmail.com"
  },
  {
    id: "6",
    title: "Tajib tesatsta",
    description: "tajci_rajif@gmail.com"
  },
  {
    id: "7",
    title: "Ferhat Avteeatedic",
    description: "wannabe_rajif@gmail.com"
  },
  {
    id: "9",
    title: "AmrTESTRovcanin",
    description: "duck_whisperer@gmail.com"
  }
];

export default class Projects extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: DATA
    };
  }

  render() {
    return (
      <SafeAreaView>
        <List data={this.state.data} />
      </SafeAreaView>
    );
  }
}
