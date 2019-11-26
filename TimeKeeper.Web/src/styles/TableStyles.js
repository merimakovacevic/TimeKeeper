import { lighten } from "@material-ui/core/styles/colorManipulator";

const styles = theme => ({
  root: {
    width: "90%",
    padding: "1rem 1rem 0",
    position: "relative",
    left: "50%",
    top: "50%",
    transform: "translate(-50%, -50%)"
  },
  root2: {
    margin: 0
  },
  table: {
    minWidth: 1020
  },
  tableWrapper: {
    overflowX: "auto",
    padding: "1rem 2rem"
  },
  highlight:
    theme.palette.type === "light"
      ? {
          color: theme.palette.secondary.main,
          backgroundColor: lighten(theme.palette.secondary.light, 0.85)
        }
      : {
          color: theme.palette.text.primary,
          backgroundColor: theme.palette.secondary.dark
        },
  spacer: {
    flex: "1 1 100%"
  },
  actions: {
    color: theme.palette.text.secondary
  },
  title: {
    flex: "0 0 auto"
  },
  tableCell: {
    fontSize: "1.1rem",
    fontWeight: "bold",
    backgroundColor: "#f5f6fa"
  },
  loader: {
    color: "white"
  },
  loaderText: {
    color: "white",
    marginTop: "2rem"
  },
  center: {
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center"
  },
  
  twoWordLabel: {
    width: "32rem"
}
});

export default styles;
