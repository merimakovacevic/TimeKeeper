import {
  EMPLOYEES_FETCH_START,
  EMPLOYEES_FETCH_SUCCESS,
  EMPLOYEES_FETCH_FAIL,
  EMPLOYEE_FETCH_START,
  EMPLOYEE_FETCH_SUCCESS,
  EMPLOYEE_FETCH_FAIL,
  EMPLOYEE_SELECT,
  EMPLOYEE_CANCEL,
  EMPLOYEE_EDIT_START,
  EMPLOYEE_EDIT_FAIL,
  EMPLOYEE_EDIT_SUCCESS,
  EMPLOYEE_ADD_START,
  EMPLOYEE_ADD_SUCCESS,
  EMPLOYEE_ADD_FAIL,
  EMPLOYEE_DELETE_START,
  EMPLOYEE_DELETE_FAIL,
  EMPLOYEE_DELETE_SUCCESS
} from "./actionTypes";
import {
  employeesUrl,
  apiGetAllRequest,
  apiGetOneRequest,
  apiPutRequest,
  apiPostRequest,
  apiDeleteRequest
} from "../../utils/api";

const employeesFetchStart = () => {
  return {
    type: EMPLOYEES_FETCH_START
  };
};

const employeesFetchSuccess = (data) => {
  return {
    type: EMPLOYEES_FETCH_SUCCESS,
    data
  };
};

const employeesFetchFail = (error) => {
  return {
    type: EMPLOYEES_FETCH_FAIL,
    error
  };
};

export const fetchEmployees = () => {
  return (dispatch) => {
    dispatch(employeesFetchStart());
    apiGetAllRequest(employeesUrl)
      .then((res) => {
        dispatch(employeesFetchSuccess(res.data.data));
      })
      .catch((err) => dispatch(employeesFetchFail(err)));
  };
};

export const employeeSelect = (id, mode) => {
  return {
    type: EMPLOYEE_SELECT,
    id,
    mode
  };
};

const employeeFetchStart = () => {
  return {
    type: EMPLOYEE_FETCH_START
  };
};

const employeeFetchFail = (error) => {
  return {
    type: EMPLOYEE_FETCH_FAIL,
    error
  };
};

const employeeFetchSuccess = (data) => {
  return {
    type: EMPLOYEE_FETCH_SUCCESS,
    data
  };
};

export const fetchEmployee = (id) => {
  return (dispatch) => {
    dispatch(employeeFetchStart());
    apiGetOneRequest(employeesUrl, id)
      .then((res) => {
        return dispatch(employeeFetchSuccess(res.data.data));
      })
      .catch((err) => dispatch(employeeFetchFail(err)));
  };
};

export const employeeCancel = () => {
  return {
    type: EMPLOYEE_CANCEL
  };
};

const employeeEditStart = () => {
  return {
    type: EMPLOYEE_EDIT_START
  };
};

const employeeEditFail = (error) => {
  return {
    type: EMPLOYEE_EDIT_FAIL,
    error
  };
};

const employeeEditSuccess = () => {
  return {
    type: EMPLOYEE_EDIT_SUCCESS,
    reload: "employeeEditReload"
  };
};

export const employeePut = (id, body) => {
  return (dispatch) => {
    dispatch(employeeEditStart());
    apiPutRequest(employeesUrl, id, body)
      .then((res) => {
        dispatch(employeeEditSuccess());
        dispatch(employeeCancel());
      })
      .catch((err) => {
        dispatch(employeeEditFail(err));
      });
  };
};

const employeeAddStart = () => {
  return {
    type: EMPLOYEE_ADD_START
  };
};

const employeeAddFail = (error) => {
  return {
    type: EMPLOYEE_ADD_FAIL,
    error
  };
};

const employeeAddSuccess = () => {
  return {
    type: EMPLOYEE_ADD_SUCCESS,
    reload: "employeeAddReload"
  };
};

export const employeeAdd = (body) => {
  return (dispatch) => {
    dispatch(employeeAddStart());
    apiPostRequest(employeesUrl, body)
      .then((res) => {
        dispatch(employeeAddSuccess());
        dispatch(employeeCancel());
      })
      .catch((err) => dispatch(employeeAddFail(err)));
  };
};

const employeeDeleteStart = () => {
  return {
    type: EMPLOYEE_DELETE_START
  };
};

const employeeDeleteFail = (error) => {
  return {
    type: EMPLOYEE_DELETE_FAIL,
    error
  };
};

const employeeDeleteSuccess = () => {
  return {
    type: EMPLOYEE_DELETE_SUCCESS,
    reload: "employeeDeleteReload"
  };
};

export const employeeDelete = (id) => {
  return (dispatch) => {
    dispatch(employeeDeleteStart());
    apiDeleteRequest(employeesUrl, id)
      .then((res) => {
        dispatch(employeeDeleteSuccess());
        dispatch(employeeCancel());
      })
      .catch((err) => dispatch(employeeDeleteFail(err)));
  };
};
