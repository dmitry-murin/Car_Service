import React from 'react';
import moment from 'moment';

import ImgElement from './imgElement.jsx';

class Element extends React.Component{
    constructor(props) {
        super(props);
    }
    render(){
        return (
        <div className="card">
        <div className={"card-header "+((moment(this.props.element.DateStart)<moment())?"bg-dark":"bg-light")} role="tab" id={"heading"+this.props.index}>
          <h5 className="mb-0">
            <a className="collapsed" data-toggle="collapse" href={"#collapse"+this.props.index} aria-expanded="false" aria-controls={"collapseTwo"+this.props.index}>
            {`${moment(this.props.element.DateStart).format("DD.MM.YYYY HH.mm")}-${moment(this.props.element.DateEnd).format("HH.mm")} ${this.props.element.NameWorker}`}
            </a>
          </h5>
        </div>
        <div id={"collapse"+this.props.index} className="collapse" role="tabpanel" aria-labelledby={"heading"+this.props.index} data-parent="#accordion">
            <div className="card-body">
                <dl className="row">
                    <dt className="col-sm-3">Цель визита</dt>
                    <dd className="col-sm-9">{this.props.element.Purpose}</dd>
                </dl>
                <dl className="row">
                    <dt className="col-sm-3">Марка автомобиля</dt>
                    <dd className="col-sm-9">{this.props.element.DesiredDiagnosis}</dd>
                </dl>
                <dl className="row">
                    <dt className="col-sm-3">Комментарии</dt>
                    <dd className="col-sm-9">{this.props.element.BreakdownDetails}</dd>
                </dl>
                <div className="row">
                    {this.props.element.Image.map((element, index)=>{
                        return <ImgElement key={index} img={element} server={this.props.server}/>
                    })}
                </div>
            </div>
        </div>
      </div>
        );
    }
};

export default Element;