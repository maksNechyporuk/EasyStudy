import React from "react";
import classnames from "classnames";
import PropTypes from "prop-types";

const AlertGroup = ({
    title,
    alertColor
}) => {
    return (
      <div className={classnames("alert", "alert-dismissible", "fade", "show", alertColor)} role="alert">
            <span>{title}</span>
      </div>
    );
};

AlertGroup.propTypes = {
    title: PropTypes.string.isRequired,
    alertColor: PropTypes.string.isRequired
};

AlertGroup.defaultProps = {
    alertColor: "alert-success"
};

export default AlertGroup;