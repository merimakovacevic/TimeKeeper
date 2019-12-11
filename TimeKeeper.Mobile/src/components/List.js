import React from "react";
import { FlatList, StyleSheet } from "react-native";
import Constants from "expo-constants";
import { Item } from "./Item";

export default function App(props) {
  const [selected, setSelected] = React.useState(new Map());

  const onSelect = React.useCallback(
    (id) => {
      const newSelected = new Map(selected);
      newSelected.set(id, !selected.get(id));

      setSelected(newSelected);
    },
    [selected]
  );

  return (
    <FlatList
      data={props.data}
      renderItem={({ item }) => (
        <Item
          style={styles.item}
          id={item.id}
          title={item.title}
          description={item.description}
          selected={!!selected.get(item.id)}
          onSelect={props.onPress}
          bottomDivider
          chevron
        />
      )}
      keyExtractor={(item) => item.id}
      extraData={selected}
    />
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginTop: Constants.statusBarHeight
  },
  item: {
    padding: 20,
    marginVertical: 8,
    marginHorizontal: 16,
    display: "flex",
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: 3,
    height: 100
  },
  title: {
    fontSize: 28,
    position: "absolute",
    top: 0,
    left: 100,
    color: "black"
  },
  description: {
    fontSize: 16,
    color: "black",
    position: "absolute",
    top: 60,
    left: 100
  },
  image: {
    width: 50,
    height: 50
  }
});
