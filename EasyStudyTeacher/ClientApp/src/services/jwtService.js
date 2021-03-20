import jwt from "jsonwebtoken";
import axios from "axios";

export const loginByJWT = (tokens) => {
  const { token, refreshToken, schoolId } = tokens;
  var user = jwt.decode(token);
  console.log("tokens=>", tokens);
  if (!Array.isArray(user.roles)) {
    user.roles = Array.of(user.roles);
  }
  localStorage.setItem("authToken", token);
  localStorage.setItem("refreshToken", refreshToken);
  if (schoolId) {
    localStorage.setItem("schoolId", schoolId);
  }
  setAuthorisationToken(token);

  //dispatch(loginActions.setCurrentUser(user));
};
export const setAuthorisationToken = (token) => {
  if (token) {
    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  } else {
    delete axios.defaults.headers.common["Authorization"];
  }
};

export const logout = () => {
  localStorage.removeItem("authToken");
  localStorage.removeItem("refreshToken");
  localStorage.removeItem("schoolId");
};
