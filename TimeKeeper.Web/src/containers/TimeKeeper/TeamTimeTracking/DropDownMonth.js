import React from "react";
import ReactDOM from "react-dom";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import Input from "@material-ui/core/Input";
import OutlinedInput from "@material-ui/core/OutlinedInput";
import FilledInput from "@material-ui/core/FilledInput";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import axios from "axios";
import classNames from "classnames";
import config from "../../../config";

const styles = theme => ({
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

class DropDownMonth extends React.Component {
  state = {
    age: "",
    name: "hai",
    labelWidth: 0
  };

  componentDidMount() {}

  handleChange = event => {
    this.setState({ [event.target.name]: event.target.value });
  };

  render() {
    const { classes } = this.props;
    let teams = this.state.teams;
    return (
      <form className={classes.root} autoComplete="off">
        <FormControl className={classes.formControl}>
          <InputLabel htmlFor="age-simple">Month</InputLabel>
          <Select
            onChange={this.handleChange}
            inputProps={{
              name: "age",
              id: "age-simple"
            }}
          >
            <MenuItem key={1} value={1}>
              {"January"}
            </MenuItem>
            <MenuItem key={2} value={2}>
              {"February"}
            </MenuItem>
            <MenuItem key={3} value={3}>
              {"March"}
            </MenuItem>
            <MenuItem key={4} value={4}>
              {"April"}
            </MenuItem>
            <MenuItem key={5} value={5}>
              {"May"}
            </MenuItem>
            <MenuItem key={6} value={6}>
              {"June"}
            </MenuItem>
            <MenuItem key={7} value={7}>
              {"July"}
            </MenuItem>
            <MenuItem key={8} value={8}>
              {"August"}
            </MenuItem>
            <MenuItem key={9} value={9}>
              {"September"}
            </MenuItem>
            <MenuItem key={10} value={10}>
              {"October"}
            </MenuItem>
            <MenuItem key={11} value={11}>
              {"November"}
            </MenuItem>
            <MenuItem key={12} value={12}>
              {"December"}
            </MenuItem>
          </Select>
        </FormControl>
      </form>
    );
  }
}

DropDownMonth.propTypes = {
  classes: PropTypes.object.isRequired
};
export default withStyles(styles)(DropDownMonth);
