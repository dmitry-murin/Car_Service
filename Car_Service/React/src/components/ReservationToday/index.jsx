import React from 'react';
import ReservationList from './reservationList.jsx';
import SelectDate from './selectDate.jsx';
import Header from './../../containers/header';
import Footer from './../Footer/index.jsx';

class ReservationToday extends React.Component{
    render(){
        return (<div className="body"> 
                <Header/>
                <div className="d-flex flex-row justify-content-around flex-wrap">
                    <SelectDate
                        reservation={this.props.reservation}
                        server={this.props.server}
                        selectedDate={this.props.selectedDate}
                        changeDate={this.props.changeDate}
                    />
                    <ReservationList reservation={this.props.reservation} server={this.props.server}/>
                </div>
            <Footer/>
        </div>);
    }
};
export default ReservationToday;