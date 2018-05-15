import { fork } from 'redux-saga/effects';
import user from './user'
import workers from './workers'
import worker from './worker'
import reservation from './reservation'
import reservationList from './reservationList'
import reservationHistory from './reservationHistory'
import confirm from './confirm'

export default function* rootSaga() {
  yield fork(user),
  yield fork(workers),
  yield fork(worker),
  yield fork(reservation),
  yield fork(reservationList),
  yield fork(reservationHistory),
  yield fork(confirm)
}