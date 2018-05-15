import { put, takeEvery,call } from 'redux-saga/effects';
import { setWorker,setWorkTime,getWorkTime} from './../actions';
import { postJSON, getJSON } from './../helpers';
import {NotificationManager} from 'react-notifications';
import history from "./../components/App/history"
const urlWorker ="http://localhost:29975/api/worker";

export function* addTime(action){
    let data={
        startTime: action.startTime,
        endTime: action.endTime
    }
    let result = yield call (postJSON, `${urlWorker}/${action.id}/workTime`, data);
    if(result.success){
        NotificationManager.success('Success');
        yield put(getWorkTime(action.id));
    }
    else
        NotificationManager.error(result.data);
}
export function* getWorker(action){
    let result = yield call (getJSON, `${urlWorker}/${action.id}`);
    if(result.success){
        yield put(setWorker(result.data.Id, `${result.data.Name} ${result.data.SurName}`, result.data.Telephone, result.data.Email));
    }
}
export function* getWorkTimes(action){
    let result = yield call (getJSON, `${urlWorker}/${action.id}/workTime`);
    if(result.success){
        yield put(setWorkTime(result.data.Times));
    }
}
export default function* rootSaga() {
    yield takeEvery('ADD_WORK_TIME', addTime),
    yield takeEvery('GET_WORKER', getWorker),
    yield takeEvery('GET_WORK_TIME', getWorkTimes)
}