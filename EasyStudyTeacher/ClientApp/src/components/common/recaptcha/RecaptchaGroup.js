import React from "react";
import classnames from "classnames";
import PropTypes from 'prop-types'


import ReCAPTCHA from "react-google-recaptcha";
import { recaptchaSiteKey } from '../../../config';

export const RecaptchaGroup = React.forwardRef((props,ref)=>{
    return (
        <div className="form-group row justify-content-center recaptchaGroup">
            <ReCAPTCHA
                sitekey={recaptchaSiteKey}
                onChange={props.onChange}
                className={classnames(`g-recaptcha`,props.className, !!props.error && "is-invalid")}
                ref={ref}
            />
            {!!props.error && <div className="invalid-feedback col-12">{props.error}</div>}
        </div>
        )
})
RecaptchaGroup.propTypes = {
    onChange: PropTypes.func,
    className: PropTypes.string,
    error: PropTypes.string,
};
export default RecaptchaGroup;