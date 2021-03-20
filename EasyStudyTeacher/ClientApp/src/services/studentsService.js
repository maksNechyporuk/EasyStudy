import axios from "axios";

export const getStudentsWithoutGroupAsync = async () => {
  return await axios.get(`/api/Student/StudentsWithoutGroup`);
};

export const getStudentsByGroupIdAsync = async (id) => {
  return await axios.get(`/api/Student/searchByGroup`, { params: { id: id } });
};

export const GetStudentById = async (id) => {
  return await axios.get(`/api/Student/searchById`, {
    params: { id: id },
  });
};

export const DeleteStudentsAsync = async (model) => {
  return await axios.post(`/api/Student/DeleteStudents`, model);
};

export const CreateStudent = async (model) => {
  return await axios.post(`/api/Student/CreateStudent`, {
    ...model,
    SchoolId: localStorage.getItem("schoolId"),
  });
};

export const UpdateStudent = async (model) => {
  return await axios.post(`/api/Student/UpdateStudent`, model);
};

export const getStudentsAsync = async () => {
  return await axios.get(`/api/Student/students`);
};

export const getStudentsBySchoolAsync = async () => {
  return await axios.get(`/api/Student/getStudentsBySchool`, {
    params: { id: localStorage.getItem("schoolId") },
  });
};
