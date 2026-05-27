import secureLocalStorage from "react-secure-storage";
import { erro } from "@/components/utils/toast";
import { jwtDecode } from "jwt-decode";
import { api } from "./api";

export interface UsuarioToken {
  id: string;
  nome: string;
  email: string;
  nif: string;
  role?: string;
  img?: string;
}

export async function login(nif: string, senha: string) {
  try {
    const response = await api.post("autenticacao/login", { nif, senha });
    const token = response.data.token;
    secureLocalStorage.setItem("Token", token);
  } catch (error: any) {
    throw erro("NIF ou senha inválidos");
  }
}

export async function logout() {
  try {
    secureLocalStorage.removeItem("Token");
  } catch (error: any) {
    throw erro("Erro ao sair da conta");
  }
}

export function obterUsuarioAutenticado(): UsuarioToken | null {
  try {
    const token = secureLocalStorage.getItem("Token");
    if (token && typeof token === "string") {
      const decoded = jwtDecode<UsuarioToken>(token);
      console.log(decoded)
      return decoded;
    }
  } catch (error) {
    console.error("Erro ao decodificar o token de sessão:", error);
  }
  return null;
}
