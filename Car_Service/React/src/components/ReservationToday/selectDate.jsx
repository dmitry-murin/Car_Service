import React from 'react';

import DatePicker from 'react-datepicker';
import moment from 'moment';

import 'react-datepicker/dist/react-datepicker.css';

class SelectDate extends React.Component{
    constructor(props) {
        super(props);
    }
    componentWillMount()
    {
        this.props.changeDate(moment());
    }
    render(){
        return (<div className="mx-auto">
            <h1 className="display-4">Выберете дату:</h1>
            <form ref="Form">
                <div className="form-group">
                    <DatePicker
                        inline
                        className="form-control"
                        todayButton={"Сегодня"}
                        selected={this.props.selectedDate}
                        onChange={this.props.changeDate}
                        dateFormat="LLL"
                    />
                </div>
            </form>
        </div>);
    }
};
export default SelectDate;