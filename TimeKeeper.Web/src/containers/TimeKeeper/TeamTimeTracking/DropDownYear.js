import React from "react";
import { withStyles } from "@material-ui/core/styles";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import { yearSelect } from "../../../store/actions/index";
import { connect } from "react-redux";

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
  }
});

const DropDownYear = (props) => {
  const { classes, yearSelect } = props;

  return (
    <form className={classes.root} autoComplete="off">
      <FormControl className={classes.formControl}>
        <InputLabel shrink htmlFor="circle">
          Year
        </InputLabel>
        <Select
          classes={{
            icon: classes.whiteColor
          }}
          className={classes.background}
          onChange={(e) => yearSelect(e.target.value)}
          inputProps={{}}
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
};

const mapStateToProps = (state) => {
  return {
    data: state.selectedYear,
    selected: state.selectedYear
  };
};

export default connect(mapStateToProps, {
  yearSelect
})(withStyles(styles)(DropDownYear));
