const Detalhe = () => {
  return (
    <>
      <header className="topbar">
        <nav className="navbar layout_guide" aria-label="Menu principal">
          <a href="#" className="logo-link" aria-label="Página inicial">
            <img
              src="../imgs/Logo Senai.png"
              alt="Logo SENAI"
              className="logo"
            />
          </a>
          <ul className="menu-list">
            <li>
              <a href="#" className="menu-link">
                Ambientes
                <i className="fa-solid fa-chevron-down" />
              </a>
            </li>
            <li>
              <a href="#" className="menu-link">
                Patrimônios
              </a>
            </li>
          </ul>
          <section className="user-area" aria-label="Informações do usuário">
            <button className="user-icon" aria-label="Abrir perfil do usuário">
              <i className="fa-solid fa-user" />
            </button>
            <div className="user-info">
              <strong>Késsia Milena</strong>
              <span>kessia@sp.senai.br</span>
            </div>
            <button className="arrow-button" aria-label="Abrir opções da conta">
              <i className="fa-solid fa-chevron-down" />
            </button>
          </section>
          <button className="hamburguer" aria-label="Abrir opções de menu ">
            <i className="fa-solid fa-bars" />
          </button>
        </nav>
      </header>
      <main className="page-content">
        <section
          className="page-detalhes layout_guide"
          aria-labelledby="titulo-patrimonio"
        >
          <a href="#" className="back-link">
            <i className="fa-solid fa-arrow-left" />
            Voltar
          </a>
          <h1 id="titulo-patrimonio">Patrimônio: 1236808</h1>
          <article className="patrimonio-card">
            <div className="patrimonio-content">
              <dl>
                <dt>Denominação</dt>
                <dd>NOTEBOOK ALTO DESEMPENHO P/ GAMER</dd>
              </dl>
              <dl>
                <dt>Tipo</dt>
                <dd>Mesa</dd>
              </dl>
              <dl>
                <dt>Data transferência</dt>
                <dd>
                  <time dateTime="2026-02-09">09/02/2026</time>
                </dd>
              </dl>
              <dl>
                <dt>Local Atual</dt>
                <dd>Sala 09/10</dd>
              </dl>
              <dl>
                <dt>Status Atual</dt>
                <dd>Ativo</dd>
              </dl>
            </div>
          </article>
        </section>
        <section
          className="table-section layout_guide"
          aria-label="Lista de histórico do patrimônio"
        >
          <h2>Histórico</h2>
          <table className="history-table">
            <thead>
              <tr>
                <th>Data</th>
                <th>Tipo de movimentação</th>
                <th>Origem</th>
                <th>Destino</th>
                <th>Responsável</th>
                <th>Justificativa</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td data-label="Data">11/02/2026</td>
                <td data-label="Tipo de movimentação">
                  <span className="status-badge">Transferência</span>
                </td>
                <td data-label="Origem">Sala 07/08</td>
                <td data-label="Destino">Sala 09/10</td>
                <td data-label="Responsável">Gustavo Lima</td>
                <td data-label="Justificativa">
                  <a href="#" aria-label="Ver justificativa da transferência">
                    <i className="fa-solid fa-circle-info" />
                  </a>
                </td>
              </tr>
            </tbody>
          </table>
        </section>
      </main>
    </>
  );
};

export default Detalhe;