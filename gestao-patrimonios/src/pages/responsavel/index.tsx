import Button from "@/components/button/button";
import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";

const Responsavel = () => {
  return (
    <>
      <main>
        <Header />
        <section className="min_height">
          <div className="container column gap">
            <h1>Responsável</h1>
            <article className="card">
              <h2>Cadastrar novo responsável:</h2>
              <form action="" className="form">
                <div className="row">
                  <div className="campo_form">
                    <label htmlFor="" className="label">Nome:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">NIF:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">RG:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">CPF:</label>
                    <input type="text" className="input" />
                  </div>
                </div>
                <div className="row">
                  <div className="campo_form">
                    <label htmlFor="" className="label">N° Carteira de trabalho:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">Email:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">CEP:</label>
                    <input type="text" className="input" />
                  </div>
                </div>
                <div className="row">
                  <div className="campo_form">
                    <label htmlFor="" className="label">Logradouro:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">Complemento:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">Bairro:</label>
                    <input type="text" className="input" />
                  </div>
                </div>
                <div className="row end">
                  <div className="campo_form">
                    <label htmlFor="" className="label">Cidade:</label>
                    <input type="text" className="input" />
                  </div>
                  <div className="campo_form">
                    <label htmlFor="" className="label">Estado:</label>
                    <input type="text" className="input" />
                  </div>
                  <Button className="btn">Salvar</Button>
                </div>
              </form>
            </article>
            <Lista />
          </div>
        </section>
      </main>
    </>
  );
};

export default Responsavel;
