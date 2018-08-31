import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class LayoutFooter extends React.Component<{}, {}> {
    public render() {
        return <div className="container">
                <div id="footer" className="row justify-content-md-center">
                  <div className="col col-2 logo-block">
                      <img src={"/dist/assets/images/logo.png"} alt="" />
                    </div>             
                    <div className="col col-9">
                      <h3>TERMS AND CONDITIONS</h3>
                      <p>Regal Cream Products Pty Ltd as trustee for the Regal Cream Products Trust, trading as Bulla Dairy Foods, (ABN 11 845 336 184) of 15 Swann Drive, Derrimut, VIC 3030. Permit numbers: NSW LTPS/17/18338, ACT TP17/01975 and SA T17/1864. Entry is only open to authorised representatives of businesses operating in Australia. Promotion ends at 11:59pm AEDST on 28/02/2018.</p>
                    </div>
                </div>                          
              </div>;
    }
}
