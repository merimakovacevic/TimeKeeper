import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { OidcProvider } from "redux-oidc";

import configureStore from "./store/configureStore";
import userManager from "./utils/userManager";

import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";

export const store = configureStore();

ReactDOM.render(
	<Provider store={store}>
		<OidcProvider store={store} userManager={userManager}>
			<BrowserRouter>
				<App />
			</BrowserRouter>
		</OidcProvider>
	</Provider>,
	document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
