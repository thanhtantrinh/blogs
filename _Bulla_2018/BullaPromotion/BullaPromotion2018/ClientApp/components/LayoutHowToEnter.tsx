import * as React from 'react';
export class LayoutHowToEnter extends React.Component<{}, {}> {
    public render() {
        return <div className="container">
            <div className="row">
                <div className="col-lg-12 text-center">
                    <h2 className="section-heading">HOW TO ENTER</h2>
                    <p>FIll in your details and go into the draw to win your share of over $50K worth of prizes.</p>
                </div>
            </div>
            <div className="row">
                <div className="col-lg-3 col-md-6 text-center">
                    <div className="service-box mt-5 mx-auto">
                        <div className="service-img">
                            <img className="imgstep1" src={"/dist/assets/images/step1.png"} alt="" />
                        </div>
                        <div className="service-intro">
                            <div className="service-index">
                                <span className="fa-stack fa-2x">
                                    <i className="fa fa-circle fa-stack-2x text-primary"></i>
                                    <i className="fa fa-num1 fa-stack-1x fa-inverse"></i>
                                </span>
                            </div>
                            <div className="service-desc">
                                <p className="mb-0 text-left">Buy a specially marked pack of Bulla 10L Ice Cream from an official Bulla Ice Cream distributor in Australia.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="col-lg-3 col-md-6 text-center">
                    <div className="service-box mt-5 mx-auto">
                        <div className="service-img">
                            <img className="imgstep2" src={"/dist/assets/images/step2.jpg"} alt="" />
                        </div>
                        <div className="service-intro">
                            <div className="service-index">
                                <span className="fa-stack fa-2x">
                                    <i className="fa fa-circle fa-stack-2x text-primary"></i>
                                    <i className="fa fa-num2 fa-stack-1x fa-inverse"></i>
                                </span>
                            </div>
                            <div className="service-desc">
                                 <p className="mb-0 text-left">Find the <strong>8 character entry code</strong> on the side of the tub, next to the BEST BEFORE date.</p>
                            </div>
                        </div>  
                    </div>
                </div>
                <div className="col-lg-3 col-md-6 text-center">
                    <div className="service-box mt-5 mx-auto">
                        <div className="service-img">
                            <img className="imgstep3" src={"/dist/assets/images/step3.png"} alt="" />
                        </div>
                        <div className="service-intro">
                            <div className="service-index">
                                <span className="fa-stack fa-2x">
                                    <i className="fa fa-circle fa-stack-2x text-primary"></i>
                                    <i className="fa fa-num3 fa-stack-1x fa-inverse"></i>
                                </span> 
                            </div>
                            <div className="service-desc">
                                 <p className="mb-0 text-left">Fill in your details and code to enter your business into the prize draw. You can enter up to 20 individual codes at at time.</p>
                            </div>
                        </div> 
                    </div>
                </div>
                <div className="col-lg-3 col-md-6 text-center">
                    <div className="service-box mt-5 mx-auto">
                        <div className="service-img">
                            <img className="imgstep4" src={"/dist/assets/images/step4.png"} alt="" />
                        </div>
                        <div className="service-intro">
                            <div className="service-index">
                              <span className="fa-stack fa-2x">
                                <i className="fa fa-circle fa-stack-2x text-primary"></i>
                                <i className="fa fa-num4 fa-stack-1x fa-inverse"></i>
                            </span>
                            </div>
                            <div className="service-desc">
                                 <p className="mb-0 text-left">We will confirm your successful entry on-screen and via email.</p>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
            <div className="row">
                <div className="col-12 mx-auto mt-4">
                    <div className="mx-auto mt-4 text-center">                        
                        <a className="btn-link-bulla ml-5" href="#" role="button">TERMS AND CONDITIONS</a>
                    </div>
                </div>
           </div>
        </div>;
    }
}

