import React from "react";
import {
  TouchableOpacity,
  StyleSheet,
  Text,
  Image,
  View
} from "react-native";
import Icon from "react-native-vector-icons/FontAwesome";

import UserIcon from '../assets/images/user.png'

function Item({ id, title, description, selected, onSelect }) {
  return (
    <TouchableOpacity
      style={styles.item}
    >
      <Image
        style={styles.image}
        source={UserIcon}
      />
      <Text style={styles.title}>{title}</Text>
      <Text style={styles.description}>{description}</Text>
      <View style={styles.moreContainer}>
        <Icon name="chevron-right" size={20} style={styles.moreIcon} />
      </View>
    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({
  item: {
    backgroundColor: "rgba(0,99,255,0.1)",
    padding: 20,
    marginVertical: 5,
    marginHorizontal: 8,
    display: "flex",
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: 0.7,
    height: 60
  },
  title: {
    fontSize: 20,
    position: "absolute",
    top: 5,
    left: 90,
    color: "black"
  },
  description: {
    fontSize: 12,
    color: "black",
    position: "absolute",
    top: 33,
    left: 90
  },
  image: {
    width: 40,
    height: 40,
    bottom: 10
  },
  moreIcon: {
    color: "#32aedc"
  }
});

export { Item };
