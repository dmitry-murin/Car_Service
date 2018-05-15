import React from 'react';
import Header from './../../containers/header';
import Footer from './../Footer/index.jsx';

import ReservationElement from './reservationElement.jsx';

class Main extends React.Component{
    constructor(props){
        super(props);
    }
    componentWillMount(){
        this.props.load()
    }
    render(){
        return (<div className="body"> 
                <Header/>
                <h1 className="display-4">История посещения СТО:</h1>
                <div id="accordion" role="tablist" className="w-50 mx-auto">
                    {this.props.reservation.map((element, index)=>{
                        return <ReservationElement key={index} element={element} index={index} server={this.props.server}/>
                    })}
                </div>
            <Footer/>
        </div>);
    }
};
export default Main;