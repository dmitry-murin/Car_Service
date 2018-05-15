import React from 'react';

import Header from './../../containers/header';
import Footer from './../Footer/index.jsx';
import AddWorkDate from './addWorkDate.jsx'
import ListDate from './listDate.jsx';

class Worker extends React.Component{
    constructor(props){
        super(props);
    }
    componentWillMount(){
        this.props.getWorker(this.props.match.params.id);
        this.props.getWorkTime(this.props.match.params.id);
    }
    render(){
        return (<div className="body">
                <Header/>
                <div className="d-flex flex-row justify-content-around flex-wrap">
                    <AddWorkDate 
                        addWorkTime={this.props.addWorkTime}
                        reset={this.props.reset}
                        setStartTime= {this.props.setStartTime}
                        setEndTime = {this.props.setEndTime}
                        worker={this.props.worker}
                        startTime={this.props.startTime}
                        endTime={this.props.endTime}
                    />
                    <ListDate dates={this.props.workTime} format={'DD.MM.YYYY HH:mm'}/>
                </div>
            <Footer/>
        </div>);
    }
};
export default Worker;