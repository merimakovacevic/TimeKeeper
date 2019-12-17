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
    marginTop: theme.spacing.unit * 2
  }
});
let counter = 0;
function createData(name) {
  counter += 1;
  return {
    id: counter,
    name
  };
}

const DropDownTeam = (props) => {
  const { classes } = props;
  const { fetchDropDownTeam } = props;
  const { dropdownTeamSelect } = props;
  const { data, selected, reload } = props;
  const [teams, setTeams] = useState([]);

  // let teams = [];
  let selectedTeam = null;

  useEffect(() => {
    fetchDropDownTeam();
    setTeams(data);
    console.log("ovo je data", data);
    console.log("ovo je teams aaa teams  ", teams);
  }, [reload]);

  /*  handleChange = (event) => {
    this.setState({ [event.target.name]: event.target.value });
  };
 */
  /* onClickV = () => {
      var lang = this.dropdown.value;đž
     var teamName = "sakkasd";
     this.props.onClickDrop(teamName);
   }; */
  /* 
  onClickDrop = (name) => (event) => {
    var value = event.target.value;
 */
  /*   this.setState({
    selectedId: value
  });
  this.props.onClickDrop(value);
  console.log(this.state.selectedId);
}; */

  return (
    <form className={classes.root} autoComplete="off">
      <FormControl className={classes.formControl}>
        <InputLabel htmlFor="age-simple">Team</InputLabel>
        <Select
          name="selectOptions"
          //onChange={this.onClickDrop("selectOptions")}
          /*   onClick={() => this.props.onClickDrop(this.state.selectedId)} */
          inputProps={{}}
        >
          {/* <MenuItem value={this.state.teams}>
               <em>None</em>
             </MenuItem> */}
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
