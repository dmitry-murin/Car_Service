import { combineReducers } from 'redux'

import reservation from './reservationDate'
import reservationList from './reservationList'
import app from './app'
import workers from './workers'
import worker from './worker'
import reservationHistory from './reservationHistory'

const rootReducer = combineReducers({
    reservation,
    app,
    workers,
    worker,
    reservationList,
    reservationHistory
})

export default rootReducer