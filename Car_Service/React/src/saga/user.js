import { put, takeEvery,call } from 'redux-saga/effects';

import { initUser, destroyUser} from './../actions';
import { postURLEncode,postJSON } from './../helpers';
import {NotificationManager} from 'react-notifications';
import history from "./../components/App/history"
import moment from "moment";
const urlToken="http://localhost:29975/api/token";
const urlRegistration ="http://localhost:29975/api/account/regiser";

export function* login(action){
    var details  = {
        username: action.email,
        password: action.pass,
        grant_type: "password"
    }
    var formBody = [];
    for (var property in details) {
      var encodedKey = encodeURIComponent(property);
      var encodedValue = encodeURIComponent(details[property]);
      formBody.push(encodedKey + "=" + encodedValue);
    }
    formBody = formBody.join("&");
    let auth = yield call (postURLEncode, urlToken, formBody);
    if(auth.success)
    {
        console.log(auth.data);
        NotificationManager.success('Success');
        yield put(initUser(auth.data.access_token, auth.data.role)); 
        window.localStorage.setItem("app_token",auth.data.access_token);
        window.localStorage.setItem("app_role",auth.data.role);
        window.localStorage.setItem("token_expire",moment().add("seconds", auth.data.expires_in));
        if(auth.data.role=="admin")
            yield call (history.push,"/admin");
    }
    else{
        NotificationManager.error(auth.data);
    }
        
}
export function* logOut(){
    NotificationManager.success('Success');
    yield put(destroyUser()); 
    window.localStorage.clear("app_token");
    window.localStorage.clear("app_role")
        
}
export function* registration(action){
    let data={
        Email: action.email,
        Password: action.pass
    }
    let registration = yield call (postJSON, urlRegistration, data);
    if(registration.success)
    {
        NotificationManager.success('Success');
        yield call (history.push,"/login");
    }
    else{
        NotificationManager.error(registration.data);
    }
}
export default function* rootSaga() {
    yield takeEvery('LOGIN_USER', login),
    yield takeEvery('LOGOUT_USER', logOut),
    yield takeEvery('REGISTRATION_USER', registration)
}