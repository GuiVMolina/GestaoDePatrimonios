import { api } from "./api";

export async function listarPatrimonioId(id: number) {
  try {
    const response = await api.get("Patrimonio/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}

export async function listarHistoricoPatrimonio(patrimoniodID: number) {
  try {
    const response = await api.get(`Patrimonio/${patrimoniodID}/historico`);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || error.message);
  }
}

export async function listarPatrimoniosPorLocal(localizacaoID: number) {
  try {
    const response = await api.get(`Localizacao/${localizacaoID}/patrimonios`);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || error.message);
  }
}
