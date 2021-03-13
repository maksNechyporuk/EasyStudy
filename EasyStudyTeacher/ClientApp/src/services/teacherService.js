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
export const GetTeacherById = async (id) => {
  return await axios.get(`/api/Teacher/GetTeacherById`, {
    params: { id: id },
  });
};

export const DeleteTeacherAsync = async (model) => {
  return await axios.post(`/api/Teacher/DeleteTeachers`, model);
};

export const CreateTeacher = async (model) => {
  return await axios.post(`/api/Teacher/CreateTeacher`, model);
};

export const UpdateTeacher = async (model) => {
  return await axios.post(`/api/Teacher/UpdateTeacher`, model);
};
