import React, { Component } from "react";
import { Text, View, StyleSheet, ActivityIndicator } from "react-native";
import { Agenda } from "react-native-calendars";
import moment from "moment";
import { getCalendarMonth } from "../utils/api";

export default class Calendar extends React.Component {
	state = {
		date: this.props.navigation.getParam("date", "null"),
		id: this.props.navigation.getParam("id", "0"),
		items: null
	};
	async componentDidMount() {
		const month = moment(this.state.date).format("MM");
		const year = moment(this.state.date).format("YYYY");
		let data = await getCalendarMonth(this.state.id, year, month);
		if (data !== undefined) {
			let newData = this.parseData(data);
			if (data.length > 0) {
				this.setItems(newData);
			}
		}
	}
	setItems(data) {
		this.setState({ items: data });
	}
	parseData(DATA) {
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
		return (
			<View style={styles.container}>
				{this.state.items !== null ? (
					<Agenda
						items={this.state.items}
						// loadItemsForMonth={this.loadItems.bind(this)}
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
				) : (
					<ActivityIndicator style={styles.loader} size={80} color="#00ff00" />
				)}
			</View>
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
		return <ActivityIndicator style={styles.loader} size={80} color="#00ff00" />;
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
	container: {
		flex: 1
	},
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
	},
	loader: {
		flex: 1,
		justifyContent: "center",
		alignItems: "center"
	}
});
