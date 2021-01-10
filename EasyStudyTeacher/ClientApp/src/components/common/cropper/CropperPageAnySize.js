import React, { Component } from "react";
import classnames from "classnames";
import "cropperjs/dist/cropper.css";
import "./Modal.css";
import Cropper from "react-cropper";
import { Card, CardBody, CardFooter } from "reactstrap";
import PropTypes from "prop-types";

const propTypes = {
  isHidden: PropTypes.bool.isRequired
};
const defaultProps = {
  isSmall: false
};

class CropperPageAnySize extends Component {
  state = {
    src: "",
    modal: false
  };

  onChange = e => {
    e.preventDefault();
    let files;
    if (e.dataTransfer) {
      files = e.dataTransfer.files;
    } else if (e.target) {
      files = e.target.files;
    }
    if (files && files[0]) {
      if (files[0].type.match(/^image\//)) {
        const reader = new FileReader();
        reader.onload = () => {
          this.toggle(e);
          this.setState({ src: reader.result });
        };
        reader.readAsDataURL(files[0]);
      } else {
        alert("Невірний тип файлу");
      }
    } else {
      alert("Будь ласка виберіть файл");
    }
  };

  cropImage = e => {
    e.preventDefault();
    if (typeof this.cropperany.getCroppedCanvas() === "undefined") {
      return;
    }
    this.setState({
      src: "",
      modal: false
    });
    this.props.getCroppedImage(this.cropperany.getCroppedCanvas().toDataURL());
  };

  optionCropImage = (e, option, value) => {
    e.preventDefault();

    if (typeof this.cropperany.getCroppedCanvas() === "undefined") {
      return;
    }
    switch (option) {
      case "rotate":
        this.cropperany.rotate(value);
        break;
      case "zoom":
        this.cropperany.zoom(value);
        break;
      default:
        break;
    }
  };

  handleClick = e => {
    this.refs.inputFile.click();
  };

  toggle = e => {
    //e.preventDefault();
    this.setState(prevState => ({
      modal: !prevState.modal
    }));
  };

  render() {
    const {isHidden} = this.props;
    console.log('----props cropper page---', this.props);
    const {modal} = this.state;
    return (
      <div className="px-1">
        <div className={classnames({ "d-none": isHidden })}>
          <label htmlFor="filesany" className="btn btn-success ml-2">
            Виберіть фото довільного розміру
          </label>
          <input
            id="filesany"
            style={{visibility: "hidden"}}
            ref="inputFile"
            type="file"
            onChange={this.onChange}
            onClick={event => {
              event.target.value = null;
            }}
          />
        </div>
        <div className={classnames("modal", { open: modal })}>
          <div className="fluid-container">
            <div className="col-12 col-lg-8">
              <Card>
                <CardBody>
                  <div style={{ width: "100%" }}>
                      <Cropper
                        style={{ height: 400, width: "100%" }}
                        preview=".img-preview"
                        guides={false}
                        viewMode={1}
                        dragMode="move"
                        autoCropArea={1}
                        src={this.state.src}
                        ref={cropperany => {
                          this.cropperany = cropperany;
                        }}
                      />
                  </div>
                </CardBody>
                <CardFooter>
                  <div className="row">
                    <div className="col">
                      <button className="btn btn-success" onClick={this.cropImage}>
                        Обрізати фото
                      </button>
                      <button className="btn btn-danger" onClick={this.toggle}>
                        Скасувати
                      </button>
                    </div>
                    <div className="order-last">
                      <div>
                        <button className="btn btn-info" onClick={e => this.optionCropImage(e, "rotate", -90)}>
                          <i className="fa fa-rotate-left" />
                        </button>
                        <button className="btn btn-info" onClick={e => this.optionCropImage(e, "rotate", 90)}>
                          <i className="fa fa-rotate-right" />
                        </button>

                        <button className="btn btn-info" onClick={e => this.optionCropImage(e, "zoom", 0.1)}>
                          <i className="fa fa-search-plus" />
                        </button>
                        <button className="btn btn-info" onClick={e => this.optionCropImage(e, "zoom", -0.1)}>
                          <i className="fa fa-search-minus" />
                        </button>
                      </div>
                    </div>
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

CropperPageAnySize.propTypes=propTypes;
CropperPageAnySize.defaultProps = defaultProps;

export default CropperPageAnySize;
