import React from 'react';

import DatePicker from 'react-datepicker';
import moment from 'moment';

import Header from './../Header/index.jsx';
import Footer from './../Footer/index.jsx';


import 'react-datepicker/dist/react-datepicker.css';

class Main extends React.Component{
    constructor(props) {
        super(props);
        this.addWorkTime=this.addWorkTime.bind(this);
    }
    addWorkTime(event){
        event.preventDefault();
        let id=this.props.worker.id;
        let startTime = this.props.startTime;
        let endTime= this.props.endTime;
        this.props.addWorkTime(id, startTime, endTime);
        this.props.reset();
    }
    render(){
        return (<div className="mx-auto">
            <h1 className="display-4">{this.props.worker.name}</h1>
            <form ref="Form">
                <div className="form-group">
                    <label>Start working time</label>
                    <DatePicker
                        className="form-control"
                        minDate={moment()}
                        maxDate={moment().add(1,"months")}
                        selected={this.props.startTime}
                        onChange={this.props.setStartTime}
                        shouldCloseOnSelect={false}
                        showTimeSelect
                        timeFormat="HH:mm"
                        timeIntervals={60}
                        dateFormat="LLL"
                    />
                </div>
                <div className="form-group">
                    <label>End working time</label>
                    <DatePicker
                        className="form-control"
                        minDate={moment()}
                        maxDate={moment().add(1,"months")}
                        selected={this.props.endTime}
                        onChange={this.props.setEndTime}
                        shouldCloseOnSelect={false}
                        showTimeSelect
                        timeFormat="HH:mm"
                        timeIntervals={60}
                        dateFormat="LLL"
                    />
                </div>
                <button type="submit" className="btn btn-primary" onClick={this.addWorkTime}>Добавить время</button>
            </form>
        </div>);
    }
};
export default Main;