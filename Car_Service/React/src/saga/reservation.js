import { put, takeEvery,call,select} from 'redux-saga/effects';
import { addReservation,setFreeTime, setPossibleEndTime} from './../actions';
import { postJSON, getJSON } from './../helpers';
import {NotificationManager} from 'react-notifications';
import moment from 'moment';


const urlReservation ="http://localhost:29975/api/reservation";
const urlWorker="http://localhost:29975/api/worker";

export const getFreeTime = (state) => state.reservation.freeTime;
export const getStartTime = (state) => state.reservation.startTime;

export function* reservation(action){
    console.log(action);
    let data = new FormData();
    for (var x = 0; x < action.files.length; x++) {
        data.append("file", action.files[x]);
    }
    data.append("isEmergency", action.isEmergency)
    if(!action.isEmergency)
    {
        data.append("workerId",action.worker);
        data.append("timeStart", action.dateStart);
        data.append("timeEnd" , action.dateEnd);
    }
    data.append("purpose",action.purpose);
    data.append("breakdownDetails", action.breakdownDetails);
    data.append("desiredDiagnosis", action.desiredDiagnosis);
    data.append("captcha", action.captcha);
    let result = yield call (postJSON, urlReservation, data);
    if(result.success){
        NotificationManager.info('На ваш почтовый ящик отправлено письмо с сылкой на подтверждение, пожалуйста подтвердите бронирование');
        NotificationManager.success('Success');
    }
    else{
        NotificationManager.error(result.data);
    }
}
export function* getWorkerTime(action){
    let freeTime=[];
    let workTime = yield call (getJSON, `${urlWorker}/${action.id}/workTime`);
    let reservationTime = yield call (getJSON, `${urlWorker}/${action.id}/reservationTime`); 
    if(workTime.success&&reservationTime.success)
    {
        workTime.data.Times.forEach(function(element) {
            getTime(element, reservationTime.data.Times, freeTime);
        });
    }
    yield put(setFreeTime(freeTime));

}
export function* getEndTime(){
    let freeTime = yield select(getFreeTime);
    let startTime = yield select(getStartTime);
    var curentTime = moment(startTime);
    let endTime=[];
    while(true){
        curentTime.add(1,"hours");
        let date=curentTime.format('MM.DD.YYYY'); 
        let indexDate=endTime.findIndex((s)=>{
            return s.date==date
        });
        
        if(indexDate==-1)
        {
            indexDate=endTime.push({date: date, freeTime: get24hours()}) -1;
        }
        endTime[indexDate].freeTime=endTime[indexDate].freeTime.filter((s)=>{
            return (s.hours()!=curentTime.hours());
        })
        let elementTime = freeTime.find(s=>{
            return s.date==curentTime.format('MM.DD.YYYY');
        })
        if(elementTime==null||elementTime.freeTime.find(s=>{return s.hours()==curentTime.hours()})!=undefined){
            break
        }
    }
    yield put(setPossibleEndTime(endTime));
}
function getTime(element,reservationTime, freeTime)
{
    let startTime = moment(element.StartTime);
    let endTime = moment(element.EndTime);
    while(startTime<endTime)
    {
        let date = startTime.format('MM.DD.YYYY');
        let indexDate=freeTime.findIndex((s)=>{
            return s.date==date
        });
        if(indexDate==-1)
        {
            indexDate=freeTime.push({date: date, freeTime: get24hours(startTime)}) -1;
        }
        freeTime[indexDate].freeTime=freeTime[indexDate].freeTime.filter((s)=>{
            return ((freeTime[indexDate].date == moment().format('MM.DD.YYYY')&&s.hours()<moment().hours())||s.hours()!=startTime.hours())||(reservationTime.find(s=>{return(startTime>=moment(s.StartTime)&&startTime<moment(s.EndTime))})!=null);
        })
        startTime.add(1,'hours');
    }

}
function get24hours(startTime){
    let time=[];
    for(var i=0;i<24;i++)
    {
        time.push(moment(startTime).utc().hours(i).minute(0).second(0));
    }
    return time;
}

export default function* rootSaga() {
    yield takeEvery('ADD_RESERVATION', reservation),
    yield takeEvery('GET_WORK_TIME_WORKER', getWorkerTime),
    yield takeEvery('GET_END_TIME', getEndTime)
}