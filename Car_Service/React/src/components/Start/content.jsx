import React from 'react';
import {
    Link
  } from 'react-router-dom';

class Content extends React.Component{
    render(){
        return (
            <div className="card text-center">
    <div className="card-body">
      <h4 className="card-title">Добро пожаловать</h4>
      <p className="card-text">
          Мы рады видеть вас на нашем сайте. Здесь вы можете забронировать время для вашего посещения.
          Для этого вам нужно войти или зарегестрироваться.
      </p>
      <p className="card-text"><small className="text-muted">Всего доброго :) Экономьте свое время</small></p>
    </div>
  </div>
);
    }
};
export default Content;