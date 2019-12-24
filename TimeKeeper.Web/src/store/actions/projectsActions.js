import {
  PROJECTS_FETCH_START,
  PROJECTS_FETCH_SUCCESS,
  PROJECTS_FETCH_FAIL,
  PROJECT_SELECTED,
  PROJECT_FETCH_START,
  PROJECT_FETCH_SUCCESS,
  PROJECT_FETCH_FAIL,
  PROJECT_SELECT,
  PROJECT_CANCEL,
  PROJECT_EDIT_START,
  PROJECT_EDIT_FAIL,
  PROJECT_EDIT_SUCCESS,
  PROJECT_ADD_START,
  PROJECT_ADD_SUCCESS,
  PROJECT_ADD_FAIL,
  PROJECT_DELETE_START,
  PROJECT_DELETE_FAIL,
  PROJECT_DELETE_SUCCESS
} from "./actionTypes";

import {
  projectsUrl,
  apiGetAllRequest,
  apiGetOneRequest,
  apiPutRequest,
  apiPostRequest,
  apiDeleteRequest
} from "../../utils/api";

const projectsFetchStart = () => {
  return {
    type: PROJECTS_FETCH_START
  };
};

const projectsFetchSuccess = (data) => {
  return {
    type: PROJECTS_FETCH_SUCCESS,
    data
  };
};

const projectsFetchFail = (error) => {
  return {
    type: PROJECTS_FETCH_FAIL,
    error
  };
};

export const fetchProjects = () => {
  return (dispatch) => {
    dispatch(projectsFetchStart());
    apiGetAllRequest(projectsUrl)
      .then((res) => {
        console.log(res);
        dispatch(projectsFetchSuccess(res.data.data));
      })
      .catch((err) => dispatch(projectsFetchFail(err)));
  };
};

export const projectSelect = (id) => {
  return {
    type: PROJECT_SELECTED,
    id
  };
};

const projectFetchStart = () => {
  return {
    type: PROJECT_FETCH_START
  };
};

const projectFetchFail = (error) => {
  return {
    type: PROJECT_FETCH_FAIL,
    error
  };
};

const projectFetchSuccess = (data) => {
  return {
    type: PROJECT_FETCH_SUCCESS,
    data
  };
};

export const fetchProject = (id) => {
  return (dispatch) => {
    dispatch(projectFetchStart());
    apiGetOneRequest(projectsUrl, id)
      .then((res) => {
        return dispatch(projectFetchSuccess(res.data.data));
      })
      .catch((err) => dispatch(projectFetchFail(err)));
  };
};

export const projectCancel = () => {
  return {
    type: PROJECT_CANCEL
  };
};

const projectEditStart = () => {
  return {
    type: PROJECT_EDIT_START
  };
};

const projectEditFail = (error) => {
  return {
    type: PROJECT_EDIT_FAIL,
    error
  };
};

const projectEditSuccess = () => {
  return {
    type: PROJECT_EDIT_SUCCESS,
    reload: "projectEditReload"
  };
};

export const projectPut = (id, body) => {
  return (dispatch) => {
    dispatch(projectEditStart());
    apiPutRequest(projectsUrl, id, body)
      .then((res) => {
        dispatch(projectEditSuccess());
        dispatch(projectCancel());
      })
      .catch((err) => {
        dispatch(projectEditFail(err));
      });
  };
};

const projectAddStart = () => {
  return {
    type: PROJECT_ADD_START
  };
};

const projectAddFail = (error) => {
  return {
    type: PROJECT_ADD_FAIL,
    error
  };
};

const projectAddSuccess = () => {
  return {
    type: PROJECT_ADD_SUCCESS,
    reload: "projectAddReload"
  };
};

export const projectAdd = (body) => {
  return (dispatch) => {
    dispatch(projectAddStart());
    apiPostRequest(projectsUrl, body)
      .then((res) => {
        dispatch(projectAddSuccess());
        dispatch(projectCancel());
      })
      .catch((err) => dispatch(projectAddFail(err)));
  };
};

const projectDeleteStart = () => {
  return {
    type: PROJECT_DELETE_START
  };
};

const projectDeleteFail = (error) => {
  return {
    type: PROJECT_DELETE_FAIL,
    error
  };
};

const projectDeleteSuccess = () => {
  return {
    type: PROJECT_DELETE_SUCCESS,
    reload: "projectDeleteReload"
  };
};

export const projectDelete = (id) => {
  return (dispatch) => {
    dispatch(projectDeleteStart());
    apiDeleteRequest(projectsUrl, id)
      .then((res) => {
        dispatch(projectDeleteSuccess());
        dispatch(projectCancel());
      })
      .catch((err) => dispatch(projectDeleteFail(err)));
  };
};
