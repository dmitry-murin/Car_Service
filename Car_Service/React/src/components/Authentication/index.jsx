import React from 'react';
import Form from './form.jsx';
import Header from './../../containers/header';
import Footer from './../Footer/index.jsx';


class Authentication extends React.Component{
    render(){
        return (<div className="body">
                <Header/>
                <Form login={this.props.login}/>
                <Footer/>
        </div>);
    }
};
export default Authentication;