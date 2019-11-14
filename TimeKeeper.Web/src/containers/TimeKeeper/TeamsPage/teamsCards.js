import React from "react";
/* import NavigationLogin from "../NavigationLogin/NavigationLogin"; */
import TeamsLogin from "./TeamsLogin";
import MembersLogin from "./MembersLogin";
class TeamsCards extends React.Component {
  render() {
    return (
      <div>
        {/*  <NavigationLogin /> */}
        <TeamsLogin />
      </div>
    );
  }
}
export default TeamsCards;
