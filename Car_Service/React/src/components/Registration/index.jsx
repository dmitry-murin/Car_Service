import React from 'react';
import Form from './form.jsx';
import Header from './../../containers/header.js';
import Footer from './../Footer/index.jsx';


class Registration extends React.Component{
    render(){
        return (<div className="body">
            <Header/>
            <Form registration={this.props.registration} />
            <Footer/>
        </div>);
    }
};
export default Registration;