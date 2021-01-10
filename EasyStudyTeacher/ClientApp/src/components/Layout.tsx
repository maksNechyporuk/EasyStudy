import * as React from "react";
import { Container } from "reactstrap";
import Header from "./Header";
import "./Header.css";

export default (props: { children?: React.ReactNode }) => (
  <div className="main">
    <Header />
    <React.Fragment>{props.children}</React.Fragment>
  </div>
);
