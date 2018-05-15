import React from 'react';
import ReservationForm from './reservation.jsx';
import Header from './../../containers/header';
import Footer from './../Footer/index.jsx';

class Main extends React.Component{
    render(){
        return (<div className="body"> 
                <Header/>
                <ReservationForm
                    workers={this.props.workers}
                    getWorkers={this.props.getWorkers}
                    captchaKey={this.props.captchaKey}
                    changeWorker={this.props.changeWorker}
                    worker={this.props.worker}
                    addReservation={this.props.addReservation}
                    startTime={this.props.startTime}
                    endTime={this.props.endTime}
                    setStartTime={this.props.setStartTimeReservation}
                    setEndTime={this.props.setEndTimeReservation}
                    workDate={this.props.workDate}
                    workTime={this.props.workTime}
                    possibleEndDate={this.props.possibleEndDate}
                    possibleEndTime={this.props.possibleEndTime}
                    checked={this.props.checked}
                    onChangeChecked={this.props.onChangeChecked}
                />
            <Footer/>
        </div>);
    }
};
export default Main;