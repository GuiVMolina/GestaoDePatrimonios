const Modal = () => {
  return (
    <>
      {/* MODAL IMPORTAR */}
      <section className="modal-overlay">
        <article className="modal-container" id="modalImportar">
          <a href="#" className="modal-close">
            x
          </a>
          <h1 className="modal-title">Importar novos patrimônios</h1>
          <form className="modal-form">
            <div className="modal-field">
              <label htmlFor="numeroPatrimonio">Número do patrimônio</label>
              <input type="text" id="numeroPatrimonio" placeholder="1245769" />
            </div>
            <div className="modal-field">
              <label htmlFor="denominacaoPatrimonio">Denominação</label>
              <input
                type="text"
                id="denominacaoPatrimonio"
                placeholder="CADEIRA GIRATÓRIA EM POLIPROPILENO PRETO"
              />
            </div>
            <button className="modal-button">SALVAR IMPORTAÇÃO</button>
          </form>
        </article>
      </section>
      {/* MODAL TRANSFERIR */}
      <section className="modal-overlay">
        <article className="modal-container" id="modalTransferir">
          <a href="#" className="modal-close">
            x
          </a>
          <h1 className="modal-title">Transferir os patrimônios</h1>
          <form className="modal-form">
            <div className="modal-field">
              <label htmlFor="ambienteTransferencia">Ambiente</label>
              <select id="ambienteTransferencia">
                <option>Manutenção</option>
                <option>Sala XX</option>
                <option>Sala XX</option>
              </select>
            </div>
            <div className="modal-field">
              <label htmlFor="motivoTransferencia">
                Motivo da transferência
              </label>
              <textarea id="motivoTransferencia" placeholder="Lorem" />
            </div>
            <button className="modal-button">TRANSFERIR</button>
          </form>
        </article>
      </section>
      {/* MODAL JUSTIFICATIVA */}
      <section className="modal-overlay">
        <article className="modal-container modal-justificativa">
          <a href="#" className="modal-close">
            x
          </a>
          <h1 className="modal-title">Justificativa</h1>
          <p className="modal-text">
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Veritatis,
            quasi distinctio! Temporibus similique expedita laboriosam,
            assumenda officia veritatis amet doloremque esse obcaecati
            repudiandae architecto in sed facilis quas harum.
          </p>
        </article>
      </section>
    </>
  );
};

export default Modal;