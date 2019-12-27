import {
  EMPLOYEES_FETCH_SUCCESS,
  EMPLOYEES_FETCH_START,
  EMPLOYEES_FETCH_FAIL,
  EMPLOYEE_FETCH_FAIL,
  EMPLOYEE_FETCH_START,
  EMPLOYEE_FETCH_SUCCESS,
  EMPLOYEE_SELECT,
  EMPLOYEE_CANCEL,
  EMPLOYEE_EDIT_SUCCESS,
  EMPLOYEE_ADD_START,
  EMPLOYEE_ADD_FAIL,
  EMPLOYEE_ADD_SUCCESS,
  EMPLOYEE_DELETE_FAIL,
  EMPLOYEE_DELETE_START,
  EMPLOYEE_DELETE_SUCCESS
} from "../actions/actionTypes";

const initialUserState = {
  data: [],
  loading: false,
  selected: false,
  employee: null,
  error: null,
  reload: false
};

export const employeesReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case EMPLOYEES_FETCH_START:
      return {
        ...state,
        loading: true
      };
    case EMPLOYEES_FETCH_SUCCESS:
      return {
        ...state,
        data: action.data,
        loading: false
      };
    case EMPLOYEES_FETCH_FAIL:
      return {
        ...state,
        error: action.error,
        loading: false
      };
    case EMPLOYEE_SELECT:
      return {
        ...state,
        selected: {
          id: action.id,
          mode: action.mode
        }
      };
    case EMPLOYEE_FETCH_START:
      return {
        ...state
      };
    case EMPLOYEE_FETCH_SUCCESS:
      return {
        ...state,
        employee: action.data
      };
    case EMPLOYEE_FETCH_FAIL:
      return {
        ...state
      };
    case EMPLOYEE_EDIT_SUCCESS:
      return {
        ...state,
        //   data: action.data.query,
        //   reload: action.reload.query
        //  data: action.data.query,
        reload: action.reload
      };
    case EMPLOYEE_ADD_START:
      return {
        ...state
      };
    case EMPLOYEE_ADD_SUCCESS:
      return {
        ...state,
        //   data: action.data.query,
        //   data: action.data.query,
        reload: action.reload
      };
    case EMPLOYEE_ADD_FAIL:
      return {
        ...state
      };
    case EMPLOYEE_DELETE_START:
      return {
        ...state
      };
    case EMPLOYEE_DELETE_SUCCESS:
      return {
        ...state,
        reload: action.reload
      };
    case EMPLOYEE_DELETE_FAIL:
      return {
        ...state,
        error: action.error
      };
    case EMPLOYEE_CANCEL:
      return {
        ...state,
        employee: null,
        selected: false
      };
    default:
      return state;
  }
};
