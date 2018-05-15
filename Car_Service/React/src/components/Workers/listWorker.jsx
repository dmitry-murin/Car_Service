import React from 'react';
import ListElement from "./../ListElement/index.jsx"

class ListWorker extends React.Component{
    constructor(props) {
        super(props);
    }
    render(){
        return (<ul className="list-group">
            {this.props.workers.map((element,num)=>{
                return <ListElement
                    key={num}
                    url={this.props.url}
                    element={element}
                />
            })}
            </ul>);
    }
};
export default ListWorker;