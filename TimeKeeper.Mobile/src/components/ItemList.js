import React from "react";
import {
  SafeAreaView,
  TouchableOpacity,
  FlatList,
  StyleSheet,
  Text,
  Image,
  View
} from "react-native";
import Constants from "expo-constants";
import Icon from "react-native-vector-icons/FontAwesome";

function Item({ id, title, description, selected, onSelect }) {
  return (
    <TouchableOpacity
      onPress={() => onSelect(id)}
      style={[
        styles.item,
        { backgroundColor: selected ? "#bae2e3" : "#99bbff" }
      ]}
    >
      <Image
        style={styles.image}
        source={{
          uri:
            "https://cdn1.iconfinder.com/data/icons/technology-devices-2/100/Profile-512.png"
        }}
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
    backgroundColor: "lightcyan",
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
    top: 10,
    left: 90,
    color: "black"
  },
  description: {
    fontSize: 12,
    color: "black",
    position: "absolute",
    top: 40,
    left: 90
  },
  image: {
    width: 40,
    height: 40,
    bottom: 10
  },
  moreIcon: {
    color: "black"
  }
});
export { Item };
