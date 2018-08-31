import * as React from 'react';
import { NavMenu } from './NavMenu';
import { LayoutHeader } from './LayoutHeader';
import { LayoutHowToEnter } from './LayoutHowToEnter';
import { LayoutPrizes } from './LayoutPrizes';
import { LayoutFooter } from './LayoutFooter';
export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
        return <div className="">
            <nav className="navbar navbar-expand-lg navbar-light" id="mainNav">
                <NavMenu />
            </nav>

            <header className="masthead text-center text-white d-flex">
                <LayoutHeader />
            </header>

            <section id="services">
                <LayoutHowToEnter />
            </section>

            <section id="sec-prizes" className="">
                <LayoutPrizes />
            </section>

            <div className="container">
                <div className='row'>
                    <div className='col-sm-12'>
                        {this.props.children}
                    </div>
                </div>
            </div>

            <section id="sec-contact" className="text-white">
              <div className="container">
                <div className="row justify-content-md-center">
                  <div className="col col-3">
                      <h3>CONTACT US</h3>
                    </div>             
                    <div className="col col-9">                      
                      <p className="ml-3">
                            Have a question about the promotion? <br/>
Contact us by <a className="clicking-link" href="#">clicking here</a> or by calling <a className="call-link" href="#">1800 001 332</a>.
                        </p>
                    </div>

                </div>
              </div>
            </section>

            <section id="contact">
                <LayoutFooter />
            </section>
        </div>;
    }
}
