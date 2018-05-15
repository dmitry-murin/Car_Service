import React from 'react';
import ReCAPTCHA from "react-google-recaptcha";
import DatePicker from 'react-datepicker';
import moment from 'moment';

import SelectWorker from "./../../components/SelectWorker/index.jsx"

class Form extends React.Component{
    constructor(props) {
        super(props);
        this.captcha="";
        this.onChange=this.onChange.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }
    componentWillMount(){
        this.props.getWorkers();
    }
    handleInputChange(e){
        this.props.onChangeChecked(e.target.checked)
    }
    handleChange(e){
        e.preventDefault();
        let captcha = this.captcha;
        let files=[];
        let purpose= this.refs.purpose.value;
        let breakdownDetails= this.refs.breakdownDetails.value;
        let desiredDiagnosis= this.refs.desiredDiagnosis.value;
        let worker= this.props.worker;
        let isEmergency= this.props.checked;
        let dateStart = !isEmergency?(this.props.startTime).toISOString():undefined ;
        let dateEnd = !isEmergency?(this.props.endTime).toISOString():undefined;
        for (var x = 0; x < this.refs.files.files.length; x++) {
            files.push(this.refs.files.files[x]);
        }
        this.props.addReservation(worker,purpose,desiredDiagnosis,breakdownDetails,files, captcha, dateStart, dateEnd, isEmergency)
        this.refs.form.reset();
    }
    onChange(value) {
        this.captcha=value;
    }
    render(){
        return (
            <form ref="form" className="mx-auto">
                <div className="form-group">
                    <label>Цель визита</label>
                    <input type="text" className="form-control" placeholder="Цель визита" required ref="purpose"/>
                </div>
                <div className="form-group">
                    <label>Марка автомобиля</label>
                    <input type="text" className="form-control"  placeholder="Марка автомобиля" required ref="desiredDiagnosis"/>
                </div>
                <div className="form-group">
                    <label className="custom-control custom-checkbox">
                        <input type="checkbox" className="custom-control-input" checked={this.props.checked} onChange={this.handleInputChange}/>
                        <span className="custom-control-indicator"></span>
                        <span className="custom-control-description">Экстренный вызов</span>
                    </label>
                </div>
                {!this.props.checked?<div>
                        <div className="form-group">
                        <label>Выберете мастера</label>
                        <SelectWorker
                            changeSelectedWorker={this.props.changeWorker}
                            workers={this.props.workers}
                        />
                    </div>
                    <div className="form-group">
                        <label>Выберете время</label>
                        <DatePicker
                            className="form-control"
                            selected={this.props.startTime}
                            onChange={this.props.setStartTime}
                            shouldCloseOnSelect={false}
                            showTimeSelect
                            timeFormat="HH:mm"
                            timeIntervals={60}
                            dateFormat="LLL"
                            includeDates = {this.props.workDate}
                            excludeTimes={this.props.workTime}
                        />
                    </div>
                    <div className="form-group">
                        <label>Выберете время</label>
                        <DatePicker
                            className="form-control"
                            selected={this.props.endTime}
                            onChange={this.props.setEndTime}
                            shouldCloseOnSelect={false}
                            showTimeSelect
                            timeFormat="HH:mm"
                            timeIntervals={60}
                            dateFormat="LLL"
                            includeDates = {this.props.possibleEndDate}
                            excludeTimes={this.props.possibleEndTime}
                        />
                    </div>
                </div>:undefined}
                <div className="form-group">
                    <label>Выберете файл(ы)</label>
                    <input type="file" ref="files" name="photo" className="form-control-file" accept="image/*" required multiple title="Загрузите одну или несколько фотографий"/>
                </div>
                <div className="form-group">
                    <label>Комментарии</label>
                    <textarea placeholder="Комементарии к поломке" rows={2} maxLength={64} ref="breakdownDetails" className="form-control"></textarea>
                </div>
                <ReCAPTCHA
                    ref="recaptcha"
                    sitekey={this.props.captchaKey}
                    onChange={this.onChange}
                />
                <button type="submit" className="btn btn-primary" onClick={this.handleChange}>Отправить</button>
            </form>
        );
    }
};

export default Form;