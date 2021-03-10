import React from "react";
import PropTypes from "prop-types";
import styles from "./TextFieldGroup.module.css";

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
  let errorClass;
  if(error)
  {
    errorClass=`${styles["is-invalid"]}`;

  }
  return (
    <div className={styles["input-container"]}>
      {isShowLabel && <label htmlFor={field} className="col-md-2 col-form-label">
        {label}
      </label> }
      <div >
        <input
          onChange={onChange}
          onBlur={onBlur}
          value={value}
          type={type}
          autoComplete={autoComplete}
          id={field}
          name={field}
          className={errorClass}
          placeholder={placeholder}
        />
        {!!error && <div className={styles["invalid-feedback"]}>{error}</div>}
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
