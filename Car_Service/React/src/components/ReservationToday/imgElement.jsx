import React from 'react';
import moment from 'moment';

class ImgElement extends React.Component{
    constructor(props) {
        super(props);
    }
    render(){
        return (<a href={`${this.props.server}/${this.props.img}`} className="rounded col-4">
                    <img src={`${this.props.server}/${this.props.img}`} className="img-fluid"  />
                </a>);
    }
};

export default ImgElement;