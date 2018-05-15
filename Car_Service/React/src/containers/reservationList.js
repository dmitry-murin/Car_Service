import { connect } from 'react-redux'

import ReservationList from '../components/ReservationToday/index.jsx';
import {getReservationList,setSelectedDate} from '../actions';

const mapStateToProps = state => ({
    reservation: state.reservationList.list,
    server: state.app.server,
    selectedDate: state.reservationList.selectedDate
})

const mapDispatchToProps = dispatch => ({
    changeDate:(date)=>{
        dispatch(setSelectedDate(date))
        dispatch(getReservationList(date))
    },
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ReservationList)