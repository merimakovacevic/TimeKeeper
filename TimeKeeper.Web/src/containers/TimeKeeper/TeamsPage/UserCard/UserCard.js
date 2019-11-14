import React from 'react';

import { withStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { fontSize } from '@material-ui/system';

const styles = {
  card: {
    width: 200,
    height: 225,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    position: 'relative',
    justifyContent: 'center',
    margin: '2rem'
  },  
  name: {
    marginTop: '-2.5rem'
  },
  pos: {
    textAlign: 'center',
    fontSize: '1rem'
  },
  img: {
      height: '80%',
      width: '100%',
      objectFit: 'contain',
      padding: '2rem'
  }
};

function SimpleCard(props) {
  const { classes } = props;
  

  return (
    <Card className={classes.card}>
      <CardContent>
        <img src="https://cdn2.iconfinder.com/data/icons/business-management-52/96/Artboard_20-512.png" alt="" className={classes.img} />
        <Typography variant="h5" component="h2" className={classes.pos} >
          {props.name}
        </Typography>
        
      </CardContent>
     
    </Card>
  );
}


export default withStyles(styles)(SimpleCard);