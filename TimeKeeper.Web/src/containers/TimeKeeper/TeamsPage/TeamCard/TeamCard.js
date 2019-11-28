import React from "react";

import { withStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";

const styles = {
	card: {
		width: 275,
		height: 300,
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
		position: "relative",
		margin: "auto"
	},
	name: {
		textAlign: "center",
		marginBottom: "2rem"
	},
	pos: {
		position: "absolute",
		bottom: ".5rem"
	}
};

class SimpleCard extends React.Component {
	state = {
		selectedTeam: null
	};

	render() {
		const { classes, name } = this.props;

		return (
			<Card className={classes.card}>
				<CardContent>
					<Typography variant="h5" component="h2" className={classes.name}>
						{name}
					</Typography>

					<p>
						Lorem ipsum dolor sit, amet consectetur adipisicing elit. Deserunt facere ullam quasi odio neque
						dolore, facilis necessitatibus eos? Expedita tempora consectetur maxime eligendi mollitia, eos
						saepe laboriosam soluta natus. Aliquid?
					</p>
				</CardContent>
				<CardActions className={classes.pos}>
					<Button size="small">Learn More</Button>
				</CardActions>
			</Card>
		);
	}
}

export default withStyles(styles)(SimpleCard);
