import React from 'react';

import { withStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

const styles = {
  card: {
    width: 275,
    height: 275,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    position: 'relative',
    justifyContent: 'center'
  },  
  name: {
    marginTop: '-2.5rem'
  },
  pos: {
    position: 'absolute',
    bottom: '.5rem'
  },
};

function SimpleCard(props) {
  const { classes, handleClick, id } = props;
  

  return (
    <Card className={classes.card}>
      <CardContent>
        
        <Typography variant="h5" component="h2" className={classes.name}>
          Team Name
        </Typography>
       
      </CardContent>
      <CardActions className={classes.pos}>
        <Button size="small" onClick={() => handleClick(id)} >Learn More</Button>
      </CardActions>
    </Card>
  );
}


export default withStyles(styles)(SimpleCard);