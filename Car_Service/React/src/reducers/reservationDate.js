const initState={
    worker: undefined,
    startTime:undefined,
    endTime: undefined,
    freeTime: undefined,
    possibleEndTime: undefined,
    isEmergency: true
};
const worker = (state = initState, action) => {
    switch (action.type) {
        case "SELECT_WORKER": {
            return Object.assign({}, state, {
                worker: action.selectWorker});
        }
        case "SET_FREE_TIME": {
            return Object.assign({}, state, {
                freeTime: action.data});
        }
        case "SET_START_TIME_RESERVATION": {
            return Object.assign({}, state, {
                startTime: action.date});
        }
        case "SET_END_TIME_RESERVATION": {
            return Object.assign({}, state, {
                endTime: action.date});
        }
        case 'SET_POSSIBLE_END_TIME':{
            return Object.assign({}, state, {
                possibleEndTime: action.data});
        }
        case "CHANGE_IS_EMERGENCY":{
            return Object.assign({}, state, {
                isEmergency: action.data});
        }
    default:
      return state
    }
}

export default worker;