import React from 'react';

class SelectWorker extends React.Component{
    constructor(props) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
    }
    handleChange(event) {
        this.props.changeSelectedWorker(parseInt(event.target.value));
    }
    render(){
       
        return (<select className="form-control" value={this.props.selectedWorker} defaultValue="" onChange={this.handleChange}>
            <option value="" disabled>Выберете мастера</option>
            {this.props.workers.map((element, index)=>{
                return <option key={index} value={element.id}>{element.name}</option>
            })}
        </select> );
    }
};
export default SelectWorker;