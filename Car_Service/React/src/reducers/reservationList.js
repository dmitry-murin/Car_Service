const initState={
    selectedDate: undefined,
    list: []
};
const reservationList = (state = initState, action) => {
    switch (action.type) {
        case "SET_RESERVATION_LIST": {
            return Object.assign({}, state, {
                list: action.list});
        }
        case "SET_SELECTED_DATE": {
            return Object.assign({}, state, {
                selectedDate: action.selectedDate});
        }
    default:
      return state
    }
}

export default reservationList;