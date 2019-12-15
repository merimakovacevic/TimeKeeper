import { createStackNavigator } from "react-navigation-stack";

import Login from "../views/Login";

const LoggedOutRotes = createStackNavigator({
	Login: {
		screen: Login
	}
});

export default LoggedOutRotes;
