import { listarAreaELocalizacao } from "@/pages/api/localizacaoService";
import {
  listarHistoricoPatrimonio,
  listarPatrimoniosPorLocal,
} from "@/pages/api/patrimonioService";
import { useEffect, useState } from "react";
import { erro } from "../utils/toast";
import Link from "next/link";
import Button from "../button/button";

type ListaPage = {
  pages?: string;
  id?: number;
};

type ListaProps = {
  localizacaoID?: number;
  nomeLocal?: string;
  nomeArea?: string;
  nomePatrimonio?: string;
  responsavel?: string;
  nif?: string;
  data?: string;
  tipo?: string;
  origem?: string;
  destino?: string;
  justificativa?: string;
};

const Lista = ({ pages, id }: ListaPage) => {
  const [items, setItems] = useState<ListaProps[]>([]);
  const [loading, setLoading] = useState(true);

  async function carregarDados() {
    setLoading(true);
    try {
      if (pages === "home") {
        const dados = await listarAreaELocalizacao();

        const listaMesclada: ListaProps[] = dados.localizacoes.map(
          (loc: any) => {
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
          },
        );

        setItems(listaMesclada);
      } else if (pages === "patrimonio" && id) {
        const dados = await listarHistoricoPatrimonio(id);
        setItems(Array.isArray(dados) ? dados : []);
      } else if (pages === "local" && id) {
        const dados = await listarPatrimoniosPorLocal(id);
        setItems(Array.isArray(dados) ? dados : []);
      }
    } catch (error: any) {
      erro("Erro ao carregar dados: " + error.message);
      setItems([]);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    carregarDados();
  }, [pages, id]);

  return (
    <div className="column full_width small_gap">
      {loading && <p>Carregando...</p>}

      {pages === "home" && !loading && (
        <>
          <ul className="caixa">
            <li>Local</li>
            <li>Área</li>
            <li>Responsável</li>
          </ul>
          <hr className="line" />
          {items.map((item) => (
            <Link
              href={`/local/${item.localizacaoID}`}
              key={item.localizacaoID}
              className="caixa bgc"
            >
              <li>{item.nomeLocal}</li>
              <li>{item.nomeArea}</li>
              {item.responsavel ? (
                <li>{item.responsavel}</li>
              ) : (
                <li>Sem responsável</li>
              )}
            </Link>
          ))}
        </>
      )}

      {pages === "patrimonio" && !loading && (
        <>
          <ul className="caixa">
            <li>Data</li>
            <li>Tipo de movimentação</li>
            <li>Origem</li>
            <li>Destino</li>
            <li>Responsável</li>
            <li>Justificativa</li>
          </ul>
          <hr className="line" />
          {items.length > 0 ? (
            items.map((item, idx) => (
              <ul className="caixa bgc" key={idx}>
                <li>{item.data}</li>
                <li className="color">{item.tipo}</li>
                <li>{item.origem}</li>
                <li>{item.destino}</li>
                <li>{item.responsavel}</li>
                <li className="column">
                  <Button className="link">Info</Button>
                </li>
              </ul>
            ))
          ) : (
            <p>Nenhum histórico encontrado</p>
          )}
        </>
      )}

      {pages === "local" && !loading && (
        <>
          <ul className="caixa">
            <li>Patrimônio</li>
            <li>Denominação</li>
            <li>Data Transferência</li>
            <li>Ações</li>
          </ul>
          <hr className="line" />
          {items.length > 0 ? (
            items.map((item, idx) => (
              <ul className="caixa bgc" key={idx}>
                <li>{item.nif}</li>
                <li>{item.nomePatrimonio}</li>
                <li>{item.data}</li>
                <li className="side">
                  <Link href="">Editar</Link>
                  <Link href="">Info</Link>
                </li>
              </ul>
            ))
          ) : (
            <p>Nenhum patrimônio encontrado</p>
          )}
        </>
      )}
    </div>
  );
};

export default Lista;
