import axios from "axios";
import moment from "moment";


export const postURLEncode=(url, data)=>{
  return axios({
        method: 'post',
        headers: {
                'Content-type': 'application/x-www-form-urlencoded'
            },
        url,
        data
    })
      .then(res => {
            if(res.status==200)
              return {
                success: true,
                  data: res.data
              }
            else return {
                success: false,
                data: res.data.Message!=null?res.data.Message:"unknown error"
            }
      })
      .catch(error => {
        return {
            success: false,
            data: error.response.data.error_description  
        }
      });
}
export const postJSON=(url, data)=>{
    return axios({
        method: 'post',
        headers: {
                'Authorization': 'Bearer '+window.localStorage.getItem("app_token")
            },
        url,
        data
    })
    .then(res => {
            if(res.status==200)
            return {
                success: true,
                data: res.data
            }
            else return {
                success: false,
                data: res.data.Message!=null?res.data.Message:"unknown error"
            }
    })
    .catch(error => {
        return {
            success: false,
            data: error.response.data.Message!=null?error.response.data.Message:"unknown error"
        }
    });
}
export const getJSON=(url)=>{
    return axios({
        method: 'get',
        headers: {
                'Authorization': 'Bearer '+window.localStorage.getItem("app_token")
            },
        url
    })
    .then(res => {
            if(res.status==200)
            return {
                success: true,
                data: res.data
            }
            else return {
                success: false,
                data: res.data.Message!=null?res.data.Message:"unknown error"
            }
    })
    .catch(error => {
        return {
            success: false,
            data: error.response.data.Message!=null?error.response.data.Message:"unknown error"
        }
    });
}
export const compareDate=(a,b)=>{
    console.log("OK");
    if (moment(a.StartTime) < moment(b.StartTime))
      return -1;
    if (moment(a.StartTime) > moment(b.StartTime))
      return 1;
    return 0;
  }