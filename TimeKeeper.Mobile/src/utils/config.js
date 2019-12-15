export const config = {
	issuer: "https://demo.identityserver.io",
	clientId: "native.code",
	redirectUrl: "io.identityserver.demo:/oauthredirect",
	scopes: ["openid", "profile", "offline_access"]
};

//exp://192.168.30.145:19000

// export const config = {
// 	issuer: "https://192.168.1.100:44300",
// 	clientId: "tk2019",
// 	redirectUrl: "http://192.168.1.100:3000/auth-callback",
// 	scopes: ["openid", "profile", "offline_access"]
// };
