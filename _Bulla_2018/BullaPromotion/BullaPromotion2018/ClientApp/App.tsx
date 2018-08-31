import './assets/fonts/FontAwesome.otf';
import './assets/fonts/fontawesome-webfont.svg';
import './assets/fonts/fontawesome-webfont.ttf';
import './assets/fonts/fontawesome-webfont.woff';
import './assets/fonts/fontawesome-webfont.woff2';

//import './assets/fonts/fa-brands-400.eot';
//import './assets/fonts/fa-brands-400.svg';
//import './assets/fonts/fa-brands-400.ttf';
//import './assets/fonts/fa-brands-400.woff';
//import './assets/fonts/fa-brands-400.woff2';

//import './assets/fonts/fa-regular-400.eot';
//import './assets/fonts/fa-regular-400.svg';
//import './assets/fonts/fa-regular-400.ttf';
//import './assets/fonts/fa-regular-400.woff';
//import './assets/fonts/fa-regular-400.woff2';

//import './assets/fonts/fa-solid-900.eot';
//import './assets/fonts/fa-solid-900.svg';
//import './assets/fonts/fa-solid-900.ttf';
//import './assets/fonts/fa-solid-900.woff';
//import './assets/fonts/fa-solid-900.woff2';

import './assets/fonts/GothamBold.eot';
//import './assets/fonts/GothamBold.otf';
import './assets/fonts/GothamBold.svg';
import './assets/fonts/GothamBold.ttf';
import './assets/fonts/GothamBold.woff';

import './assets/fonts/GothamBook.eot';
import './assets/fonts/GothamBook.otf';
import './assets/fonts/GothamBook.svg';
import './assets/fonts/GothamBook.ttf';
import './assets/fonts/GothamBook.woff';

import './assets/fonts/GothamUltra.eot';
import './assets/fonts/GothamUltra.otf';
import './assets/fonts/GothamUltra.svg';
import './assets/fonts/GothamUltra.ttf';
import './assets/fonts/GothamUltra.woff';

import './assets/images/logo.png';
import './assets/images/heroimage.png';
import './assets/images/gift10000.png';
import './assets/images/step1.png';
import './assets/images/step2.jpg';
import './assets/images/step3.png';
import './assets/images/step4.png';

import './assets/sass/Styles.scss';

//import './assets/images/heroImage.png';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { BrowserRouter } from 'react-router-dom';
import * as RoutesModule from './routes';

let routes = RoutesModule.routes;

function renderApp() {
    // This code starts up the React app when it runs in a browser. It sets up the routing
    // configuration and injects the app into a DOM element.
    const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
    ReactDOM.render(
        <AppContainer>
            <BrowserRouter children={routes} basename={baseUrl} />
        </AppContainer>,
        document.getElementById('root')
    );
}

renderApp();

// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./routes', () => {
        routes = require<typeof RoutesModule>('./routes').routes;
        renderApp();
    });
}

