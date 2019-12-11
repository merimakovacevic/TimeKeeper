import React, { Component } from "react";
import { Text, View, Button } from "react-native";
import {createStackNavigator} from 'react-navigation-stack';
// import Button from '../components/Button'
import RNModal from '../components/Modal'
import {Calendar, CalendarList, Agenda} from 'react-native-calendars';

const DATA = [
  {
    id: '1',
    title: 'Berina Omerasevic',
    description: 'berkica@gmail.com'
  },
  {
    id: '2',
    title: 'Hamza Crnovrsanin',
    description: 'hamzic@gmail.com'
  },
  {
    id: '3',
    title: 'Ajdin Zorlak',
    description: 'zoka@gmail.com'
  },
  {
    id: '4',
    title: 'Amina Muzurovic',
    description: 'muzi@gmail.com'
  },
  {
    id: '5',
    title: 'Faris Spica',
    description: 'spica_u_vodi@gmail.com'
  },
  {
    id: '6',
    title: 'Tajib Smajlovic',
    description: 'tajci_rajif@gmail.com'
  },
  {
    id: '7',
    title: 'Ferhat Avdic',
    description: 'wannabe_rajif@gmail.com'
  },
  {
    id: '9',
    title: 'Amra Rovcanin',
    description: 'duck_whisperer@gmail.com'
  },
];

export default class EmployeeProfile extends Component {
  state = {
    open: false
  };
  handleOpen = () => {
    this.setState({ open: true });
  };
  handleClose = () => {
    this.setState({ open: false });
  };
  render() {
    const id = this.props.navigation.getParam('id', '0')
    const employeeData = DATA.find(e => e.id === id);
    return (
      <View>
        {console.log(employeeData)}
        <Text> Employee profile </Text>
        <Text>Name: {employeeData.title}</Text>
        <Text>Email: {employeeData.description}</Text>
        <Button title="Calendar" onPress={this.handleOpen}></Button>
        <RNModal visible={this.state.open} onClose={this.handleClose} />
      </View>
    );
  }
}