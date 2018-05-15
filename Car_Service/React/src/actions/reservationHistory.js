export const getReservationHistory= ()=>({
    type: 'GET_RESERVATION_HISTORY'
})
export const setReservationHistory= (data)=>({
    type: 'SET_RESERVATION_HISTORY',
    reservations: data
})