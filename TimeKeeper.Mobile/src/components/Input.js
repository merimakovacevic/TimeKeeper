import React from "react";
import { View, TextInput } from "react-native";

export const Input = ({ placeholder, secureTextEntry, onChangeText, name, keyboardType, style, value }) => {
	return (
		<View style={styles.container}>
			<TextInput
				style={[styles.inputBox, style]}
				underlineColorAndroid="rgba(0,0,0,0)"
				placeholder={placeholder}
				placeholderTextColor="#80cbc4"
				secureTextEntry={secureTextEntry}
				onChangeText={onChangeText}
				name={name}
				selectionColor="#80cbc4"
				keyboardType={keyboardType}
				value={value}
			/>
		</View>
	);
};

const styles = {
	inputBox: {
		width: 300,
		backgroundColor: "rgb(63, 66, 71)",
		color: "#80cbc4",
		borderRadius: 20,
		marginBottom: 10,
		paddingHorizontal: 15,
		paddingVertical: 10
	}
};
