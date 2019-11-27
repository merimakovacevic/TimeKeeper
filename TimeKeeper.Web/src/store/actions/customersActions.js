import axios from "axios";

import {
  CUSTOMERS_FETCH_START,
  CUSTOMERS_FETCH_SUCCESS,
  CUSTOMERS_FETCH_FAIL,
  CUSTOMER_SELECTED
} from "./actionTypes";
import config from "../../config";

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
    axios(`${config.apiUrl}customers`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: config.token
      }
    })
      .then((res) => {
        dispatch(customersFetchSuccess(res.data));
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
