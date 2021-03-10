import axios from "axios";

export const getTeacherWithoutGroupAsync = async () => {
  return await axios.get(`/api/Teacher/TeachersWithoutGroup`);
};

export const getTeachersAsync = async () => {
  return await axios.get(`/api/Teacher/teacher`);
};

export const getTeacherByGroup = async (id) => {
  return await axios.get(`/api/Teacher/GetTeacherByGroup`, {
    params: { id: id },
  });
};
