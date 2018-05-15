import React from 'react';
import AddWorker from './addWorker.jsx';
import ListWorkers from './listWorker.jsx';
import Header from './../../containers/header';
import Footer from './../Footer/index.jsx';

class Main extends React.Component{
    constructor(props) {
        super(props);
        this.props.initWorkers();
    }
    render(){
        return (<div className="body">
            <Header/>
            <div className="d-flex flex-row justify-content-around flex-wrap">
                <AddWorker 
                    addWorker={this.props.addWorker}
                />
                <ListWorkers
                    workers={this.props.workers}
                    url={this.props.url}
                />
            </div>
            <Footer/>
        </div>);
    }
};
export default Main;