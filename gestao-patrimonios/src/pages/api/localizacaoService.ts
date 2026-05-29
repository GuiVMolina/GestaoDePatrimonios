import { api } from "./api";

export async function listarLocalizacao() {
  try {
    const response = await api.get("Localizacao");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}

export async function listarLocalId(id: any) {
  try {
    const response = await api.get("Localizacao/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}

export async function listarAreaELocalizacao() {
  try {
    const [areas, localizacoes] = await Promise.all([
      api.get("Area"),
      api.get("Localizacao"),
    ]);

    return {
      areas: areas.data,
      localizacoes: localizacoes.data,
    };
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}
