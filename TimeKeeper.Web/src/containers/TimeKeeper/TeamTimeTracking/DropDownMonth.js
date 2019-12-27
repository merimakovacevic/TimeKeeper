import React from "react";
import { withStyles } from "@material-ui/core/styles";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import { monthSelect } from "../../../store/actions/index";
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
  },
  label: {
    color: "white"
  }
});

const DropDownMonth = (props) => {
  const { classes } = props;

  const { monthSelect } = props;
  return (
    <form className={classes.root} autoComplete="off">
      <FormControl className={classes.formControl}>
        <InputLabel shrink htmlFor="circle" className={classes.label}>
          Month
        </InputLabel>{" "}
        <Select
          defaultValue={1}
          classes={{
            icon: classes.whiteColor
          }}
          className={classes.label}
          onChange={(e) => monthSelect(e.target.value)}
          inputProps={{}}
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
};

const mapStateToProps = (state) => {
  return {
    data: state.selectedMonth
  };
};

export default connect(mapStateToProps, {
  monthSelect
})(withStyles(styles)(DropDownMonth));
