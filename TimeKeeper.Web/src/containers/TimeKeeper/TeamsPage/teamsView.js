import React from "react";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import CardActions from "@material-ui/core/CardActions";
import MembersLogin from "./MembersLogin";
import MembersView from "./MembersView";
import Container from "@material-ui/core/Container";
class TeamsView extends React.Component {
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
        <Card
          className="teamCards shadow-drop-2-center"
          onClick={() => this.routeTo()}
        >
          <CardActionArea>
            <CardContent>
              <Typography component="p">
                <h4>{this.props.team_name}</h4>
              </Typography>
              <Typography component="p">
                <p className="descriptionTeam">{this.props.description}</p>
              </Typography>
            </CardContent>
          </CardActionArea>
          <CardActions>
            <Button size="small" color="primary" onClick={this.routeTo}>
              Find out for more
            </Button>
            <Button id="edit">Edit</Button>
            <Button id="delete">Delete</Button>
          </CardActions>
          {this.state.showTeamMember ? <MembersView id /> : null}
        </Card>
      </Container>
    );
  }
}
export default TeamsView;
