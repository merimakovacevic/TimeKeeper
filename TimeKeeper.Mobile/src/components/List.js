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
