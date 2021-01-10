import React, { Component } from "react";
import TextFieldGroup from "../../../common/TextFieldGroup";
import PhoneFieldGroup from "../../../common/PhoneFieldGroup";
import EclipseWidget from "../../../common/eclipse";
import "../../auth.scss";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";

import { validateFields } from "./validate";

export class RegisterPage extends Component {
  state = {
    lastName: "",
    firstName: "",
    middleName: "",
    email: "",
    phone: "",
    age: 0,
    errors: this.props.errors,
    loading: this.props.loading,
  };

  UNSAFE_componentWillReceiveProps = (nextProps) => {
    console.log("Change props ");
    console.log(this.props);

    this.setState({
      loading: nextProps.loading,
      errors: nextProps.errors,
    });
  };

  setStateByErrors = (name, value) => {
    if (!!this.state.errors[name]) {
      let errors = Object.assign({}, this.state.errors);
      delete errors[name];
      this.setState({
        [name]: value,
        errors,
      });
    } else {
      this.setState({ [name]: value });
    }
  };

  handleChange = (e) => {
    this.setStateByErrors(e.target.name, e.target.value);
  };
  handleSubmit = (e) => {
    e.preventDefault();
    console.log("model");
    // console.log("--register submit--");
    //const { email, photo } = this.state;
    let errors = validateFields(this.state);
    // let errors = {};
    // if (email === "") errors.email = "Поле не може бути пустим!";
    // if (photo === "") errors.photo = "Закинь фотку!";
    const isValid = Object.keys(errors).length === 0;

    if (isValid) {
      const model = {
        FirstName: this.state.firstName,
        MiddleName: this.state.middleName,
        LastName: this.state.lastName,
        Email: this.state.email,
        phoneNumber: this.state.phone,
        age: this.state.DayOfbirthday,
      };
      console.log(model);
      this.props.registerUser(model);
      // console.log("Model is Valid");
      // console.log("Model is Valid");
      //ajax axios post
    } else {
      this.setState({ errors });
    }
  };

  render() {
    const {
      middleName,
      lastName,
      firstName,
      email,
      phone,
      age,
      loading,
      errors,
    } = this.state;
    // const { captcha } = this.props;
    console.log("Regiter page state", this.state);

    //console.log(image);
    return (
      <>
        <div className="main-auth">
          <div class="form">
            <div class="thumbnail">
              <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/169963/hat.svg" />
            </div>
            <form class="login-form">
              <TextFieldGroup
                field="lastName"
                label="Прізвище"
                value={lastName}
                error={errors.lastName}
                onChange={this.handleChange}
              />

              <TextFieldGroup
                field="firstName"
                label="Ім'я"
                value={firstName}
                error={errors.firstName}
                onChange={this.handleChange}
              />
              <TextFieldGroup
                field="middleName"
                label="По батькові"
                value={middleName}
                error={errors.middleName}
                onChange={this.handleChange}
              />
              <TextFieldGroup
                field="email"
                label="Електронна пошта"
                value={email}
                error={errors.email}
                onChange={this.handleChange}
              />

              <PhoneFieldGroup
                field="phone"
                label="Телефон"
                value={phone}
                error={errors.phone}
                onChange={this.handleChange}
              />

              <TextFieldGroup
                field="age"
                label="Вік"
                value={age}
                error={errors.DayOfbirthday}
                type="number"
                onChange={this.handleChange}
              />
              <button className="btn btn-primary">Увійти</button>
              <hr />
              <button className="btn btn-primary">
                <NavLink tag={Link} className="text-white" to="/register">
                  Створити обліковий запис
                </NavLink>
              </button>
            </form>
          </div>
        </div>
        {/* <div className="container">
          <h1 className="d-flex justify-content-center">Реєстрація</h1>

          <form name="form" onSubmit={this.handleSubmit}>
            <TextFieldGroup
              field="lastName"
              label="Прізвище"
              value={lastName}
              error={errors.lastName}
              onChange={this.handleChange}
            />

            <TextFieldGroup
              field="firstName"
              label="Ім'я"
              value={firstName}
              error={errors.firstName}
              onChange={this.handleChange}
            />
            <TextFieldGroup
              field="middleName"
              label="По батькові"
              value={middleName}
              error={errors.middleName}
              onChange={this.handleChange}
            />
            <TextFieldGroup
              field="email"
              label="Електронна пошта"
              value={email}
              error={errors.email}
              onChange={this.handleChange}
            />

            <PhoneFieldGroup
              field="phone"
              label="Телефон"
              value={phone}
              error={errors.phone}
              onChange={this.handleChange}
            />

            <TextFieldGroup
              field="age"
              label="Вік"
              value={age}
              error={errors.DayOfbirthday}
              type="number"
              onChange={this.handleChange}
            />
            <div className="form-group">
              <button className="btn btn-primary">Зареєструватися</button>
            </div>
          </form>
        </div>
        {loading && <EclipseWidget />} */}
      </>
    );
  }
}

export default RegisterPage;
