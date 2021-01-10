import React, { Component } from "react";
import CaptchaWidget from "./captcha";
import TextFieldGroup from "./TextFieldGroup";
import { FiRefreshCw } from "react-icons/fi";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import * as captchaActions from "./captcha/reducer";

import get from "lodash.get";

export class CaptchaFieldGroup extends Component {
  componentDidMount() {
    this.reloadCaptcha();
  }

  reloadCaptcha = () => {
    this.props.createNewKeyCaptcha();
  };

  render() {
    const { captcha, field, label, value, error, onChange } = this.props;
    return (
      <>
        <div className="form-group row">
          <div className="offset-md-2 d-flex flex-row align-items-center">
            <div className="ml-4">
              <FiRefreshCw className="fa-2x" onClick={this.reloadCaptcha} />
            </div>
            <div className="ml-2">
              <CaptchaWidget {...captcha} />
            </div>
          </div>
        </div>
        <TextFieldGroup
          field={field}
          label={label}
          value={value}
          error={error}
          onChange={onChange}
          autoComplete="off"
        />
      </>
    );
  }
}

CaptchaFieldGroup.propTypes = {
  field: PropTypes.string.isRequired,
  value: PropTypes.string.isRequired,
  label: PropTypes.string.isRequired,
  error: PropTypes.string,
  type: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  onBlur: PropTypes.func
};

CaptchaFieldGroup.defaultProps = {
  type: "text"
};

const mapState = state => {
  return {
    captcha: {
      keyValue: get(state, "captcha.key.data"),
      isKeyLoading: get(state, "captcha.key.loading"),
      isKeyError: get(state, "captcha.key.error"),
      isSuccess: get(state, "captcha.key.success")
    }
  };
};

const mapDispatch = {
  createNewKeyCaptcha: () => {
    return dispatch => dispatch(captchaActions.createNewKey());
  }
};

export default connect(mapState, mapDispatch)(CaptchaFieldGroup);
