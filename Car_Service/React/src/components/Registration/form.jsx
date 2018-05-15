import React from 'react';

class Form extends React.Component{
    constructor(props) {
        super(props);
        this.validatePassword = this.validatePassword.bind(this);
        this.handle = this.handle.bind(this);
    }
    validatePassword(){
        var pass2=this.refs.Password1.value;
        var pass1=this.refs.Password2.value;
        if(pass1!=pass2)
            this.refs.Password2.setCustomValidity("Passwords Don't Match");
        else
            this.refs.Password2.setCustomValidity('');
    }
    handle(event){
        if(this.refs.Form.checkValidity())
        {
            event.preventDefault();
            let email=this.refs.Email.value;
            let password = this.refs.Password1.value;
            this.props.registration(email, password);
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
                    <input ref="Password1" type="password" className="form-control" placeholder="Password" required onChange={this.validatePassword}/>
                </div>
                <div className="form-group">
                    <label>Confirm password</label>
                    <input ref="Password2" type="password" className="form-control" placeholder="Confirm password" required onChange={this.validatePassword}/>
                </div>
                <button type="submit" className="btn btn-primary" onClick={this.handle}>Зарегестрироватся</button>
            </form>
        );
    }
};

export default Form;