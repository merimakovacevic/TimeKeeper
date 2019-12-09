import React from "react";
import { SafeAreaView, TouchableOpacity, FlatList, StyleSheet, Text, Image } from "react-native";
import Constants from "expo-constants";

function Item({ id, title, description, selected, onSelect }) {
	return (
		<TouchableOpacity
			onPress={() => onSelect(id)}
			style={[styles.item, { backgroundColor: selected ? "#bae2e3" : "lightcyan" }]}
		>
			<Image
				style={styles.image}
				source={{ uri: "https://cdn1.iconfinder.com/data/icons/technology-devices-2/100/Profile-512.png" }}
			/>
			<Text style={styles.title}>{title}</Text>
			<Text style={styles.description}>{description}</Text>
		</TouchableOpacity>
	);
}

const styles = StyleSheet.create({
	item: {
		backgroundColor: "lightcyan",
		padding: 20,
		marginVertical: 8,
		marginHorizontal: 16,
		display: "flex",
		flexDirection: "row",
		justifyContent: "space-between",
		marginBottom: 3,
		height: 100
	},
	title: {
		fontSize: 28,
		position: "absolute",
		top: 0,
		left: 100,
		color: "black"
	},
	description: {
		fontSize: 16,
		color: "black",
		position: "absolute",
		top: 60,
		left: 100
	},
	image: {
		width: 50,
		height: 50
	}
});
export { Item };
