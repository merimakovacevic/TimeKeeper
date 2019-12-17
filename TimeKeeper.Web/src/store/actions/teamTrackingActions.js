import {
  TEAMTRACKING_FETCH_START,
  TEAMTRACKING_FETCH_SUCCESS,
  TEAMTRACKING_FETCH_FAIL
} from "./actionTypes";
import { teamTrackingUrl, apiGetAllRequest } from "../../utils/api";

const teamTrackingFetchStart = () => {
  return {
    type: TEAMTRACKING_FETCH_START
  };
};

const teamTrackingFetchSuccess = (data) => {
  return {
    type: TEAMTRACKING_FETCH_SUCCESS,
    data
  };
};

const teamTrackingFetchFail = (error) => {
  return {
    type: TEAMTRACKING_FETCH_FAIL,
    error
  };
};

export const fetchTeamTracking = () => {
  return (dispatch) => {
    dispatch(teamTrackingFetchStart());
    apiGetAllRequest(teamTrackingUrl)
      .then((res) => {
        console.log(res);
        dispatch(teamTrackingFetchSuccess(res.data.data));
      })
      .catch((err) => dispatch(teamTrackingFetchFail(err)));
  };
};
