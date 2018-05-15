import moment from "moment";
const initState={
    captchaKey: "6LfTizUUAAAAAPrKN5EuUDOKNgIBk1ec0aYi3jyD",
    formatDate: "DD.MM.YYYY",
    formatTime: "HH.mm",
    workerURL: "/admin/worker",
    server: "http://localhost:29975",
    user: {token :moment(window.localStorage.getItem("token_expire"))>moment()?window.localStorage.getItem("app_token"):null,
        role: moment(window.localStorage.getItem("token_expire"))>moment()?window.localStorage.getItem("app_role"):null },
    isConfirm: undefined
};
const app = (state = initState, action) => {
    switch (action.type) {
        case "INIT_USER":{
            return Object.assign({}, state, {
                user: {
                    token: action.token,
                    role: action.role
                }});
        }
        case "DESTROY_USER":{
            return Object.assign({}, state, {
                user: {
                    token: null,
                    role: null
                }});
        }
        case "SET_IS_CONFIRM":{
            return Object.assign({}, state, {
                isConfirm: action.status
            });
        }
    default:
      return state
    }
}

export default app;