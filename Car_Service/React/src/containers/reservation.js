import { connect } from 'react-redux'

import ReservationForm from '../components/Reservation/index.jsx';
import moment from "moment";
import {changeEmergency, getEndTime,getWorkTimeWorker,getReservationTimeWorker,getWorkers,selectWorker,addReservation, setStartTimeReservation, setEndTimeReservation} from '../actions';
const mapStateToProps = state => ({
    workers: state.workers.workers,
    captchaKey: state.app.captchaKey,
    worker: state.reservation.worker,
    startTime: state.reservation.startTime,
    endTime: state.reservation.endTime,
    workDate: getFreeDate(state.reservation.freeTime),
    workTime: getFreeTime(state.reservation.freeTime,state.reservation.startTime),
    possibleEndDate: getFreeDate(state.reservation.possibleEndTime),
    possibleEndTime: getFreeTime(state.reservation.possibleEndTime,state.reservation.endTime),
    checked: state.reservation.isEmergency    
})
const mapDispatchToProps = dispatch => ({
    getWorkers:()=>{
        dispatch(getWorkers());
    },
    changeWorker:(id)=>{
        dispatch(selectWorker(id));
        dispatch(getWorkTimeWorker(id));
        dispatch(getReservationTimeWorker(id));        
    },
    addReservation:(worker,purpose,desiredDiagnosis,breakdownDetails,files, captcha, dataStart, dateEnd, isEmergency)=>{
        dispatch(addReservation(worker,purpose,desiredDiagnosis,breakdownDetails,files, captcha, dataStart, dateEnd, isEmergency))
    },
    setStartTimeReservation:(date)=>{
        dispatch(setStartTimeReservation(date.minute(0).second(0).millisecond(0)));
        dispatch(getEndTime());
    },
    setEndTimeReservation:(data)=>{
        dispatch(setEndTimeReservation(data.minute(0).second(0).millisecond(0)));
    },
    onChangeChecked:(data)=>{
        dispatch(changeEmergency(data));
    }
})
let getFreeDate=(freeTimes)=>{
    if(freeTimes!=null)
        return freeTimes.map(s=>{return moment(s.date, 'MM.DD.YYYY')});
    else return [];
}
let getFreeTime=(freeTimes,startTime)=>{
    if(freeTimes!=undefined)
    {
        let time = freeTimes.find(s=>{return s.date==(moment(startTime).format('MM.DD.YYYY'))})
        if(time)
            return time.freeTime;
        else return get24hours();
    }
    else return get24hours();
}
let get24hours=()=>{
    let time=[];
    for(var i=0;i<24;i++)
    {
        time.push(moment().hours(i).minute(0).second(0));
    }
    return time;
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ReservationForm)