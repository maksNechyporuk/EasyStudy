import React, { Component } from "react";
import CropperModal from "./CropperModal";
import PropTypes from "prop-types";

export class ImageFieldGroupCropper extends Component {
  render() {
    const { photo } = this.props;
    let image =
      "https://topdogtours.com/wp-content/uploads/Top-Dog-Tours-Logo-no-Text-300x259.png";
    if (!!photo) {
      image = photo;
    }
    return (
      // <div className="container">
      //   <div className="row">
      <div className="form-group row">
        <div className="col-md-8 col-9 d-flex align-content-center flex-wrap">
          <CropperModal
            setDialog={this.props.setDialog}
            getCroppedImage={this.props.getCroppedImage}
            error={this.props.error}
          />
        </div>
      </div>
      //   </div>
      // </div>
    );
  }
}

ImageFieldGroupCropper.propTypes = {
  getCroppedImage: PropTypes.func.isRequired,
  error: PropTypes.string,
  photo: PropTypes.string.isRequired,
};

ImageFieldGroupCropper.defaultProps = {};

export default ImageFieldGroupCropper;
