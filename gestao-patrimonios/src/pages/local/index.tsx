const Local = () => {
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
          className="page-header layout_guide"
          aria-labelledby="titulo-ambientes"
        >
          <h1 id="titulo-ambientes">Ambientes</h1>
          <form className="search-area" role="search">
            <label htmlFor="pesquisa-ambiente" className="sr-only">
              Pesquisar ambiente
            </label>
            <input
              type="search"
              id="pesquisa-ambiente"
              name="pesquisaAmbiente"
              placeholder="Pesquise o ambiente"
            />
            <button
              type="button"
              className="filter-button"
              aria-label="Filtrar ambientes"
            >
              <i className="fa-solid fa-sliders" />
            </button>
          </form>
        </section>
        <section
          className="table-section layout_guide"
          aria-label="Lista de ambientes"
        >
          <table className="environment-table">
            <thead>
              <tr>
                <th>Local</th>
                <th>Responsável</th>
                <th>Detalhes</th>
              </tr>
            </thead>
            <tbody>
              <tr className="">
                <td>Sala 30/31 (anfiteatro)</td>
                <td>Samanta Melissa</td>
                <td>
                  <a href="#" aria-label="Ver detalhes da Sala 30/31">
                    <i className="fa-solid fa-circle-info" />
                  </a>
                </td>
              </tr>
            </tbody>
          </table>
        </section>
        <nav className="pagination" aria-label="Paginação">
          <button
            type="button"
            className="pagination-button"
            aria-label="Página anterior"
          >
            ‹
          </button>
          <a href="#" className="pagination-link current" aria-current="page">
            1
          </a>
          <a href="#" className="pagination-link">
            2
          </a>
          <a href="#" className="pagination-link">
            3
          </a>
          <button
            type="button"
            className="pagination-button"
            aria-label="Próxima página"
          >
            ›
          </button>
        </nav>
      </main>
    </>
  );
};

export default Local;