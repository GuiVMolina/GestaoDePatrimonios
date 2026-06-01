import secureLocalStorage from "react-secure-storage";
import axios from "axios";
import { jwtDecode } from "jwt-decode";

const apiLocal = "https://localhost:7063/api/";

const apiViaCep = "https://viacep.com.br/ws/";

export const api = axios.create({
  baseURL: apiLocal,
});

export const apiCep = axios.create({
  baseURL: apiViaCep,
});

api.interceptors.request.use((config) => {
  const token = secureLocalStorage.getItem("Token");

  if (token && typeof token === "string") {
    config.headers.Authorization = "Bearer " + token;

    try {
      jwtDecode(token);
    } catch (error) {
      console.error("Token inválido ou malformatado", error);
    }
  }

  return config;
});
