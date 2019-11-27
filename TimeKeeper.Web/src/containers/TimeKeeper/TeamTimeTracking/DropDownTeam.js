// import React from "react";
// import ReactDOM from "react-dom";
// import PropTypes from "prop-types";
// import { withStyles } from "@material-ui/core/styles";
// import Input from "@material-ui/core/Input";
// import OutlinedInput from "@material-ui/core/OutlinedInput";
// import FilledInput from "@material-ui/core/FilledInput";
// import InputLabel from "@material-ui/core/InputLabel";
// import MenuItem from "@material-ui/core/MenuItem";
// import FormHelperText from "@material-ui/core/FormHelperText";
// import FormControl from "@material-ui/core/FormControl";
// import Select from "@material-ui/core/Select";
// import axios from "axios";
// import classNames from "classnames";
// import config from "../../../config";

// const styles = theme => ({
//   root: {
//     display: "flex",
//     flexWrap: "wrap"
//   },
//   formControl: {
//     margin: theme.spacing.unit,
//     minWidth: 120
//   },
//   selectEmpty: {
//     marginTop: theme.spacing.unit * 2
//   }
// });
// let counter = 0;
// function createData(name) {
//   counter += 1;
//   return {
//     id: counter,
//     name
//   };
// }

// class DropDown extends React.Component {
//   constructor(props) {
//     super(props);
//     this.state = {
//       labelWidth: 0,
//       teams: [],
//       selectedId: null
//     };
//     this.onClickDrop = this.onClickDrop.bind(this);
//   }
//   componentDidMount() {
//     axios(`${config.apiUrl}teams`, {
//       headers: {
//         "Content-Type": "application/json",
//         Authorization: config.token
//       }
//     })
//       .then(res => {
//         console.log(res);
//         this.setState({ teams: res.data });

//         /*  let fetchedData = res.data.map(r => createData(r.name));
//         this.setState({ teams: fetchedData, loading: false }); */
//       })
//       .catch(error => console.log(error.response));
//   }

//   handleChange = event => {
//     this.setState({ [event.target.name]: event.target.value });
//   };

//   /* onClickV = () => {
//     // var lang = this.dropdown.value;đž
//     var teamName = "sakkasd";
//     this.props.onClickDrop(teamName);
//   }; */

//   onClickDrop = name => event => {
//     var value = event.target.value;

//     this.setState({
//       selectedId: value
//     });
//     this.props.onClickDrop(value);
//     console.log(this.state.selectedId);
//   };

//   render() {
//     const { classes } = this.props;
//     let teams = this.state.teams;
//     return (
//       <form className={classes.root} autoComplete="off">
//         <FormControl className={classes.formControl}>
//           <InputLabel htmlFor="age-simple">Team</InputLabel>
//           <Select
//             name="selectOptions"
//             onChange={this.onClickDrop("selectOptions")}
//             /*   onClick={() => this.props.onClickDrop(this.state.selectedId)} */
//             inputProps={{}}
//           >
//             {/* <MenuItem value={this.state.teams}>
//               <em>None</em>
//             </MenuItem> */}
//             {this.state.teams.map(team => (
//               <MenuItem key={team.id} value={team.id}>
//                 {team.name}
//               </MenuItem>
//             ))}
//           </Select>
//         </FormControl>
//       </form>
//     );
//   }
// }

// DropDown.propTypes = {
//   classes: PropTypes.object.isRequired
// };
// export default withStyles(styles)(DropDown);
