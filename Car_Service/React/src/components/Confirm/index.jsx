import React from 'react';

import Header from './../../containers/header.js';
import Footer from './../Footer/index.jsx';


class Confirm extends React.Component{
    constructor(props){
        super(props);
    }
    componentWillMount(){
        this.props.reset();
        this.props.getIsConfirm(this.props.match.params.guid);
    }
    render(){
        let preload= <img src="/img/preload.png" alt="" width="300px" height="300px"/>;
        let error =<h1>Неверная ссылка или время подтверждения истекло</h1>;
        let success =<h1>Ваше бронирование успешно подтверждено</h1>;
        console.log(this.props.isConfirm);
        console.log(error);
        return (<div className="body">
            <Header/>
            <div className="d-flex flex-row justify-content-around flex-wrap">
                {this.props.isConfirm==undefined?preload:(this.props.isConfirm?success:error)}
            </div>
            <Footer/>
        </div>);
    }
};
export default Confirm;