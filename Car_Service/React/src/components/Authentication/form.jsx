import React from 'react';

class Form extends React.Component{
    constructor(props) {
        super(props);
        this.handle = this.handle.bind(this);
    }
    handle(event){
        if(this.refs.Form.checkValidity())
        {
            event.preventDefault();
            let email= this.refs.Email.value;
            let password = this.refs.Password.value;
            this.props.login(email, password);
        }
            
    }
    render(){
        return (
            <form ref="Form" className="mx-auto">
                <div className="form-group">
                    <label>Email address</label>
                    <input type="email" ref="Email" className="form-control" aria-describedby="emailHelp" placeholder="Enter email" required/>
                </div>
                <div className="form-group">
                    <label>Password</label>
                    <input ref="Password" type="password" className="form-control" placeholder="Password" required/>
                </div>
                <button type="submit" className="btn btn-primary" onClick={this.handle}>Войти</button>
            </form>
        );
    }
};
export default Form;