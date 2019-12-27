import { connect } from "react-redux";
import {
  fetchDropDownTeam,
  dropdownTeamSelect
} from "../../../store/actions/index";
import { withStyles } from "@material-ui/core/styles";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import React, { useEffect, useState } from "react";

const styles = (theme) => ({
  root: {
    display: "flex",
    flexWrap: "wrap"
  },
  formControl: {
    margin: theme.spacing.unit,
    minWidth: 120
  },
  selectEmpty: {
    marginTop: theme.spacing(2)
  },
  whiteColor: {
    color: "white"
  },
  label: {
    color: "white"
  }
});

const DropDownTeam = (props) => {
  const { classes } = props;
  const { fetchDropDownTeam } = props;
  const { dropdownTeamSelect } = props;
  const { data, reload } = props;
  const [teams, setTeams] = useState([]);

  useEffect(() => {
    fetchDropDownTeam();
    setTeams(data);
  }, [reload]);

  return (
    <form className={classes.root} autoComplete="off">
      <FormControl className={classes.formControl}>
        <InputLabel shrink htmlFor="circle" className={classes.label}>
          Team
        </InputLabel>
        {console.log("TEAMS", teams)}
        <Select
         className={classes.label}
          defaultValue={1}
          classes={{
            icon: classes.whiteColor
          }}
          onChange={(e) => dropdownTeamSelect(e.target.value)}
          inputProps={{}}
        >
          {teams.map((team) => (
            <MenuItem key={team.id} value={team.id}>
              {team.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </form>
  );
};

const mapStateToProps = (state) => {
  return {
    data: state.teams.data,
    selected: state.selectedTeam,
    reload: state.teams.reload
  };
};

export default connect(mapStateToProps, {
  fetchDropDownTeam,
  dropdownTeamSelect
})(withStyles(styles)(DropDownTeam));
