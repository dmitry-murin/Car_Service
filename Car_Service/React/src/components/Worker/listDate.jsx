import React from 'react';
import moment from 'moment';

class ListDate extends React.Component{
    constructor(props) {
        super(props);
    }
    render(){
        console.log(this.props)
        return (<ul className="list-group">
            {this.props.dates.map((element,num)=>{
                return <div key={num} className="list-group-item list-group-item-action">
                    {moment(element.StartTime).format(this.props.format)} - {moment(element.EndTime).format(this.props.format)}
                </div>
            })}
            </ul>);
    }
};
export default ListDate;