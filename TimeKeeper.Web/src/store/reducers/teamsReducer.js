import {
  DROPDOWNTEAM_FETCH_SUCCESS,
  DROPDOWNTEAM_FETCH_START,
  DROPDOWNTEAM_FETCH_FAIL,
  DROPDOWNTEAM_SELECT
} from "../actions/actionTypes";

const initialUserState = {
  data: [],
  loading: false,
  error: null,
  selectedTeam: 1,
  reload: false
};

export const teamsReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case DROPDOWNTEAM_FETCH_START:
      return {
        ...state,
        loading: true
      };
    case DROPDOWNTEAM_FETCH_SUCCESS:
      return {
        ...state,
        data: action.data,
        loading: false,
        reload: action.reload
      };
    case DROPDOWNTEAM_FETCH_FAIL:
      return {
        ...state,
        error: action.error,
        loading: false
      };
    case DROPDOWNTEAM_SELECT:
      return {
        ...state,
        selectedTeam: action.id
      };
    default:
      return state;
  }
};
