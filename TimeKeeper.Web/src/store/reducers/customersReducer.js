import {
  CUSTOMERS_FETCH_SUCCESS,
  CUSTOMERS_FETCH_START,
  CUSTOMERS_FETCH_FAIL,
  CUSTOMER_SELECTED
} from "../actions/actionTypes";

const initialUserState = {
  data: [],
  loading: false,
  selectedCustomer: null,
  error: null
};

export const customersReducer = (state = initialUserState, action) => {
  switch (action.type) {
    case CUSTOMERS_FETCH_START:
      return {
        ...state,
        loading: true
      };
    case CUSTOMERS_FETCH_SUCCESS:
      return {
        ...state,
        data: action.data,
        loading: false
      };
    case CUSTOMERS_FETCH_FAIL:
      return {
        ...state,
        error: action.error,
        loading: false
      };
    case CUSTOMER_SELECTED:
      return {
        ...state,
        selectedCustomer: action.id
      };
    default:
      return state;
  }
};
