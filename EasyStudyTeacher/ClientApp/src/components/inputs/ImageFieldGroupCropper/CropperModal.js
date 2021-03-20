import React, { Component } from "react";
import classnames from "classnames";
import "cropperjs/dist/cropper.css";
import "./Modal.scss";
import Cropper from "react-cropper";
import { Card, CardBody, CardFooter } from "reactstrap";
import PropTypes from "prop-types";
import FileFieldGroup from "../FileFieldGroup/FileFieldGroup";

class CropperModal extends Component {
  state = {
    src: "",
    label: "",
    modal: false,
  };

  onChange = (e) => {
    console.log("onChange");
    e.preventDefault();
    //this.props.setDialog(false);

    //console.log("-------john hello---------", e.target.value);
    let files;
    if (e.dataTransfer) {
      files = e.dataTransfer.files;
    } else if (e.target) {
      files = e.target.files;
    }
    if (files && files[0]) {
      if (files[0].type.match(/^image\//)) {
        const file_name = files[0].name;
        const reader = new FileReader();
        reader.onload = () => {
          this.toggle(e);
          this.setState({ src: reader.result, label: file_name });
        };
        reader.readAsDataURL(files[0]);
      } else {
        alert("Невірний тип файлу");
      }
    } else {
      alert("Будь ласка виберіть файл");
    }
  };

  cropImage = (e) => {
    console.log("cropImage");

    e.preventDefault();
    if (typeof this.cropper.getCroppedCanvas() === "undefined") {
      return;
    }
    this.setState({
      src: "",
      modal: false,
    });
    this.props.getCroppedImage(this.cropper.getCroppedCanvas().toDataURL());
  };

  optionCropImage = (e, option, value) => {
    e.preventDefault();
    console.log("optionCropImage");
    if (typeof this.cropper.getCroppedCanvas() === "undefined") {
      return;
    }
    switch (option) {
      case "rotate":
        this.cropper.rotate(value);
        break;
      case "zoom":
        this.cropper.zoom(value);
        break;
      default:
        break;
    }
  };

  toggle = (e) => {
    console.log("toggle");

    //e.preventDefault();
    this.setState((prevState) => ({
      modal: !prevState.modal,
    }));
  };

  render() {
    //
    //console.log('----props cropper page---', this.props);
    const { modal, label } = this.state;
    const { error } = this.props;
    return (
      <div className="w-100">
        <FileFieldGroup
          field="image"
          label={label}
          value=""
          error={error}
          onChange={this.onChange}
          type="file"
        />

        <div className={classnames("modal", { open: modal })}>
          <div className="fluid-container">
            <div className="col-12 col-lg-8">
              <Card>
                <CardBody>
                  <div style={{ width: "100%" }}>
                    <Cropper
                      style={{ height: 400, width: "100%" }}
                      preview=".img-preview"
                      aspectRatio={1 / 1}
                      guides={false}
                      viewMode={1}
                      dragMode="move"
                      src={this.state.src}
                      ref={(cropper) => {
                        this.cropper = cropper;
                      }}
                    />
                  </div>
                </CardBody>
                <CardFooter>
                  <div className="row">
                    <div
                      className="col"
                      style={{ display: "flex", justifyContent: "center" }}
                    >
                      <button
                        className="btn btn-success"
                        onClick={this.cropImage}
                      >
                        Обрізати фото
                      </button>
                      <button className="btn btn-danger" onClick={this.toggle}>
                        Скасувати
                      </button>
                    </div>
                    {/* <div className="order-last">
                      <div>
                        <button
                          className="btn btn-info"
                          onClick={(e) =>
                            this.optionCropImage(e, "rotate", -90)
                          }
                        >
                          <i className="fa fa-rotate-left" />
                        </button>
                        <button
                          className="btn btn-info"
                          onClick={(e) => this.optionCropImage(e, "rotate", 90)}
                        >
                          <i className="fa fa-rotate-right" />
                        </button>

                        <button
                          className="btn btn-info"
                          onClick={(e) => this.optionCropImage(e, "zoom", 0.1)}
                        >
                          <i className="fa fa-search-plus" />
                        </button>
                        <button
                          className="btn btn-info"
                          onClick={(e) => this.optionCropImage(e, "zoom", -0.1)}
                        >
                          <i className="fa fa-search-minus" />
                        </button>
                      </div>
                    </div> */}
                  </div>
                </CardFooter>
              </Card>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

const propTypes = {
  getCroppedImage: PropTypes.func.isRequired,
  error: PropTypes.string,
};
const defaultProps = {
  //isSmall: false
};

CropperModal.propTypes = propTypes;
CropperModal.defaultProps = defaultProps;

export default CropperModal;
