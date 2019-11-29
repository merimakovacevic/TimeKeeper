import {
  PROJECTS_FETCH_SUCCESS,
  PROJECTS_FETCH_START,
  PROJECTS_FETCH_FAIL,
  PROJECT_SELECTED
} from "../actions/actionTypes";

const initialUserState = {
  data: [],
  loading: false,
  selectedProject: null,
  error: null
};

export const projectsReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case PROJECTS_FETCH_START:
      return {
        ...state,
        loading: true
      };
    case PROJECTS_FETCH_SUCCESS:
      return {
        ...state,
        data: action.data,
        loading: false
      };
    case PROJECTS_FETCH_FAIL:
      return {
        ...state,
        error: action.error,
        loading: false
      };
    case PROJECT_SELECTED:
      return {
        ...state,
        selectedProject: action.id
      };
    default:
      return state;
  }
};
