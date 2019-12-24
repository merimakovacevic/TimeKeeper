import { DROPDOWNMONTH_SELECT } from "../actions/actionTypes";

const initialUserState = {
  selectedMonth: 1
};

export const monthReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case DROPDOWNMONTH_SELECT:
      return {
        ...state,
        selectedMonth: action.id
      };
    default:
      return state;
  }
};
