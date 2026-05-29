import Button from "@/components/button/button";
import styles from "./senha.module.css";
import { erro, notificacao } from "@/components/utils/toast";
import { useRouter } from "next/router";
import { login } from "@/pages/api/auth";
import { useState } from "react";

const Login = () => {
  const [nif, setNIF] = useState<string>("");
  const [senha, setSenha] = useState<string>("");

  const router = useRouter();

  async function autenticar(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    try {
      await login(nif, senha);
      notificacao("Login bem sucedido!");
      router.push("/home");
    } catch (error: any) {
      erro(error.response.data);
    }
  }

  return (
    <main id={styles.login_page}>
      <section id={styles.login_banner} aria-label="Apresentação do sistema">
        <img
          src="../imgs/imagem_login.png"
          alt="Imagem de fundo relacionada à tecnologia"
          className="image"
        />
        <div id={styles.login_banner_info}>
          <img
            src="../imgs/logo_senai.png"
            alt="Logo do SENAI"
            className="senai-logo"
          />
          <h2>Gestão de patrimônios</h2>
          <p className="banner-content-text">
            Controle, organização e transparência do patrimônio com eficiência.
          </p>
          <p />
        </div>
      </section>
      <section
        id={styles.login_form}
        className="column full_width "
        aria-label="Formulário de login"
      >
        <form className="form" onSubmit={autenticar}>
          <h1>Redefinir Senha</h1>
          <div className="campo_form">
            <label htmlFor="senha">Nova senha:</label>
            <div className="row">
              <input
                type="password"
                id="senha"
                name="senha"
                placeholder="Insira a sua nova senha"
                className="input"
                value={senha}
                onChange={(e) => setSenha(e.target.value)}
                required={true}
              />
              <Button type="button" className="icon" aria-label="Mostrar senha">
                👁
              </Button>
            </div>
          </div>
          <div className="campo_form">
            <label htmlFor="senha">Confirmar senha:</label>
            <div className="row">
              <input
                type="password"
                id="senha"
                name="senha"
                placeholder="Confirmar a sua senha"
                className="input"
                value={senha}
                onChange={(e) => setSenha(e.target.value)}
                required={true}
              />
              <Button type="button" className="icon" aria-label="Mostrar senha">
                👁
              </Button>
            </div>
          </div>
          <Button type="submit" className="full_width">
            Salvar
          </Button>
        </form>
      </section>
    </main>
  );
};

export default Login;
