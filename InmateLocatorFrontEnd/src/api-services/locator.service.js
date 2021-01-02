import Axios from "axios";
import { authHeader } from "../auth-helper/auth-header";

const RESOURCE_NAME = "/Inmates";

export default {
  getAll() {
    return Axios.get(RESOURCE_NAME);
  },
  get(id) {
    return Axios.get(`${RESOURCE_NAME}/${id}`);
  },
  getAuth(id) {
    debugger;
    return Axios.get(`${RESOURCE_NAME}/${id}`, {
      headers: authHeader()
    });
  },
  create(data) {
    return Axios.post(RESOURCE_NAME, data);
  },
  update(id, data) {
    return Axios.put(`${RESOURCE_NAME}/${id}`, data);
  },
  delete(id) {
    return Axios.delete(`${RESOURCE_NAME}/${id}`);
  }
};
