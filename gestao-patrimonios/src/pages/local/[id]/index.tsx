import Button from "@/components/button/button";
import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";
import { erro } from "@/components/utils/toast";
import { listarLocalId } from "@/pages/api/localizacaoService";
import Link from "next/link";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

type LocalProps = {
  nomeLocal: string;
  nifLocal: string;
};

const Local = () => {
  const [local, setLocal] = useState<LocalProps>({
    nomeLocal: "Carregando...",
    nifLocal: "Carregando...",
  });

  const params = useParams();
  const id = params?.id;

  async function listarLocal() {
    try {
      const response = await listarLocalId(Number(id));
      setLocal(response);
    } catch (error: any) {
      erro(error.message);
      setLocal({
        nomeLocal: "Erro ao carregar",
        nifLocal: "N/A",
      });
    }
  }

  useEffect(() => {
    if (!id) return;
    listarLocal();
  }, [id]);

  return (
    <>
      <Header />
      <section className="min_height">
        <div className="container column gap">
          <Link href="/home" className="btn start">
            ← Voltar
          </Link>
          <div className="row full_width">
            <h1>{local ? local.nomeLocal : "Erro"}</h1>
            <p>{local.nifLocal}</p>
            <div className="side">
              <input
                type="text"
                placeholder="Pesquise o patrimônio..."
                className="input"
              />
              <Button className="full_width">+ Patrimônio</Button>
              <Button className="btn2">=</Button>
            </div>
          </div>
          <Lista pages="local" id={Number(id)} />
        </div>
      </section>
    </>
  );
};

export default Local;
