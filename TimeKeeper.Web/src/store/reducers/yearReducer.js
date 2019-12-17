import { DROPDOWNYEAR_SELECT } from "../actions/actionTypes";

const initialUserState = {
  selectedYear: null
};

export const yearReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case DROPDOWNYEAR_SELECT:
      return {
        ...state,
        selectedYear: action.id
      };
    default:
      return state;
  }
};
