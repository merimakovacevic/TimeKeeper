import React from "react";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import CardActions from "@material-ui/core/CardActions";
import { Container } from "@material-ui/core";
//import MembersLogin from "../MembersLogin/MembersLogin";
class MembersView extends React.Component {
  state = {
    flipped: false,
    showTeamMember: false
  };
  flipCard() {
    this.setState({ flipped: !this.state.flipped });
  }
  routeTo = () => {
    this.setState({ showTeamMember: true });
  };
  render() {
    return (
      <Container>
        <h6>{this.props.description}</h6>
        <Card
          className="teamCards shadow-drop-2-center"
          onClick={() => this.routeTo()}
        >
          <CardActionArea>
            <CardContent>
              <Typography component="p"></Typography>
              <Typography component="p">
                <p className="descriptionTeam">{this.props.name}</p>
              </Typography>
            </CardContent>
          </CardActionArea>
          <CardActions></CardActions>
        </Card>
      </Container>
    );
  }
}
export default MembersView;
