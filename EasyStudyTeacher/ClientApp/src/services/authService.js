import axios from "axios";

export const register = async (model) => {
  return await axios.post(`/api/Teacher/Register`, model);
};

export const login = async (model) => {
  return await axios.post(`/api/Teacher/Login`, model);
  // .then((res) => {
  //   console.log(res);
  //   console.log(res.data);
  // })
  // .catch((err) => {
  //   console.log(err);
  // });
};
