import React, { Component } from "react";
import { Text, View, StyleSheet } from "react-native";
import { Agenda } from "react-native-calendars";
import moment from "moment";

const DATA = [
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-01T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-02T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-03T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12426,
        name: "Expire trial organizations"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-04T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11380,
        name: "Expire trial organizations"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-05T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12091,
        name: "Expire trial organizations"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-06T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12836,
        name: "Expire trial organizations"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-07T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11666,
        name:
          "Fix bugs (Description on trophy card, Back button on sign up screen, Error on edit button click, Sign out in menu)"
      }
    ]
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-08T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-09T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-10T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11560,
        name: "Expire trial organization"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-11T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12293,
        name: "Expire trial organization"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-12T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11044,
        name:
          "Fix bugs (Description on trophy card, Back button on sign up screen,  Error on edit button click,  Sign out in menu)"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-13T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11664,
        name:
          "Fix bugs (Description on trophy card, Back button on sign up screen,  Error on edit button click,  Sign out in menu)"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-14T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12330,
        name:
          "Fix bugs (Description on trophy card, Back button on sign up screen,  Error on edit button click,  Sign out in menu)"
      }
    ]
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-15T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-16T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-17T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12294,
        name:
          "Fix bugs (Description on trophy card, Back button on sign up screen, Error on edit button click, Sign out in menu)"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-18T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11062,
        name:
          "Fix bugs (Description on trophy card, Back button on sign up screen, Error on edit button click, Sign out in menu)"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-19T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11855,
        name:
          "Change registration process to be automatized , automatically log in user ( re work process)"
      }
    ]
  },
  {
    dayType: {
      id: 6,
      name: "Vacation"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-20T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-21T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11307,
        name:
          "Change registration process to be automatized , automatically log in user ( re work process)"
      }
    ]
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-22T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-23T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-24T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11413,
        name:
          "Change registration process to be automatized , automatically log in user ( re work process)"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-25T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11946,
        name:
          "Change registration process to be automatized , automatically log in user ( re work process)"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-26T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12718,
        name: "Rename all portable stands to private"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-27T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 11362,
        name: "Forgotten password"
      }
    ]
  },
  {
    dayType: {
      id: 1,
      name: "Workday"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-28T00:00:00",
    totalHours: 8.0,
    comment: null,
    details: [
      {
        id: 12049,
        name: "Change profile picture (spike)"
      }
    ]
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-29T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  },
  {
    dayType: {
      id: 11,
      name: "Weekend"
    },
    employee: {
      id: 4,
      name: "David Davis"
    },
    date: "2019-06-30T00:00:00",
    totalHours: 0.0,
    comment: null,
    details: []
  }
];
export default class Calendar extends React.Component {
  state = {
    date: this.props.navigation.getParam("date", "null"),
    items: {}
  };

  parseData() {
    let items = {};
    for (let i = 0; i < DATA.length; i++) {
      let stringDate = moment(DATA[i].date).format("YYYY-MM-DD");
      items[stringDate] === undefined
        ? (items[stringDate] = [
            {
              height: 100,
              name: DATA[i].details[0] ? DATA[i].details[0].name : "No details"
            }
          ])
        : items[stringDate].push({
            height: 100,
            name: DATA[i].details[0] ? DATA[i].details[0].name : "No details"
          });
    }
    return items;
  }

  render() {
    const items = this.parseData();
    this.parseData();
    return (
      <Agenda
        items={items}
        loadItemsForMonth={this.loadItems.bind(this)}
        selected={this.state.date}
        renderItem={this.renderItem.bind(this)}
        renderEmptyDate={this.renderEmptyDate.bind(this)}
        rowHasChanged={this.rowHasChanged.bind(this)}
        // markingType={'period'}
        // markedDates={{
        //    '2017-05-08': {textColor: '#666'},
        //    '2017-05-09': {textColor: '#666'},
        //    '2017-05-14': {startingDay: true, endingDay: true, color: 'blue'},
        //    '2017-05-21': {startingDay: true, color: 'blue'},
        //    '2017-05-22': {endingDay: true, color: 'gray'},
        //    '2017-05-24': {startingDay: true, color: 'gray'},
        //    '2017-05-25': {color: 'gray'},
        //    '2017-05-26': {endingDay: true, color: 'gray'}}}
        // monthFormat={'yyyy'}
        // theme={{calendarBackground: 'red', agendaKnobColor: 'green'}}
        // renderDay={(day, item) => (<Text>{day ? day.day: 'item'}</Text>)}
      />
    );
  }

  loadItems(day) {
    setTimeout(() => {
      for (let i = -15; i < 85; i++) {
        const time = day.timestamp + i * 24 * 60 * 60 * 1000;
        const strTime = this.timeToString(time);
        if (!this.state.items[strTime]) {
          this.state.items[strTime] = [];
          const numItems = Math.floor(Math.random() * 5);
          for (let j = 0; j < numItems; j++) {
            this.state.items[strTime].push({
              name: "Item for " + strTime,
              height: Math.max(50, Math.floor(Math.random() * 150))
            });
          }
        }
      }
      const newItems = {};
      Object.keys(this.state.items).forEach((key) => {
        newItems[key] = this.state.items[key];
      });
      this.setState({
        items: newItems
      });
    }, 1000);
    // console.log(`Load Items for ${day.year}-${day.month}`);
  }

  renderItem(item) {
    return (
      <View style={[styles.item, { height: item.height }]}>
        <Text>{item.name}</Text>
      </View>
    );
  }

  renderEmptyDate() {
    return (
      <View style={styles.emptyDate}>
        <Text>This is empty date!</Text>
      </View>
    );
  }

  rowHasChanged(r1, r2) {
    return r1.name !== r2.name;
  }

  timeToString(time) {
    const date = new Date(time);
    return date.toISOString().split("T")[0];
  }
}

const styles = StyleSheet.create({
  item: {
    backgroundColor: "white",
    flex: 1,
    borderRadius: 5,
    padding: 10,
    marginRight: 10,
    marginTop: 17
  },
  emptyDate: {
    height: 15,
    flex: 1,
    paddingTop: 30
  }
});
