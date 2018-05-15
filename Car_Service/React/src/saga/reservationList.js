import { put, takeEvery,call } from 'redux-saga/effects';
import moment from 'moment';

import {setReservationList} from './../actions';
import {getJSON} from './../helpers';
import {NotificationManager} from 'react-notifications';
const urlReservationList="http://localhost:29975/api/reservation";

export function* getReservationList(data){
    let reservation = yield call (getJSON, urlReservationList+"?date="+moment(data.date).format("YYYY-MM-DDTHH:mm"));
    if(reservation.success)
    {
        console.log(reservation);
        yield put(setReservationList(reservation.data));
    }
    else{
        NotificationManager.error(reservation.data);
    }
}
export default function* rootSaga() {
    yield takeEvery('GET_RESERVATION_LIST', getReservationList)
}