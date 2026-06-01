import Button from "@/components/button/button";
import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";
import { ArrowBigLeft } from "lucide-react";
import Link from "next/link";
import { useState } from "react";
import { apiCep } from "../../api/api";

const Responsavel = () => {
  const [cep, setCep] = useState("");
  const [bairro, setBairro] = useState("");
  const [cidade, setCidade] = useState("");
  const [estado, setEstado] = useState("");
  const [logradouro, setLogradouro] = useState("");

  async function buscarCep() {
    try {
      const response = await apiCep.get(`${cep}/json`);
      const dados = await response.data;

      setBairro(dados.bairro);
      setCidade(dados.localidade);
      setEstado(dados.estado);
      setLogradouro(dados.logradouro);
    } catch (error: any) {}
  }

  return (
    <>
      <Header />
      <section className="min_height">
        <div className="container column gap">
          <Link href="/home" className="btn start small_gap">
            <ArrowBigLeft /> Voltar
          </Link>
          <h1>Responsável</h1>
          <article className="card full_width">
            <h2>Cadastrar novo responsável:</h2>
            <form action="" className="form">
              <div className="row to_column">
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Nome:
                  </label>
                  <input type="text" className="input" />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    NIF:
                  </label>
                  <input type="text" className="input" />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    RG:
                  </label>
                  <input type="text" className="input" />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    CPF:
                  </label>
                  <input type="text" className="input" />
                </div>
              </div>
              <div className="row to_column">
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    N° Carteira de trabalho:
                  </label>
                  <input type="text" className="input" />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Email:
                  </label>
                  <input type="text" className="input" />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    CEP:
                  </label>
                  <input
                    type="text"
                    className="input"
                    value={cep}
                    onChange={(e) => setCep(e.target.value)}
                    onBlur={buscarCep}
                  />
                </div>
              </div>
              <div className="row to_column">
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Logradouro:
                  </label>
                  <input
                    type="text"
                    className="input"
                    value={logradouro}
                    onChange={(e) => setLogradouro(e.target.value)}
                  />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Complemento:
                  </label>
                  <input type="text" className="input" />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Bairro:
                  </label>
                  <input
                    type="text"
                    className="input"
                    value={bairro}
                    onChange={(e) => setBairro(e.target.value)}
                  />
                </div>
              </div>
              <div className="row to_column end">
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Cidade:
                  </label>
                  <input
                    type="text"
                    className="input"
                    value={cidade}
                    onChange={(e) => setCidade(e.target.value)}
                  />
                </div>
                <div className="campo_form">
                  <label htmlFor="" className="label">
                    Estado:
                  </label>
                  <input
                    type="text"
                    className="input"
                    value={estado}
                    onChange={(e) => setEstado(e.target.value)}
                  />
                </div>
                <Button className="btn">Salvar</Button>
              </div>
            </form>
          </article>
          <Lista />
        </div>
      </section>
    </>
  );
};

export default Responsavel;
