import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import './main.css';
import { App } from './App.jsx';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <BrowserRouter basename={baseUrl}>
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </BrowserRouter>
);

