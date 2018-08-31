import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
//import logo from './assets/images/logo.png';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className="container">
            <a className="navbar-brand js-scroll-trigger logoHeader" href="#page-top"><img src={"/dist/assets/images/logo.png"} alt="" /></a>
                <button className="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
            </button>

                <div className="collapse navbar-collapse" id="navbarResponsive">
                    <ul className="navbar-nav ml-auto">
                        <li className="nav-item">
                            <a className="nav-link js-scroll-trigger" href="#page-top">HOME</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link js-scroll-trigger" href="#howtoenter">HOW TO ENTER</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link js-scroll-trigger" href="#prizes">PRIZES</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link js-scroll-trigger" href="#enternow">ENTER NOW</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link js-scroll-trigger" href="#termsconditions">TERMS & CONDITIONS</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link js-scroll-trigger" href="#contactus">CONTACT US</a>
                        </li>
                    </ul>
                </div>
            </div>;
    }
}
