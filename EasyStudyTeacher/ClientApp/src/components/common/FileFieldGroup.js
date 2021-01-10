import React from "react";
import classnames from "classnames";
import PropTypes from "prop-types";

const FileFieldGroup = ({
    field,
    value,
    label,
    error,
    type,
    onChange,
    onBlur
}) => {
    return (
        <div className="custom-file">
            <input
                onChange={onChange}
                onBlur={onBlur}
                value={value}
                type={type}
                id={field}
                name={field}
                className={classnames("custom-file-input", {
                    "is-invalid": !!error
                })}
            />

            <label className="custom-file-label" htmlFor={field}>
                {label}
            </label>
            {!!error && <div className="invalid-feedback">{error}</div>}
        </div>
    );
};

FileFieldGroup.propTypes = {
    field: PropTypes.string.isRequired,
    value: PropTypes.string.isRequired,
    label: PropTypes.string.isRequired,
    error: PropTypes.string,
    type: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired,
    onBlur: PropTypes.func
};

FileFieldGroup.defaultProps = {
    type: "file"
};

export default FileFieldGroup;