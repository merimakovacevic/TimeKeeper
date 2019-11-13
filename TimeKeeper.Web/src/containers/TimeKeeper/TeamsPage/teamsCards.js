import React from "react";
import "./TeamsCards.css";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import TeamsLogin from "../TeamsLogin/TeamsLogin";
import MembersLogin from "../MembersLogin/MembersLogin";
class TeamsCards extends React.Component {
  render() {
    return (
      <div>
        <NavigationLogin />
        <TeamsLogin />
      </div>
    );
  }
}
export default TeamsCards;
