export const selectWorker= (worker)=>({
    type: 'SELECT_WORKER',
    selectWorker: worker
})
export const addReservation=(worker,purpose,desiredDiagnosis,breakdownDetails,files, captcha, dateStart, dateEnd, isEmergency)=>({
    type: 'ADD_RESERVATION',
    worker,
    purpose,
    desiredDiagnosis,
    breakdownDetails,
    files, 
    captcha,
    dateStart,
    dateEnd,
    isEmergency
})
export const setStartTimeReservation=(date)=>({
    type: 'SET_START_TIME_RESERVATION',
    date
})
export const setEndTimeReservation=(date)=>({
    type: 'SET_END_TIME_RESERVATION',
    date
})
export const getWorkTimeWorker=(id)=>({
    type: 'GET_WORK_TIME_WORKER',
    id
})
export const getReservationTimeWorker=(id)=>({
    type: 'GET_RESERVATION_TIME_WORKER',
    id
})
export const setFreeTime=(data)=>({
    type: 'SET_FREE_TIME',
    data
})
export const getEndTime=()=>({
    type: 'GET_END_TIME'
})
export const setPossibleEndTime=(data)=>({
    type: 'SET_POSSIBLE_END_TIME',
    data
})
export const changeEmergency=(data)=>({
    type: 'CHANGE_IS_EMERGENCY',
    data
})

