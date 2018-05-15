const initState={
    workers:[]
};
const workers = (state = initState, action) => {
    switch (action.type) {
        case "SET_WORKERS":{
                return Object.assign({}, state, {
                    workers: action.workers
            })
        }
    default:
      return state
    }
}

export default workers;