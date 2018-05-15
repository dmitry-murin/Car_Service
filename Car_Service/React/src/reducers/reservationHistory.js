const initState={
    reservations:[]
};
const reservationHistory = (state = initState, action) => {
    switch (action.type) {
        case "SET_RESERVATION_HISTORY": {
            return Object.assign({}, state, {
                reservations: action.reservations});
        }
    default:
      return state
    }
}

export default reservationHistory;