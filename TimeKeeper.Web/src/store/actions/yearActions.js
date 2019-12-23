import { DROPDOWNYEAR_SELECT } from "./actionTypes";

export const yearSelect = (id) => {
  return {
    type: DROPDOWNYEAR_SELECT,
    id
  };
};
