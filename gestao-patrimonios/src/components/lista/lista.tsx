import { listarAreaELocalizacao } from "@/pages/api/localizacaoService";
import { useEffect, useState } from "react";
import { erro } from "../utils/toast";
import Link from "next/link";

type Lista = {
  localizacaoID: number;
  nomeLocal: string;
  nomeArea: string;
  responsavel: string;
}; 

const Lista = () => {
  const [local, setLocal] = useState<Lista[]>([]);

  async function carregarLocalCompleto() {
    try {
      const dados = await listarAreaELocalizacao();

      const listaMesclada: Lista[] = dados.localizacoes.map((loc: any) => {
        const areaCorrespondente = dados.areas.find(
          (area: any) => area.areaID === loc.areaID,
        );

        return {
          localizacaoID: loc.localizacaoID,
          nomeLocal: loc.nomeLocal,
          nomeArea: areaCorrespondente
            ? areaCorrespondente.nomeArea
            : "Sem área",
          responsavel: loc.responsavel,
        };
      });

      setLocal(listaMesclada);
    } catch (error: any) {
      erro("Erro ao carregar os locais: " + error.message);
      setLocal([]);
    }
  }

  useEffect(() => {
    carregarLocalCompleto();
  }, []);

  return (
    <div className="column full_width small_gap">
      <ul className="caixa">
        <li>Local</li>
        <li>Área</li>
        <li>Responsável</li>
      </ul>
      <hr className="line" />
      {local.map((item) => (
        <Link href={`/local?id=${item.localizacaoID}`} key={item.localizacaoID} className="caixa bgc">
          <li>{item.nomeLocal}</li>
          <li>{item.nomeArea}</li>
          {item.responsavel ? (
            <li>{item.responsavel}</li>
          ) : (
            <li>Sem responsável</li>
          )}
        </Link>
      ))}
    </div>
  );
};

export default Lista;
