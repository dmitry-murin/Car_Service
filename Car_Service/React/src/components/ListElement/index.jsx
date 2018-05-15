import React from 'react';
import {
    Link
  } from 'react-router-dom';

class Element extends React.Component{
    render(){
        return (<li>
            <Link className="list-group-item list-group-item-action" to={`${this.props.url}/${this.props.element.id}`}>{this.props.element.name}</Link>
        </li>);
    }
};

export default Element;