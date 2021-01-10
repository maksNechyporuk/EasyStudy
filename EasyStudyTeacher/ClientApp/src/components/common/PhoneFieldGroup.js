import React from "react";
import classnames from "classnames";
import PropTypes from "prop-types";
import InputMask from "react-input-mask";

const PhoneFieldGroup = ({
  field,
  value,
  label,
  error,
  type,
  onChange,
  onBlur
}) => {
  return (
    <div className="form-group row">
      <label htmlFor={field} className="col-md-2 col-form-label">
        {label}
      </label>
      <div className="col-md-10">
        <InputMask
          mask="+99 (999) 999 99 99"
          className={classnames("form-control", { "is-invalid": !!error })}
          id={field}
          name={field}
          value={value}
          onChange={onChange}
          onBlur={onBlur}
        />

        {!!error && <div className="invalid-feedback">{error}</div>}
      </div>
    </div>
  );
};

PhoneFieldGroup.propTypes = {
  field: PropTypes.string.isRequired,
  value: PropTypes.string.isRequired,
  label: PropTypes.string.isRequired,
  error: PropTypes.string,
  type: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  onBlur: PropTypes.func
};

PhoneFieldGroup.defaultProps = {
  type: "text"
};

export default PhoneFieldGroup;
