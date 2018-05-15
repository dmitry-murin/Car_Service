import { put, takeEvery,call } from 'redux-saga/effects';

import {setIsConfirm} from './../actions';
import {getJSON} from './../helpers';
const urlReservationHistory="http://localhost:29975/api/reservation/confirm/";

export function* getConfirm(data){
    let reservation = yield call (getJSON, urlReservationHistory+data.guid);
    if(reservation.success)
    {
        yield put(setIsConfirm(true));
    }
    else{
        yield put(setIsConfirm(false));
    }
}
export default function* rootSaga() {
    yield takeEvery('GET_CONFIRM', getConfirm)
}