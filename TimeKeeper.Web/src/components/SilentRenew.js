import React from "react";
import { AuthConsumer } from "./AuthContext";
export const SilentRenew = () => (
	<AuthConsumer>
		{({ signinSilentCallback }) => {
			signinSilentCallback();
			return <span>loading</span>;
		}}
	</AuthConsumer>
);
