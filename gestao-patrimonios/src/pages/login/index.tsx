const Login = () => {
  return (
    <>
      <main className="login-page">
        <section className="login-banner" aria-label="Apresentação do sistema">
          <img
            src="../imgs/Imagem do login.png"
            alt="Imagem de fundo relacionada à tecnologia"
            className="banner-image"
          />
          <div className="banner-overlay" />
          <div className="banner-content">
            <img
              src="../imgs/Logo Senai.png"
              alt="Logo do SENAI"
              className="senai-logo"
            />
            <h2>Gestão de patrimônios</h2>
            <p className="banner-content-text">
              Controle, organização e transparência do patrimônio com
              eficiência.
            </p>
            <p />
          </div>
        </section>
        <section className="login-area" aria-label="Formulário de login">
          <form className="login-form">
            <h1>Login</h1>
            <div className="form-group">
              <label htmlFor="nif">NIF:</label>
              <input
                type="text"
                id="nif"
                name="nif"
                placeholder="Insira o seu NIF"
                required={true}
              />
            </div>
            <div className="form-group">
              <label htmlFor="senha">Senha:</label>
              <div className="password-field">
                <input
                  type="password"
                  id="senha"
                  name="senha"
                  placeholder="Insira a sua senha"
                  required={true}
                />
                <button
                  type="button"
                  className="show-password"
                  aria-label="Mostrar senha"
                >
                  👁
                </button>
              </div>
            </div>
            <button type="submit" className="login-button">
              Entrar
            </button>
          </form>
        </section>
      </main>
    </>
  );
};

export default Login;