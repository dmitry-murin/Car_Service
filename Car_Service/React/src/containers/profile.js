import { connect } from 'react-redux'

import {getReservationHistory} from '../actions';
import { compareDate} from './../helpers';
import Profile from '../components/Profile/index.jsx';

const mapStateToProps = state => ({
  reservation: state.reservationHistory.reservations.sort(compareDate).reverse(),
  server: state.app.server
})

const mapDispatchToProps = dispatch => ({
  load:()=>{
    dispatch(getReservationHistory())
  }
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Profile)