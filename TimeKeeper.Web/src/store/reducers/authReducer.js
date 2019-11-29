import {} from "../actions/actionTypes";

const initialUserState = {
	user: null,
	error: false
};

export const userReducer = (state = initialUserState, action) => {
	switch (action.type) {
		default:
			return state;
	}
};
