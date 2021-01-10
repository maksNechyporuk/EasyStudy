import React from "react";
import classnames from "classnames";
import PropTypes from "prop-types";

const TextFieldGroup = ({
  field,
  value,
  label,
  error,
  type,
  autoComplete,
  onChange,
  onBlur,
  placeholder,
  isShowLabel
}) => {
  return (
    <div className="form-group row">
      {isShowLabel && <label htmlFor={field} className="col-md-2 col-form-label">
        {label}
      </label> }
      <div className={classnames(isShowLabel ? "col-md-10" : "col-12")}>
        <input
          onChange={onChange}
          onBlur={onBlur}
          value={value}
          type={type}
          autoComplete={autoComplete}
          id={field}
          name={field}
          className={classnames("form-control", {
            "is-invalid": !!error
          })}
          placeholder={placeholder}
        />
        {!!error && <div className="invalid-feedback">{error}</div>}
      </div>
    </div>
  );
};

TextFieldGroup.propTypes = {
  field: PropTypes.string.isRequired,
  value: PropTypes.string.isRequired,
  label: PropTypes.string.isRequired,
  error: PropTypes.string,
  type: PropTypes.string.isRequired,
  autoComplete: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  onBlur: PropTypes.func,
  placeholder: PropTypes.string,
  isShowLabel: PropTypes.bool
};

TextFieldGroup.defaultProps = {
  type: "text",
  autoComplete: "on",
  isShowLabel: true
};

export default TextFieldGroup;
