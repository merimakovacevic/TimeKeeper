import React from "react";
import { TouchableOpacity, StyleSheet, Text, Image, View } from "react-native";
import UserIcon from "../assets/images/user.png";
import CustomerIcon from "../assets/images/customers.png";
import ProjectsIcon from "../assets/images/projects.png";
import TeamsIcon from "../assets/images/teams.png";
import { Feather } from "@expo/vector-icons";

function Item({ id, title, description, selected, onSelect, type }) {
	return (
		<TouchableOpacity
			onPress={() => onSelect(id)}
			style={[styles.item, { backgroundColor: selected ? "#bae2e3" : "white" }]}
		>
			<View style={styles.row}>
				<Image
					source={
						type === "customers"
							? CustomerIcon
							: type === "projects"
							? ProjectsIcon
							: type === "teams"
							? TeamsIcon
							: UserIcon
					}
					style={styles.pic}
				/>
				<View>
					<View style={styles.nameContainer}>
						<Text style={styles.nameTxt} numberOfLines={1} ellipsizeMode="tail">
							{title}
						</Text>
					</View>
					<View style={styles.msgContainer}>
						<Text style={styles.msgTxt}>{description}</Text>
					</View>
				</View>
				<View style={styles.moreContainer}>
					{type === "customers" || type === "projects" || type === "teams" ? null : (
						<Feather name="chevron-right" size={30} style={styles.moreIcon} />
					)}
				</View>
			</View>
		</TouchableOpacity>
	);
}

const styles = StyleSheet.create({
	row: {
		flexDirection: "row",
		alignItems: "center",
		justifyContent: "space-between",
		borderColor: "#DCDCDC",
		backgroundColor: "#fff",
		borderBottomWidth: 1,
		padding: 10,
		margin: 5
	},
	pic: {
		width: 50,
		height: 50
	},
	nameContainer: {
		flexDirection: "row",
		justifyContent: "space-between",
		width: 280
	},
	nameTxt: {
		marginLeft: 15,
		fontWeight: "600",
		color: "#222",
		fontSize: 18,
		width: 170
	},

	msgContainer: {
		flexDirection: "row",
		alignItems: "center"
	},
	msgTxt: {
		fontWeight: "400",
		color: "#008B8B",
		fontSize: 12,
		marginLeft: 15
	},
	moreIcon: {
		color: "#32aedc",
		right: 13
	}
});

export { Item };
