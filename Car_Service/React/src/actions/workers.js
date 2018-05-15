export const addWorker= (surName,firstName, email, telephone)=>({
    type: 'ADD_WORKER',
    surName,
    firstName, 
    email, 
    telephone
})
export const setWorkers= (workers)=>({
    type: 'SET_WORKERS',
    workers
})
export const getWorkers= ()=>({
    type: 'GET_WORKERS'
})