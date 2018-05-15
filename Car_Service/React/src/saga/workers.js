import { put, takeEvery,call } from 'redux-saga/effects';
import { initUser, destroyUser, getWorkers,setWorkers} from './../actions';
import {NotificationManager} from 'react-notifications';
import { postJSON, getJSON } from './../helpers';
import history from "./../components/App/history"
const urlWorker ="http://localhost:29975/api/worker";

export function* addUser(action){
    let data={
        surName: action.surName,
        name: action.firstName,
        email: action.email,
        telephone: action.telephone
    }
    let result = yield call (postJSON, urlWorker, data);
    if(result.success){
        NotificationManager.success('Success');
        yield put(getWorkers());
    }
    else
        NotificationManager.error(result.data);
}
export function* getAllWorkers(){
    let result = yield call (getJSON, urlWorker);
    if(result.success){
        yield put(setWorkers(result.data.map((s)=>{
            return {id: s.Id, name: `${s.Name} ${s.SurName}`}
        })));
    }
}
export default function* rootSaga() {
    yield takeEvery('ADD_WORKER', addUser),
    yield takeEvery('GET_WORKERS', getAllWorkers)
}