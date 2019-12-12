import React, { Component } from "react";
import {
  Text,
  View,
  StyleSheet,
  Image,
  TouchableOpacity,
  Button
} from "react-native";
import RNModal from "../components/Modal";
import DateTimePicker from "react-native-modal-datetime-picker";
import moment from "moment";

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
      <View style={styles.container}>
        <Text style={styles.header}> Employee profile </Text>
        <Image
          style={styles.avatar}
          source={{ uri: "https://bootdey.com/img/Content/avatar/avatar6.png" }}
        />
        <View style={styles.body}>
          <View style={styles.bodyContent}>
            <Text style={styles.name}>Name: {employeeData.title}</Text>
            <Text style={styles.description}>
              Email: {employeeData.description}
            </Text>
            <TouchableOpacity
              onPress={this.handleOpen}
              style={styles.buttonContainer}
            >
              <Text>Calendar</Text>
            </TouchableOpacity>
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
        </View>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  header: {
    backgroundColor: "#00BFFF",
    height: 200
  },
  avatar: {
    width: 130,
    height: 130,
    borderRadius: 63,
    borderWidth: 4,
    borderColor: "white",
    marginBottom: 10,
    alignSelf: "center",
    position: "absolute",
    marginTop: 130
  },
  name: {
    fontSize: 22,
    color: "#696969",
    fontWeight: "600"
  },
  body: {
    marginTop: 40
  },
  bodyContent: {
    flex: 1,
    alignItems: "center",
    padding: 30
  },
  description: {
    fontSize: 16,
    color: "#696969",
    marginTop: 10,
    textAlign: "center"
  },
  buttonContainer: {
    marginTop: 10,
    height: 45,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    marginBottom: 20,
    width: 250,
    borderRadius: 30,
    backgroundColor: "#00BFFF"
  }
});
