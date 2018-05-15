import React from 'react';
import {
    Link
  } from 'react-router-dom';


class Header extends React.Component{
    constructor(props){
        super(props);
    }
    render(){
        return (<nav className="header navbar navbar-light bg-light justify-content-between">
            <li className="navbar-brand" >
                <Link to={this.props.role=="admin"?"/admin":"/"}>
                    <img src="/img/icon.png" width="50" height="60" className="d-inline-block align-top" alt=""/>
                </Link>
                <span className="h1">Car Service</span>
                
            </li>
                {this.props.isLogin?
                <li className="nav-item">
                    {this.props.role=="admin"?<Link to="/admin/worker" className="navbar-brand">
                        <button className="btn btn-outline-primary my-2 my-sm-0" type="button">Рабочие</button>
                    </Link>:<Link to="/profile" className="navbar-brand">
                        <button className="btn btn-outline-primary my-2 my-sm-0" type="button">Профиль</button>
                    </Link>}
                    <button className="btn btn-outline-primary my-2 my-sm-0" type="button" onClick={this.props.logoutUser}>Выйти</button>
                </li>
                :
                <li className="nav-item">
                    <Link to="/login" className="navbar-brand">
                    <button className="btn btn-outline-primary my-2 my-sm-0" type="button">Войти</button>
                    </Link>
                    <Link className="navbar-brand" to="/registration">
                        <button className="btn btn-outline-primary my-2 my-sm-0" type="button">Регистрация</button>
                    </Link>
                </li>}
          </nav>);
    }
};

export default Header;