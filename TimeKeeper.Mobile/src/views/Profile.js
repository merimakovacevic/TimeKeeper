import React, { Component } from "react";
import { Text, View } from "react-native";
import { RNModal } from "rn-start-elements";

export default class Profile extends Component {
  render() {
    return (
      <View>
        <RNModal visible>
          <View>
            <Text>Modal Content !!!</Text>
          </View>
        </RNModal>{" "}
      </View>
    );
  }
}
