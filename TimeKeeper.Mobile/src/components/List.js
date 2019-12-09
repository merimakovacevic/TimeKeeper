import React from "react";
import { SafeAreaView, TouchableOpacity, FlatList, StyleSheet, Text, Image } from "react-native";
import Constants from "expo-constants";
import { Item } from "./ListItem";
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

export default function App() {
	const [selected, setSelected] = React.useState(new Map());

	const onSelect = React.useCallback(
		(id) => {
			const newSelected = new Map(selected);
			newSelected.set(id, !selected.get(id));

			setSelected(newSelected);
		},
		[selected]
	);

	return (
		<SafeAreaView style={styles.container}>
			<FlatList
				data={DATA}
				renderItem={({ item }) => (
					<Item
						style={styles.item}
						id={item.id}
						title={item.title}
						description={item.description}
						selected={!!selected.get(item.id)}
						onSelect={onSelect}
						bottomDivider
						chevron
					/>
				)}
				keyExtractor={(item) => item.id}
				extraData={selected}
			/>
		</SafeAreaView>
	);
}

const styles = StyleSheet.create({
	container: {
		flex: 1,
		marginTop: Constants.statusBarHeight
	},
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
