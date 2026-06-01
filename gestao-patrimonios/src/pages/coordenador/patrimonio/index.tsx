import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";
import { importarPatrimonioCsv } from "@/pages/api/patrimonioService";
import { erro, notificacao } from "@/components/utils/toast";
import { ArrowBigLeft, Upload } from "lucide-react";
import { useRef, useState } from "react";
import Link from "next/link";
// import { importarPatrimoniosCsv } from "../api/patrimonioService";

const Patrimonio = () => {
  const [arquivo, setArquivo] = useState<File | null>(null);
  const inputRef = useRef<HTMLInputElement>(null)

  async function cadastrarArquivo(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    if (!arquivo) {
      alert("Insira um arquivo");
      return;
    }
    try {
      await importarPatrimonioCsv(arquivo);
      notificacao("Importação feita com sucesso!");
      setArquivo(null);
      if(inputRef.current){
        inputRef.current.value = ""
      }
    } catch (error: any) {
      erro(error.message);
    }
  }

  return (
    <>
      <Header />
      <main className="min_height">
        <section>
          <div className="container column gap">
            <Link href="/home" className="btn start small_gap">
              <ArrowBigLeft /> Voltar
            </Link>
            <h1>Patrimônios</h1>
            <form className="form card" onSubmit={cadastrarArquivo}>
              <h2 className="row">
                Importar patrimônios <Upload />
              </h2>
              <div className="campo_form">
                <label htmlFor="area">Upload de arquivo csv:</label>
                <input
                  className="file"
                  type="file"
                  id="area"
                  accept=".csv"
                  onChange={(e) => setArquivo(e.target.files![0])}
                  ref={inputRef}
                />
              </div>
              <button className="btn">Salvar</button>
            </form>
            <Lista pages="local" />
          </div>
        </section>
      </main>
    </>
  );
};

export default Patrimonio;
