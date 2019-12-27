import {
  DROPDOWNTEAM_FETCH_START,
  DROPDOWNTEAM_FETCH_SUCCESS,
  DROPDOWNTEAM_FETCH_FAIL,
  DROPDOWNTEAM_SELECT
} from "./actionTypes";
import { dropDownTeamsUrl, apiGetAllRequest } from "../../utils/api";

const dropdownTeamFetchStart = () => {
  return {
    type: DROPDOWNTEAM_FETCH_START
  };
};

const dropdownTeamFetchSuccess = (data) => {
  return {
    type: DROPDOWNTEAM_FETCH_SUCCESS,
    data,
    reload: "teamFetchReload"
  };
};

const dropdownTeamFetchFail = (error) => {
  return {
    type: DROPDOWNTEAM_FETCH_FAIL,
    error
  };
};

export const fetchDropDownTeam = () => {
  return (dispatch) => {
    dispatch(dropdownTeamFetchStart());
    apiGetAllRequest(dropDownTeamsUrl)
      .then((res) => {
        dispatch(dropdownTeamFetchSuccess(res.data.data));
      })
      .catch((err) => dispatch(dropdownTeamFetchFail(err)));
  };
};

export const dropdownTeamSelect = (id) => {
  return {
    type: DROPDOWNTEAM_SELECT,
    id
  };
};
