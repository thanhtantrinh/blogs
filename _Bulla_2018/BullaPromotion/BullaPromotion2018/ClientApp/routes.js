import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchEmployee } from './components/FetchEmployee';
import { AddEmployee } from './components/AddEmployee';
export var routes = React.createElement(Layout, null,
    React.createElement(Route, { exact: true, path: '/', component: Home }),
    React.createElement(Route, { path: '/fetchemployee', component: FetchEmployee }),
    React.createElement(Route, { path: '/addemployee', component: AddEmployee }),
    React.createElement(Route, { path: '/employee/edit/:empid', component: AddEmployee }));
//# sourceMappingURL=routes.js.map