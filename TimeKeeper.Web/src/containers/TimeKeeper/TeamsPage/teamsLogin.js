import React from "react";
import "./TeamsLogin.css";
import TeamsView from "../TeamsView/TeamsView";
import Members from "../MembersLogin/MembersLogin";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import teams from "./../../data/team.json";
import config from "../../config";
import axios from "axios";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import Container from "@material-ui/core/Container";
function createData(name, description, members) {
  return { name, description, members };
}
class TeamsLogin extends React.Component {
  state = {
    data: []
  };
  componentDidMount() {
    this.setState({ loading: true });
    axios(`${config.apiUrl}teams`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: config.token
      }
    })
      .then(res => {
        let fetchedData = res.data.map(r =>
          createData(r.name, r.description, r.members)
        );
        this.setState({ data: fetchedData, loading: false });
        console.log(fetchedData);
      })
      .catch(err => this.setState({ loading: false }));
  }
  render() {
    let settings = {
      dots: true,
      infinite: true,
      speed: 800,
      slidesToShow: 2,
      slidesToScroll: 1,
      autoplay: true,
      autoplaySpeed: 3000,
      responsive: [
        {
          breakpoint: 1200,
          settings: {
            infinite: true,
            slidesToShow: 3,
            slidesToScroll: 1,
            dots: true,
            arrows: true
          }
        },
        {
          breakpoint: 980,
          settings: {
            slidesToShow: 2,
            slidesToScroll: 1,
            dots: true,
            arrows: true
          }
        },
        {
          breakpoint: 600,
          settings: {
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: true
          }
        }
      ]
    };
    const teamView = this.state.data.map((member, i) => (
      <TeamsView
        key={i}
        team_name={member.name}
        description={member.description}
        firstName={member.members.firstName}
        name={member.members.name}
      />
    ));
    return (
      <Container>
        <h2 className="teams">Teams</h2>
        <a className="btn btn-teams">Add Teams</a>
        <Slider {...settings}>{teamView}</Slider>
      </Container>
    );
  }
}
export default TeamsLogin;
