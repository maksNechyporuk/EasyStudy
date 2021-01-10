import React, { Component } from 'react';

class CaptchaWidgetContainer extends Component {
   
    constructor(props) {
        super(props);

        this.state = {
            keyValue: null
        }

    }
    static getDerivedStateFromProps(nextProps, prevState){
        if(nextProps.keyValue!==prevState.keyValue) {
          return { keyValue: nextProps.keyValue};
       }
       else return null;
     }
    

    render() {
        const { isKeyLoading } = this.props;
        //console.log('-----Captcha props-----', this.props);
        const {keyValue} = this.state;
        // console.log('----Capthca state----', this.state);
        const url= `/api/captchaImage/get-captcha/${keyValue}`;
        const content=(
            <>
                <img src={url} width="200" height="70" alt="captcha" />
            </>
        );
        return (
            isKeyLoading ?
                <div >
                    {/* <img className="img-fluid" src="https://i.gifer.com/ZZ5H.gif"  /> */}
                    <i className="fa fa-spinner fa-pulse fa-fw" style={{fontSize: "55px"}}></i>
                    <span className="sr-only">Loading...</span>
                </div> : content
        )
    }
}

const CaptchaWidget = CaptchaWidgetContainer;
export default CaptchaWidget;

