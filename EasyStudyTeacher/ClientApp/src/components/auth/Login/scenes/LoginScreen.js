import React from "react";
import "../../auth.scss";
import TextFieldGroup from "../../../common/TextFieldGroup";

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
const NormalLoginForm = () => {
  const onFinish = (values) => {
    console.log("Received values of form: ", values);
  };

  return (
    <div className="main-auth">
      <div class="form">
        <div class="thumbnail">
          <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/169963/hat.svg" />
        </div>
        <form class="login-form">
          <TextFieldGroup type="text" placeholder="Логін" isShowLabel={false} />
          <TextFieldGroup
            type="password"
            placeholder="Пароль"
            isShowLabel={false}
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
  );
};
export default NormalLoginForm;
