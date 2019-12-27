const styles = (theme) => ({
  parrent: {},
  wrapper: {
    display: "flex",
    width: "100%",
    height: "100%",
    padding: "2rem"
  },
  container: {
    width: "100%",
    margin: "2rem"
  },
  input: {
    margin: "0 0 1rem 0"
  },
  errorMessage: {
    color: "red",
    fontSize: ".85rem"
  },
  root: {
    width: "100%",
    marginTop: theme.spacing(3)
  },
  table: {
    minWidth: 0
  },
  tableHead: {
    backgroundColor: "#f5f6fa"
  },
  tableCell: {
    fontSize: "1.1rem",
    fontWeight: "bold"
  },
  img: {
    height: 200,
    width: 220,
    margin: "0 0 2rem 0",
    objectFit: "contain"
  },

  buttons: {
    display: "flex",
    justifyContent: "space-between",
    marginTop: "6rem"
  },
  statusInput: {
    marginTop: "15px"
  },
  titleInput: {
    marginTop: "8px"
  },
  submitButton: {
    background: "rgb(38, 166, 154)",
    borderRadius: 3,
    color: "white"
  }
});
export default styles;
