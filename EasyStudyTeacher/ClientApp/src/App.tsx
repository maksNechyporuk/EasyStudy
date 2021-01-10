import * as React from "react";
import { Route } from "react-router";
import Layout from "./components/Layout";
import Home from "./components/Home";
import Counter from "./components/Counter";
import FetchData from "./components/FetchData";
import RegisterPage from "./components/auth/Register";
import NormalLoginForm from "./components/auth/Login/scenes/LoginScreen.js";

import "./custom.css";

export default () => (
  <Layout>
    <Route exact path="/" component={NormalLoginForm} />
    <Route path="/register" component={RegisterPage} />
    <Route path="/login" component={NormalLoginForm} />
  </Layout>
);
