import React, { Component } from "react";
import { SafeAreaView, FlatList } from "react-native";

import { Item } from "./ItemList";

export class List extends Component {
	render() {
		return (
			<SafeAreaView>
				<FlatList
					data={this.props.data}
					renderItem={({ item }) => (
						<Item id={item.id} title={`${item.firstName} ${item.lastName}`} description={item.email} />
					)}
					keyExtractor={(item) => item.id}
				/>
			</SafeAreaView>
		);
	}
}
