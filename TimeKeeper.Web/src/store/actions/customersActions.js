import {
  CUSTOMERS_FETCH_START,
  CUSTOMERS_FETCH_SUCCESS,
  CUSTOMERS_FETCH_FAIL,
  CUSTOMER_SELECTED
} from "./actionTypes";
import { customersUrl, apiGetAllRequest } from "../../utils/api";

const customersFetchStart = () => {
  return {
    type: CUSTOMERS_FETCH_START
  };
};

const customersFetchSuccess = (data) => {
  return {
    type: CUSTOMERS_FETCH_SUCCESS,
    data
  };
};

const customersFetchFail = (error) => {
  return {
    type: CUSTOMERS_FETCH_FAIL,
    error
  };
};

export const fetchCustomers = () => {
  return (dispatch) => {
    dispatch(customersFetchStart());
    apiGetAllRequest(customersUrl)
      .then((res) => {
        dispatch(customersFetchSuccess(res.data.data));
      })
      .catch((err) => dispatch(customersFetchFail(err)));
  };
};

export const customerSelect = (id) => {
  return {
    type: CUSTOMER_SELECTED,
    id
  };
};
