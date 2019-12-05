import React, { Component } from "react";
import {
  SafeAreaView,
  TouchableOpacity,
  FlatList,
  StyleSheet,
  Text,
  Image
} from "react-native";
import Constants from "expo-constants";
import { Item } from "./ItemList.js";

/* 
const DATA = [
  {
    id: '1',
    title: 'Berina Omerasevic',    
    description: 'berkica@gmail.com'
  },
  {
    id: '2',
    title: 'Hamza Crnovrsanin',
    description: 'hamzic@gmail.com'
  },
  {
    id: '3',
    title: 'Ajdin Zorlak',
    description: 'zoka@gmail.com'
  },
  {
    id: '4',
    title: 'Amina Muzurovic',
    description: 'muzi@gmail.com'
  },
  {
    id: '5',
    title: 'Faris Spica',
    description: 'spica_u_vodi@gmail.com'
  },
  {
    id: '6',
    title: 'Tajib Smajlovic',
    description: 'tajci_rajif@gmail.com'
  },
  {
    id: '7',
    title: 'Ferhat Avdic',
    description: 'wannabe_rajif@gmail.com'
  },
  {
    id: '9',
    title: 'Amra Rovcanin',
    description: 'duck_whisperer@gmail.com'
  },
];
  */

/* export default function App() {
 */

export default class List extends Component {
  render() {
    return (
      <SafeAreaView>
        <FlatList
          data={this.props.data}
          renderItem={({ item }) => (
            <Item
              id={item.id}
              title={item.title}
              description={item.description}
            />
          )}
          keyExtractor={(item) => item.id}
        />
      </SafeAreaView>
    );
  }
}
