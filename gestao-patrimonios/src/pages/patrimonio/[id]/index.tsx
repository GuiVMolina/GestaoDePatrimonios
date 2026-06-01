import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";
import Link from "next/link";
import { listarPatrimonioId } from "../../api/patrimonioService";
import { erro } from "@/components/utils/toast";
import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import { ArrowBigLeft } from "lucide-react";

type PatrimonioProps = {
  nif: string;
  data: string;
  tipo: string;
  status: boolean;
  localAtual: string;
  nomePatrimonio: string;
};

const Patrimonio = () => {
  const [patrimonio, setPatrimonio] = useState<PatrimonioProps | null>(null);
  const params = useParams();
  const id = params?.id;

  async function listarPatrimonio() {
    try {
      const response = await listarPatrimonioId(Number(id));
      setPatrimonio(response);
    } catch (error: any) {
      erro(error.message);
    }
  }

  useEffect(() => {
    if (!id) return;
    listarPatrimonio();
  }, [id]);

  return (
    <>
      <Header />
      <section className="min_height">
        <div className="container column gap">
          <Link href="/home" className="btn start small_gap">
            <ArrowBigLeft /> Voltar
          </Link>
          <div className="full_width column small_gap">
            <h1>Patrimônio: {patrimonio?.nif}</h1>
            <ul className="caixa bgc row">
              <li className="column">
                <p>Denominação</p>
                <h3 className="title">
                  {patrimonio?.nomePatrimonio
                    ? patrimonio?.nomePatrimonio
                    : "N/A"}
                </h3>
              </li>
              <li className="column">
                <p>Tipo</p>
                <h3 className="title">
                  {patrimonio?.tipo ? patrimonio?.tipo : "N/A"}
                </h3>
              </li>
              <li className="column">
                <p>Data Transferência</p>
                <h3 className="title">
                  {patrimonio?.data ? patrimonio?.data : "N/A"}
                </h3>
              </li>
              <li className="column">
                <p>Local Atual</p>
                <h3 className="title">
                  {patrimonio?.localAtual ? patrimonio?.localAtual : "N/A"}
                </h3>
              </li>
              <li className="column">
                <p>Status Atual</p>
                <h3 className="title">
                  {patrimonio?.status ? "Ativo" : "Inativo"}
                </h3>
              </li>
            </ul>
          </div>
          <div className="full_width column small_gap">
            <h2>Histórico</h2>
            <Lista pages="patrimonio" id={Number(id)} />
          </div>
        </div>
      </section>
    </>
  );
};

export default Patrimonio;
