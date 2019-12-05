import { createSwitchNavigator } from "react-navigation";
import LoggedInRoutes from "./LoggedInRoutes";
import LoggedOutRoutes from "./LoggedOutRoutes";

export const getRootNavigator = (loggedIn) =>
	createSwitchNavigator(
		{
			LoggedInRoutes: {
				screen: LoggedInRoutes
			},
			LoggedOutRoutes: {
				screen: LoggedOutRoutes
			}
		},
		{
			initialRouteName: loggedIn ? "LoggedInRoutes" : "LoggedOutRoutes"
		}
	);
