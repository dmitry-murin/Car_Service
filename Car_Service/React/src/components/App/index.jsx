import React from 'react';
import {Link} from 'react-router-dom';
import {
    Router,
    Route,
    Redirect,
    Switch
  } from 'react-router-dom';
import {NotificationContainer} from 'react-notifications';
import history from "./history.js";

import Start from './../Start/index.jsx';

import Registration from './../../containers/registration.js';
import Main from './../../containers/reservation.js'
import Authentication from './../../containers/authentication.js';
import Workers from './../../containers/workers.js';
import Worker from './../../containers/worker.js';
import ReservationList from './../../containers/reservationList.js';
import Profile from './../../containers/profile.js';
import Confirm from './../../containers/confirm.js';



class App extends React.Component{
    render(){
        return (
            <div>
            <Router history={history}>
                <Switch>
                    <Route exact path="/" render={(props) => (this.props.user.token?<Main {...props}/>:<Start {...props}/>)}/>
                    <Route path="/login" render={(props) => (this.props.user.token?(<Redirect  to="/"/>):<Authentication {...props}/>)}/>
                    <Route path={"/confirm/:guid"} component={Confirm}/>
                    <Route path="/profile" render={(props) => (this.props.user.token?(<Profile {...props}/>):<Redirect  to="/"/>)}/>
                    <Route path="/registration" render={(props) => (this.props.user.token?(<Redirect to="/"/>):<Registration {...props}/>)}/>
                    <Route path="/admin" render={() => (this.props.user.role=="admin"?(<Switch>
                        <Route exact path="/admin" component={ReservationList}/>
                        <Route path={"/admin/worker/:id"} component={Worker}/>
                        <Route path="/admin/worker" component={Workers}/>
                    </Switch>):(<Redirect to="/login"/>))}/>
                </Switch> 
            </Router>
            <NotificationContainer/>
            </div>
        );
    }
};

export default App;