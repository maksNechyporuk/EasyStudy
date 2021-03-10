import axios from "axios";

export const getGroupsAsync = async (model) => {
  return await axios.get(`/api/Group/getGroups`);
};

export const createGroup = async (model) => {
  return await axios.post(`/api/Group/Create`, model);
};

export const deleteGroup = async (id) => {
  return await axios.delete(`/api/Group/Delete/${id}`);
};

export const deleteSelectedGroups = async (model) => {
  return await axios.post(`/api/Group/DeleteSelected`, model);
};

export const updateGroupAsync = async (model) => {
  return await axios.put(`/api/Group/Edit`, model);
};
