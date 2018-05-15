import React from 'react';
import Header from './../../containers/header.js';
import Footer from './../Footer/index.jsx';
import Content from './content.jsx';

class Start extends React.Component{
    render(){
        return (<div className="body">
        <Header/>
        <Content/>
        <Footer/>
        </div>);
    }
};
export default Start;