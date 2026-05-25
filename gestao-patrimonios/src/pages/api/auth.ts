import secureLocalStorage from "react-secure-storage";
import { erro } from "@/components/utils/toast";
import { api } from "./api";

export async function login(email: string, senha: string) {
  try {
    const response = await api.post("autenticacao/login", { email, senha });
    const token = response.data.token;

    secureLocalStorage.setItem("Token", token);
  } catch (error: any) {
    throw erro("Email ou senha inválidos");
  }
}

export async function logout() {
  try {
    secureLocalStorage.removeItem("Token");
  } catch (error: any) {
    throw erro("Erro ao sair da conta");
  }
}