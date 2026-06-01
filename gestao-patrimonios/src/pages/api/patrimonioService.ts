import { api } from "./api";

export async function listarPatrimonios() {
  try {
    const response = await api.get("Patrimonio");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}

export async function listarPatrimonioId(id: any) {
  try {
    const response = await api.get("Patrimonio/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}

export async function listarPatrimoniosPorLocal(id: string) {
  try {
    const response = await api.get("Patrimonio");
    const todosOsPatrimonios = response.data;

    if (Array.isArray(todosOsPatrimonios)) {
      console.log("Dados recebidos da API:", todosOsPatrimonios[0]);

      return todosOsPatrimonios.filter(
        (patrimonio: any) =>
          patrimonio.localizacaoID === id || patrimonio.LocalizacaoID === id,
      );
    }

    return [];
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}

export async function importarPatrimonioCsv(arquivo: File) {
  try {
    const formData = new FormData();
    formData.append("arquivoCsv:", arquivo);

    await api.post("Patrimonio/importar-csv", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });

    await api.post("Patrimonio/importar-csv");
  } catch (error: any) {
    throw new Error(error.response.data);
  }
}
