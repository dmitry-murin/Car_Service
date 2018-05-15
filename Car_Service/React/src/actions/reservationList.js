export const getReservationList= (date)=>({
    type: 'GET_RESERVATION_LIST',
    date
})
export const setReservationList= (data)=>({
    type: 'SET_RESERVATION_LIST',
    list: data
})
export const setSelectedDate=(date)=>({
    type: 'SET_SELECTED_DATE',
    selectedDate: date
})