import { DROPDOWNYEAR_SELECT } from "../actions/actionTypes";

const initialUserState = {
  selected: null
};

export const yearReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case DROPDOWNYEAR_SELECT:
      return {
        ...state,
        selected: action.id
      };
    default:
      return state;
  }
};
