import React from "react";
import { Text, TouchableOpacity, StyleSheet } from "react-native";

import theme from "../assets/Theme";

const Button = ({ onPress, children, outline }) => {
	return (
		<TouchableOpacity onPress={onPress} style={outline ? styles.outline : styles.button}>
			{children}
		</TouchableOpacity>
	);
};

const styles = StyleSheet.create({
	button: {
		height: 60,
		width: "100%",
		backgroundColor: theme.COLORS.PRIMARY,
		borderRadius: 5,
		justifyContent: "center"
	},
	outline: {
		width: "100%",
		height: 60,
		backgroundColor: theme.COLORS.TRANSPARENT,
		borderRadius: 5,
		borderColor: theme.COLORS.DEFAULT,
		borderWidth: 1,
		justifyContent: "center"
	}
});

export default Button;
