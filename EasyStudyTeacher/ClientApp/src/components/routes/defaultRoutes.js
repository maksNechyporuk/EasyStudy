import React from "react";

const Register = React.lazy(() => import("../components/auth/Register"));
const Login = React.lazy(() => import("../components/auth/Login"));

const defaultRoutes = [
  { path: "/register", exact: true, name: "Реєстрація", component: Register },
  { path: "/login", exact: true, name: "Логін", component: Login },
];
export default defaultRoutes;
