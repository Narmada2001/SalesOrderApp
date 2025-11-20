import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:5001/api", // change to your backend URL
});

export default api;

