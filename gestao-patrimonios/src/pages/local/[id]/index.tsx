import Button from "@/components/button/button";
import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";
import Link from "next/link";
import { verificarAutenticacao } from "@/components/utils/auth";
import { listarLocalId } from "@/pages/api/localizacaoService";
import { useParams, useRouter } from "next/navigation";
import { erro } from "@/components/utils/toast";
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

  const [estaAutenticado, setEstaAtutenticado] = useState(false);
  const router = useRouter();

  const params = useParams();
  const id = params?.id;

  async function listarLocal() {
    try {
      const response = await listarLocalId(id);
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
    if (!verificarAutenticacao()) {
      router.push("/login");
      return;
    }
    setEstaAtutenticado(true);
    listarLocal();
  }, [id]);

  if (!estaAutenticado) {
    return null;
  }

  return (
    <>
      <Header />
      <section className="min_height">
        <div className="container column gap">
          <Link href="/home" className="btn start">
            ← Voltar
          </Link>
          <div className="row full_width">
            <h1>{local.nomeLocal}</h1>
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
          <Lista pages="local" id={id} />
        </div>
      </section>
    </>
  );
};

export default Local;
