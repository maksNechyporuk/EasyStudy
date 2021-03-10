import axios from "axios";

export const getStudentsWithoutGroupAsync = async () => {
  return await axios.get(`/api/Student/StudentsWithoutGroup`);
};

export const getStudentsByGroupIdAsync = async (id) => {
  return await axios.get(`/api/Student/searchByGroup`, { params: { id: id } });
};
