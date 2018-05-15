import React from 'react';

class SelectWorker extends React.Component{
    constructor(props) {
        super(props);
        this.addWorker = this.addWorker.bind(this);
    }
    addWorker(event) {
        if(this.refs.Form.checkValidity())
        {
            event.preventDefault();
            let surName= this.refs.surName.value;
            let firstName= this.refs.firstName.value;
            let email= this.refs.email.value;
            let telephone= this.refs.tel.value;
            this.props.addWorker(surName, firstName, email, telephone);
            this.refs.Form.reset();
        }   
    }
    render(){
        return (
            <form ref="Form" >
                <div className="form-group" >
                    <label>Name</label>
                    <input type="text" ref="firstName" className="form-control" placeholder="Name" required/>
                </div>
                <div className="form-group">
                    <label>Surname</label>
                    <input type="text" ref="surName" className="form-control" placeholder="Surname" required/>
                </div>
                <div className="form-group">
                    <label>Email address</label>
                    <input type="email" ref="email" className="form-control" aria-describedby="emailHelp" placeholder="Enter email" required/>
                </div>
                <div className="form-group">
                    <label>Telephone</label>
                    <input ref="tel" type="tel" className="form-control" placeholder="Telephone" required/>
                </div>
                <button type="submit" className="btn btn-primary" onClick={this.addWorker}>Добавить</button>
            </form>);
    }
};
export default SelectWorker;