import React, { PureComponent, Component, Fragment } from "react";
import { Formik, useField, Field, FastField } from "formik";
import { TextField } from "@material-ui/core";
import * as Yup from "yup";

export default function FormikTextField(props) {
  return (
    <FastField name={props.name}>
      {({
        field, // { name, value, onChange, onBlur }
        meta,
        form
      }) => {
        // console.log("re-render textField");
        const handleFieldChange = e => {
          form.setFieldValue(field.name, e.target.value, false);
        };
        return (
          <TextField
            {...field}
            id={props.id}
            margin="normal"
            variant="outlined"
            fullWidth={true}
            label={props.label}
            type={props.type}
            rows={props.rows}
            multiline={props.multiline}
            onChange={handleFieldChange}
            InputLabelProps={props.type === "date" ? { shrink: true } : {}}
            // floatingLabelFixed={props.type === "date" ? true : false}
            error={Boolean(meta.touched && meta.error)}
            helperText={meta.touched && meta.error ? meta.error : " "}
          />
        );
      }}
    </FastField>
  );
}
//HOOKS
// export default function FormikTextField(props) {
//   const [field, meta] = useField(props);
//   // console.log("hello");
//   return (
//     <TextField
//       {...field}
//       margin="normal"
//       variant="outlined"
//       fullWidth={true}
//       label={props.label}
//       error={Boolean(meta.touched && meta.error)}
//       helperText={meta.touched && meta.error ? meta.error : " "}
//     />
//   );
// }

//CHILDREN
// export default class FormikTextField extends PureComponent {
//   constructor(props) {
//     super(props);
//     this.state = {
//       value: ""
//     };
//   }
//   handleChange = e => {
//     this.setState({ value: e.target.value });
//   };
//   render() {
//     const { id, name, label, validationSchema } = this.props;
//     return (
//       <Field type="text" id={id} name={name} label={label} value={this.state.value}>
//         {({
//           field, // { name, value, onChange, onBlur }
//           form,
//           meta
//         }) => {
//           console.log("formik handle ", field.onChange, field.onBlur);

//           return (
//             <TextField
//               {...field}
//               // pass the field value
//               value={this.state.value}
//               // change only the state of this field on change, not the whole form
//               onChange={e => {
//                 Yup.reach(validationSchema, field.name)
//                   .validate(e.target.value)
//                   .then(() => form.setFieldError(field.name, undefined))
//                   .catch(e => form.setFieldError(field.name, e.message));
//                 // form.handleChange(e);
//                 this.handleChange(e);
//               }}
//               // onChange from field-props, only change on blur
//               // onBlur={field.onChange}
//               margin="normal"
//               variant="outlined"
//               fullWidth={true}
//               label={label}
//               error={Boolean(meta.touched && meta.error)}
//               helperText={meta.touched && meta.error ? meta.error : " "}
//             />
//           );
//         }}
//       </Field>
//     );
//   }
// }

// export default class FormikTextField extends Component {
//   state = {
//     isBlurred: false
//   };
//   shouldComponentUpdate() {
//     return this.state.isBlurred;
//   }
//   blurInput = () => {
//     this.setState({ isBlurred: true });
//   };
//   focusInput = () => {
//     this.setState({ isBlurred: false });
//   };
//   render() {
//     console.log("rerender field");
//     return (
//       <FastField name={this.props.name}>
//         {({
//           field, // { name, value, onChange, onBlur }
//           meta
//         }) => {
//           const handleOnBlur = e => {
//             this.blurInput();
//             field.onBlur(e);
//           };
//           console.log("re-render textField");
//           return (
//             <TextField
//               {...field}
//               margin="normal"
//               variant="outlined"
//               fullWidth={true}
//               onBlur={handleOnBlur}
//               onFocus={this.focusInput}
//               label={this.props.label}
//               type={this.props.type}
//               InputLabelProps={
//                 this.props.type === "date" ? { shrink: true } : {}
//               }
//               error={Boolean(meta.touched && meta.error)}
//               helperText={meta.touched && meta.error ? meta.error : " "}
//             />
//           );
//         }}
//       </FastField>
//     );
//   }
// }
