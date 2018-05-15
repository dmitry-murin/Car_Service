import { connect } from 'react-redux'

import Worker from '../components/Worker/index.jsx';
import { compareDate} from './../helpers';
import {setStartTime, setEndTime, addWorkTime,getWorker,getWorkTime} from '../actions';

const mapStateToProps = state => ({
    worker: state.worker.worker,

    formatTime: state.app.formatTime,
    startTime: state.worker.startTime,
    endTime: state.worker.endTime,
    workTime: state.worker.workTime.sort(compareDate)
})

const mapDispatchToProps = dispatch => ({
    addWorkTime:(id, startTime, endTime)=>{
        dispatch(addWorkTime(id, startTime, endTime));
    },
    reset:()=>{
        dispatch(setStartTime(undefined));
        dispatch(setEndTime(undefined));
    },
    setStartTime:(date)=>{
        dispatch(setStartTime(date));
    },
    setEndTime:(data)=>{
        dispatch(setEndTime(data));
    },
    getWorker:(id)=>{
        dispatch(getWorker(id))
    },
    getWorkTime:(id)=>{
        dispatch(getWorkTime(id))
    }
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Worker)