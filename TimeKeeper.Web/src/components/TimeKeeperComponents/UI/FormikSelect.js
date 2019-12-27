import React, { PureComponent, Component, Fragment } from "react";
import { Formik, useField, Field, FastField } from "formik";
import { TextField, MenuItem } from "@material-ui/core";

export default function FormikSelect(props) {
  return (
    <FastField name={props.name}>
      {({
        field, // { name, value, onChange, onBlur }
        meta,
        form
      }) => {
        // console.log("status.value", field.value);
        const handleFieldChange = (e) => {
          form.setFieldValue(field.name, e.target.value, false);
          props.setSelectedValue(e);
        };
        return (
          <TextField
            {...field}
            variant="outlined"
            fullWidth={true}
            id={props.id}
            select
            label={props.label}
            value={field.value ? field.value : ""}
            onChange={handleFieldChange}
            error={Boolean(meta.touched && meta.error)}
            helperText={meta.touched && meta.error ? meta.error : " "}
            margin="normal"
          >
            {props.items.map((status) => (
              <MenuItem value={status} key={status.id}>
                {status.name}
              </MenuItem>
            ))}
          </TextField>
        );
      }}
    </FastField>
  );
}
