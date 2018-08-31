import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class LayoutHeader extends React.Component<{}, {}> {
    public render() {
        return <div className="container">
            <div className="row">
                <div className="col-lg-10 mx-auto mt-4">
                    <img className="heroimage" src={"/dist/assets/images/heroimage.png"} alt="" />
                    <div className="box-gift mx-auto mt-4">
                        <img src={"/dist/assets/images/gift10000.png"} alt="" />
                    </div>
                    <div className="mx-auto mt-5">
                        <a className="btn btn-bulla enter-now" href="#" role="button">Enter Now</a>
                    </div>
                    <div className="mx-auto mt-4">
                        <a className="btn-link-bulla" href="#" role="button">HOW TO ENTER</a> 
                        <a className="btn-link-bulla ml-5" href="#" role="button">TERMS AND CONDITIONS</a>
                    </div>
                </div>
            </div>
        </div>;
    }
}
