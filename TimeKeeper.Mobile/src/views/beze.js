import React, { Component } from "react";
import { Text, View, Button } from "react-native";
import { createStackNavigator } from "react-navigation-stack";
import RNModal from "../components/Modal";
import moment from "moment";
import DateTimePicker from "react-native-modal-datetime-picker";

const DATA = [
  {
    id: "1",
    title: "Berina Omerasevic",
    description: "berkica@gmail.com"
  },
  {
    id: "2",
    title: "Hamza Crnovrsanin",
    description: "hamzic@gmail.com"
  },
  {
    id: "3",
    title: "Ajdin Zorlak",
    description: "zoka@gmail.com"
  },
  {
    id: "4",
    title: "Amina Muzurovic",
    description: "muzi@gmail.com"
  },
  {
    id: "5",
    title: "Faris Spica",
    description: "spica_u_vodi@gmail.com"
  },
  {
    id: "6",
    title: "Tajib Smajlovic",
    description: "tajci_rajif@gmail.com"
  },
  {
    id: "7",
    title: "Ferhat Avdic",
    description: "wannabe_rajif@gmail.com"
  },
  {
    id: "9",
    title: "Amra Rovcanin",
    description: "duck_whisperer@gmail.com"
  }
];

export default class EmployeeProfile extends Component {
  state = {
    open: false,
    isDateTimePickerVisible: false,
    stringDate: null,
    date: null
  };
  handleOpen = () => {
    this.setState({ open: true });
  };

  handleClose = () => {
    this.setState({ open: false });
  };
  handleOpenCalendar = () => {
    this.setState({ open: false });
    this.props.navigation.navigate("Calendar", {
      date: this.state.date
    });
  };
  showDateTimePicker = () => {
    this.setState({ isDateTimePickerVisible: true });
  };
  hideDateTimePicker = () => {
    this.setState({ isDateTimePickerVisible: false });
  };

  handleDatePicked = (date) => {
    this.setState({ date: date });
    console.log("A date picked: ", date);
    stringDate = JSON.parse(JSON.stringify(moment(date).format("MMM Do YY")));
    this.setState({ stringDate: stringDate });
    this.hideDateTimePicker();
  };
  render() {
    const id = this.props.navigation.getParam("id", "0");
    const employeeData = DATA.find((e) => e.id === id);
    return (
      <View>
        <Text> Employee profile </Text>
        <Text>Name: {employeeData.title}</Text>
        <Text>Email: {employeeData.description}</Text>
        <Button title="Open Calendar" onPress={this.handleOpen}></Button>
        <RNModal visible={this.state.open} onClose={this.handleClose}>
          <View>
            <Button
              title={!this.state.date ? "Show DatePicker" : "Change date"}
              onPress={this.showDateTimePicker}
            />
            <DateTimePicker
              isVisible={this.state.isDateTimePickerVisible}
              onConfirm={this.handleDatePicked}
              onCancel={this.hideDateTimePicker}
            />
            <View>
              {!this.state.date ? null : (
                <Text>Picked date {this.state.stringDate}</Text>
              )}
            </View>
            <Button title="Open" onPress={this.handleOpenCalendar} />
          </View>
        </RNModal>
      </View>
    );
  }
}
