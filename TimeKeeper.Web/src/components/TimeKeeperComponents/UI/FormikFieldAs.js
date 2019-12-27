import React from "react";
import { TextField } from "@material-ui/core";

export default function CustomFieldComponent(props) {
  // console.log("errors", props.error, "touched", props.touched, "value", props.value);
  console.log("rendered");
  return (
    <TextField
      {...props}
      margin="normal"
      variant="outlined"
      fullWidth={true}
      label={props.label}
      type={props.type}
      rows={props.rows}
      multiline={props.multiline}
      InputLabelProps={props.type === "date" ? { shrink: true } : {}}
    />
  );
}
