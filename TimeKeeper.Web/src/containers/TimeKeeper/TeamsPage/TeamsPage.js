import React from 'react'

import TeamCard from './TeamCard/TeamCard'
import TeamDescription from './TeamDescription/TeamDescription'
import UserCard from './UserCard/UserCard'

class TeamsPage extends React.Component {
state = {
    teams: [
        {id:1, teamName: 'Charlie', description: 'desc za charlie' }
    ],
    selectedTeam: {

    },
    members: [{
        id: 1, name: 'Tajib', role: 'Front-End'
    },{
        id: 2, name: 'Tajib', role: 'Back-End'
    },{
        id: 3, name: 'Tajib', role: 'Front-End'
    }]
}

handleClick = (id) => {
    let selectedTeam = this.state.teams.filter(t => t.id === id)
    this.setState({selectedTeam: selectedTeam[0]})
}

    render() {
        return <div>
       
            <TeamDescription/>
           


            {this.state.teams.map(t => <TeamCard key={t.id} id={t.id}  handleClick={this.handleClick} />)}
        </div>
    }
}

export default TeamsPage

// {this.state.teams.members.map(m => <UserCard key={m.id} role={m.role} name={m.name} />)}