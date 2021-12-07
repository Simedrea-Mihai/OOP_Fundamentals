import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { Home } from './Home';
import { Teams } from './Teams';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Team from './Teams';
import Register from './Register'

ReactDOM.render(

    <BrowserRouter>

        <div>

            <Route component={App} />
            <Route path='/home' component={Home} />
            <Route path='/team'> <Team /> </Route>
            <Route path='/sign_in'> <Register /> </Route>


        </div>

    </BrowserRouter>,

    document.getElementById('root')
);

reportWebVitals();
