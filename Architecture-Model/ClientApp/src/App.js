import React, { Component } from 'react';
import { Route, Redirect } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Login } from './authentication/login';

import './custom.css'

function getProtectedRoute(path, component)
{
    let userToken = sessionStorage.getItem("userToken");
    if (!userToken)
    {
        return <Redirect exact path={path} to="/login" />;
    }

    return <Route exact path={path} component={component} />;
}

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} /> 
        <Route path='/login' component={Login} /> 
            {getProtectedRoute("/admin", Counter) }
      </Layout>
    );
  }
}
