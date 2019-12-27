import {
  TEAMTRACKING_FETCH_SUCCESS,
  TEAMTRACKING_FETCH_START,
  TEAMTRACKING_FETCH_FAIL
} from "../actions/actionTypes";

const initialUserState = {
  data: [],
  loading: false,
  error: null,
  reload: false
};

export const teamTrackingReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case TEAMTRACKING_FETCH_START:
      return {
        ...state,
        loading: true
      };
    case TEAMTRACKING_FETCH_SUCCESS:
      return {
        ...state,
        data: action.data,
        loading: false,
        reload: action.reload
      };
    case TEAMTRACKING_FETCH_FAIL:
      return {
        ...state,
        error: action.error,
        loading: false
      };
    default:
      return state;
  }
};
