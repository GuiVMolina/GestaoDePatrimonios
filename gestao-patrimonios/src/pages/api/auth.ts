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
  img?: string; // Mantenha aqui se pretender usar futuramente
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
      // 1. Decodificamos como 'any' ou um objeto genérico primeiro, 
      // pois as chaves reais que vêm no JSON são strings de URLs.
      const decoded = jwtDecode<any>(token);
      
      // 2. Fazemos o mapeamento manual das claims da API para a sua interface limpa
      const usuario: UsuarioToken = {
        id: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/nameidentifier"],
        nome: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
        nif: decoded["NIF"] || decoded["nif"], // Garante o mapeamento idependente do case da API
        role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
        img: decoded["img"] || undefined // Caso a API passe a enviar futuramente
      };

      return usuario;
    }
  } catch (error) {
    console.error("Erro ao decodificar o token de sessão:", error);
  }
  return null;
}