import { api } from "./api";

export async function listarHistorico(id: any) {
  try {
    const response = await api.get("LogPatrimonio/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}