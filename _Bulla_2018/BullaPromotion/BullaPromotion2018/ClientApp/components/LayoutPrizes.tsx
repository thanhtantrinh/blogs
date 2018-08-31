import * as React from 'react';
export class LayoutPrizes extends React.Component<{}, {}> {
    public render() {
        return <div className="container text-center">                
                <div className="row">
                    <div className="col-lg-6 mx-auto">
                        <h2 className="sec-prizes-title mb-3">PRIZES</h2>

                        <div className="sec-prizes-content">
                            <div className="prizes-content">
                                <p><strong className="text-primary">FOUR LUCKY WINNERS</strong> will each receive the <br/> <strong className="text-dark">$10,000</strong> cash Major Prize for their business!</p>
                                <p><strong className="text-primary">TWELVE WINNERS</strong> (1 per week for 12 weeks) will
                                    <br/>each receive a <strong className="text-dark">$10,000</strong> cash prize as part of the
                                    <br/>weekly Scoop to Win Draw!
                                </p>
                            </div>
                        </div>

                        <div className="mx-auto mt-1">
                            <a className="btn btn-bulla enter-now" href="#" role="button">Enter Now</a>
                        </div>
                        <div className="mx-auto mt-4">                   
                            <a className="btn-link-bulla" href="#" role="button">TERMS AND CONDITIONS</a>
                        </div>
                    </div>
                </div>
              </div>;
    }
}