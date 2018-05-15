export const setStartTime= (date)=>({
    type: 'SET_START_TIME',
    date
})
export const setEndTime= (date)=>({
    type: 'SET_END_TIME',
    date
})
export const setWorker= (id, name, telephone,email)=>({
    type: 'SET_WORKER',
    id,
    name,
    telephone,
    email
})
export const getWorker= (id)=>({
    type: 'GET_WORKER',
    id
})
export const addWorkTime= (id, startTime, endTime)=>({
    type: 'ADD_WORK_TIME',
    id,
    startTime,
    endTime
}) 
export const getWorkTime= (id)=>({
    type: 'GET_WORK_TIME',
    id
}) 
export const setWorkTime= (workTime)=>({
    type: 'SET_WORK_TIME',
    workTime
})
