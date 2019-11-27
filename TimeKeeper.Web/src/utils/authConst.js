export const IDENTITY_CONFIG = {
	authority: "https://localhost:44300",
	client_id: "tk2019",
	redirect_uri: "http://localhost:3000/auth-callback",
	post_logout_redirect_uri: "http://localhost:3000",
	response_type: "id_token token",
	scope: "openid profile roles names timekeeper",
	filterProtocolClaims: true,
	loadUserInfo: true,
	automaticSilentRenew: true,
	silent_redirect_uri: "http://localhost:3000/app"
};

let issuer = "https://localhost:44300";

export const METADATA_OIDC = {
	issuer: "https://localhost:44300",
	jwks_uri: issuer + "/.well-known/openid-configuration/jwks",
	authorization_endpoint: issuer + "/connect/authorize",
	token_endpoint: issuer + "/connect/token",
	userinfo_endpoint: issuer + "/connect/userinfo",
	end_session_endpoint: issuer + "/connect/endsession",
	check_session_iframe: issuer + "/connect/checksession",
	revocation_endpoint: issuer + "/connect/revocation",
	introspection_endpoint: issuer + "/connect/introspect"
};
