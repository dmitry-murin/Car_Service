import { put, takeEvery,call } from 'redux-saga/effects';

import {setReservationHistory} from './../actions';
import {getJSON} from './../helpers';
import {NotificationManager} from 'react-notifications';
const urlReservationHistory="http://localhost:29975/api/reservation/history";

export function* getReservationHistory(){

    let reservation = yield call (getJSON, urlReservationHistory);
    if(reservation.success)
    {
        yield put(setReservationHistory(reservation.data));
    }
    else{
        NotificationManager.error(reservation.data);
    }
}
export default function* rootSaga() {
    yield takeEvery('GET_RESERVATION_HISTORY', getReservationHistory)
}