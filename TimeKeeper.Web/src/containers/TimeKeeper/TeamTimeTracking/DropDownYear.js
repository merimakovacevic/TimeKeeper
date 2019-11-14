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

class DropDownYear extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      labelWidth: 0,
      selectedYear: null
    };
    this.onClickDrop = this.onClickDrop.bind(this);
  }

  componentDidMount() {}

  onClickDrop = name => event => {
    var value = event.target.value;

    this.setState({
      selectedMonth: value
    });
    this.props.onClickDrop(value);
    console.log(this.state.selectedMonth);
  };

  handleChange = event => {
    this.setState({ [event.target.name]: event.target.value });
  };

  render() {
    const { classes } = this.props;
    let teams = this.state.teams;
    return (
      <form className={classes.root} autoComplete="off">
        <FormControl className={classes.formControl}>
          <InputLabel htmlFor="age-simple">Year</InputLabel>
          <Select
            name="selectOptions"
            onChange={this.onClickDrop("selectOptions")}
            onClick={this.state.handlerYear}
            inputProps={{
              name: "age",
              id: "age-simple"
            }}
          >
            <MenuItem key={2019} value={2019}>
              {"2019"}
            </MenuItem>
            <MenuItem key={2018} value={2018}>
              {"2018"}
            </MenuItem>
            <MenuItem key={2017} value={2017}>
              {"2017"}
            </MenuItem>
          </Select>
        </FormControl>
      </form>
    );
  }
}

DropDownYear.propTypes = {
  classes: PropTypes.object.isRequired
};
export default withStyles(styles)(DropDownYear);
