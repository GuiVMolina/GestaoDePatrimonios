import { listarPorNIF } from "@/pages/api/usuarioService";
import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import { erro } from "../utils/toast";

interface UserProps {
  nome: string;
  email: string;
  img?: string;
}

const User = ({ nome, email, img }: UserProps) => {
  const [user, setUser] = useState<UserProps | null>(null);
  const params = useParams();
  const id = params?.id;

  async function listarUser() {
    try {
      const response = await listarPorNIF(id);
      setUser(response);
    } catch (error: any) {
      erro(error.message);
    }
  }

  useEffect(() => {
    if (id) {
      listarUser();
    }
  }, [id]);

  return (
    <div className="side">
      {img ? (
        <img src={img} alt={nome} className="user" />
      ) : (
        <div className="side">
          <img
            src="/imgs/default_avatar.png"
            alt="Avatar padrão"
            className="user"
          />
          <div className="column">
            <p>{nome}</p>
            <p>{email}</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default User;