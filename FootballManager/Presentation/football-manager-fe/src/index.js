import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { Home } from './Home';
import { Teams } from './Teams';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Team from './Teams';

ReactDOM.render(

    <BrowserRouter>

        <div>

            <Route component={App} />
            <Route path='/home' component={Home} />
            <Route path='/team'> <Team/> </Route>


        </div>

    </BrowserRouter>,

    document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
