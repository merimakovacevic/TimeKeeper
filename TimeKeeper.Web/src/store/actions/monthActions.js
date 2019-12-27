import { DROPDOWNMONTH_SELECT } from "./actionTypes";

export const monthSelect = (id) => {
  return {
    type: DROPDOWNMONTH_SELECT,
    id
  };
};
