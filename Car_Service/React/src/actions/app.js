export const loginUser= (email, pass)=>({
    type: 'LOGIN_USER',
    email,
    pass
})
export const initUser= (token, role)=>({
    type: 'INIT_USER',
    token,
    role
})
export const destroyUser= ()=>({
    type: 'DESTROY_USER'
})
export const logoutUser= ()=>({
    type: 'LOGOUT_USER'
})
export const registrationUser=( email, pass)=>({
    type: 'REGISTRATION_USER',
    email,
    pass
})
export const setIsConfirm=(status)=>({
    type: "SET_IS_CONFIRM",
    status
})
export const getConfirm=(guid)=>({
    type: "GET_CONFIRM",
    guid
})
