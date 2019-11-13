import React from "react";
import MembersView from "./MembersView";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
/* import NavigationLogin from "../NavigationLogin/NavigationLogin";
 */ import TeamsLogin from "./TeamsLogin";
import TeamsView from "./TeamsView";
import config from "../../../config";
import axios from "axios";
function createData(name, description, members) {
  return { name, description, members };
}
class MembersLogin extends React.Component {
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
      speed: 400,
      slidesToShow: 3,
      slidesToScroll: 1,
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
    const membersView = this.state.data.map((name, description, member, i) => (
      <MembersView
        key={i}
        team_name={member.name}
        description={member.description}
        //firstName={member.members.firstName}
        //name={member.members.name}
      />
    ));
    return (
      <div>
        <h3 className="teams">Team</h3>

        <Slider {...settings}>{membersView}</Slider>
      </div>
    );
  }
}
export default MembersLogin;
