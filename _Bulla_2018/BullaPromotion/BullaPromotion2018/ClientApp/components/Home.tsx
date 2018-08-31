import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return     <section id="sec-enternow">
      <div class="container">
        <div class="row">
          <div class="col-lg-12 mx-auto text-center">
            <h2 class="section-heading">ENTER NOW</h2>            
            <p class="mb-5">Submit your business details below and enter the draw for a chance to WIN a $10,000 Business Cash Injection OR one of 12x Weekly Prizes of $1,000! </p>
          </div>
        </div>
        </div>
    </section>;
    }
}
