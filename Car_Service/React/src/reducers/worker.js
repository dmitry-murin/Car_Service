const initState={
    worker: {},
    startTime: undefined,
    endTime: undefined,
    workTime:[]
}; 
const workers = (state = initState, action) => {
    switch (action.type) {
        case "SET_START_TIME": {
            return Object.assign({}, state, {
                startTime: action.date});
        }
        case "SET_END_TIME": {
            return Object.assign({}, state, {
                endTime: action.date});
        }
        case "SET_WORKER": {
            return Object.assign({}, state, {
                worker: {
                    id: action.id,
                    name: action.name,
                    telephone: action.telephone,
                    email: action.email
                }
            })
        };
        case "SET_WORK_TIME": {
            return Object.assign({}, state, {
                workTime: action.workTime
            })
        };
        default:
            return state
    }
}
export default workers;