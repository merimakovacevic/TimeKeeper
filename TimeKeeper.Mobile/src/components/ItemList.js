import React from "react";
import { TouchableOpacity, StyleSheet, Text, Image, View } from "react-native";
import Icon from "react-native-vector-icons/FontAwesome";

import UserIcon from "../assets/images/user.png";

function Item({ id, title, description, selected, onSelect }) {
	return (
		<TouchableOpacity>
			<View style={styles.row}>
				<Image source={UserIcon} style={styles.pic} />
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
					<Icon name="chevron-right" size={20} style={styles.moreIcon} />
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
		color: "#32aedc"
	}
});

export { Item };
