const styles = (theme) => ({
  root: {
    width: "90%",
    margin: "auto",
    marginBottom: "3rem",
    marginTop: "5rem",
    // marginTop: theme.spacing(3),
    overflowX: "hidden"
  },
  toolbar: {
    display: "flexbox",
    justifyContent: "space-between",
    padding: ".5rem 0.1rem .5rem .8rem",
    backgroundColor: "#40454F"
  },
  table: {
    minWidth: 700,
    width: "100%"
  },
  button: {
    fontSize: "1.2rem",
    padding: 0,
    color: "#A3A6B4"
  },
  tableHeadFontsize: {
    textTransform: "uppercase",
    fontWeight: "500",
    fontSize: "1.1rem"
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
  hover: {
    "&:hover": {
      backgroundColor: "#707580 !important"
    }
  },
  deleteButton: {
    "&:hover": {
      backgroundColor: "rgba(255,51,51,.3) !important"
    }
  },
  editButton: {
    fill: "green",
    "&:hover": {
      backgroundColor: "rgba(0,153,0,.1) !important"
    }
  },
  search: {
    "& > *": {
      margin: theme.spacing(1),
      width: 200
    }
  },
  selectors: {
    display: "flex",
    flexDirection: "row-reverse"
  },
  textField: {
    color: "white !important"
  },
  dropdown: {
    width: 100,
    margin: '.5rem'
  }
});

export default styles;
