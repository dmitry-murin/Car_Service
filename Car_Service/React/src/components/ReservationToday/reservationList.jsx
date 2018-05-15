import React from 'react';
import ReservationElement from './reservationElement.jsx';

class List extends React.Component{
    constructor(props) {
        super(props);
    }
    render(){
        return (
            <div id="accordion" role="tablist" className="w-50 mx-auto">
                {this.props.reservation.map((element, index)=>{
                    return <ReservationElement key={index} element={element} index={index} server={this.props.server}/>
                })}
            </div>
        );
    }
};

export default List;