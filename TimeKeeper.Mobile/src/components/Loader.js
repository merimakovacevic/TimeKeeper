import React from "react";
import { ActivityIndicator } from "react-native";

const Loader = ({ size = "large", color = "green" }) => <ActivityIndicator size={size} color={color} />;

export default Loader;
