import { createUserManager } from "redux-oidc";

const userManagerConfig = {
	authority: "https://localhost:44300",
	client_id: "tk2019",
	redirect_uri: "http://localhost:3000/auth-callback",
	post_logout_redirect_uri: "http://localhost:3000",
	response_type: "id_token token",
	scope: "openid profile roles names timekeeper",
	filterProtocolClaims: true,
	loadUserInfo: true,
	automaticSilentRenew: true,
	// silent_redirect_uri: "http://localhost:3000/app"
};

const userManager = createUserManager(userManagerConfig);

export default userManager;
