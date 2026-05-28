import { useEffect, useState } from "react";
import { obterUsuarioAutenticado, UsuarioToken } from "@/pages/api/auth";
import User from "../user[id]/user";
import Link from "next/link";

const Header = () => {
  const [usuario, setUsuario] = useState<UsuarioToken | null>(null);
  const [estaAutenticado, setEstaAutenticado] = useState(false);

  useEffect(() => {
    const dados = obterUsuarioAutenticado();

    if (dados) {
      setUsuario(dados);
      setEstaAutenticado(true);
    } else {
      setEstaAutenticado(false);
    }
  }, []);

  return (
    <header className="header">
      <div className="container">
        {/* <Link href="/home"> */}
          <img src="./imgs/logo_senai.png" alt="Logo SENAI" className="logo" />
        {/* </Link> */}
        <select name="Ambientes" id="ambientes" className="select">
          <option value="Ambientes" disabled selected>
            Ambientes
          </option>
          <option value="Ambientes"><Link href="/area" className="redirect">Área</Link></option>
          <option value="Ambientes"><Link href="/home" className="redirect">Local</Link></option>
        </select>
        <h3>Patrimônios</h3>

        {estaAutenticado && usuario ? (
          <User nome={usuario.nome} email={usuario.email} img={usuario.img} />
        ) : (
          <Link href="/login" className="link">
            Logar
          </Link>
        )}
      </div>
    </header>
  );
};

export default Header;
