import Button from "@/components/button/button";
import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";

type Dados = {
  nomeLocal: string;
};

const Local = ({ nomeLocal }: Dados) => {
  return (
    <>
      <main>
        <Header />
        <section className="min_height">
          <div className="container column gap">
            <div className="row full_width">
              <h1>Patrimônios: {nomeLocal}</h1>
              <div className="side">
                <input
                  type="text"
                  placeholder="Pesquise o ambiente..."
                  className="input"
                />
                <Button className="btn2">=</Button>
              </div>
            </div>
            <Lista />
          </div>
        </section>
      </main>
    </>
  );
};

export default Local;
