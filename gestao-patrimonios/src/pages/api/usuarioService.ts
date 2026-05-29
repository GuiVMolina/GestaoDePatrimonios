import { api } from "./api";

export async function listarPorNIF(id: any) {
  try {
    const response = await api.get("Usuario/" + id);

    const user = {
      ...response.data,
    };

    return user;
  } catch (error: any) {
    throw new Error(error.responde.data);
  }
}
