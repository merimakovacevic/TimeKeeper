import { DROPDOWNYEAR_SELECT } from "../actions/actionTypes";

const initialUserState = {
  selected: 2019
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
