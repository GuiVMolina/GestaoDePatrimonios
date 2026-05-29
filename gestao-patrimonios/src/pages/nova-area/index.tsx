import Button from "@/components/button/button";
import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";
import Link from "next/link";

const Area = () => {
  return (
    <>
      <main>
        <Header />
        <section className="min_height">
          <div className="container column gap">
            <Link href="/home" className="btn start">
              ← Voltar
            </Link>
            <h1>Área</h1>
            <article className="card full_width">
              <h2>Cadastrar nova área:</h2>
              <form action="" className="form">
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Nome da área:
                  </label>
                  <input type="text" className="input" />
                </div>
                <Button className="btn">Salvar</Button>
              </form>
            </article>
            <Lista />
          </div>
        </section>
      </main>
    </>
  );
};

export default Area;
