import Button from "../button/button";
import Link from "next/link";
import { listarPatrimoniosPorLocal } from "@/pages/api/patrimonioService";
import { listarAreaELocalizacao } from "@/pages/api/localizacaoService";
import { listarHistorico } from "@/pages/api/logService";
import { useEffect, useState } from "react";
import { erro } from "../utils/toast";

type ListaProps = {
  pages?: string;
  id?: any;
};

type ItemLista = {
  id?: string | number;
  patrimonioID?: string | number;
  localizacaoID?: string | number;
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

const Lista = ({ pages, id }: ListaProps) => {
  const [items, setItems] = useState<ItemLista[]>([]);
  const [paginaAtual, setPaginaAtual] = useState(1);
  const cardsPorPagina = 50;

  const totalPaginas = Math.ceil(items.length / cardsPorPagina);

  const indiceInicial = (paginaAtual - 1) * cardsPorPagina;
  const indiceFinal = indiceInicial + cardsPorPagina;

  const itensPaginados = items.slice(indiceInicial, indiceFinal);

  function proximaPagina() {
    if (paginaAtual < totalPaginas) {
      setPaginaAtual(paginaAtual + 1);
    }
  }

  function paginaAnterior() {
    if (paginaAtual > 1) {
      setPaginaAtual(paginaAtual - 1);
    }
  }

  const maxBotoesVisiveis = 6;

  let paginaInicial = Math.max(
    1,
    paginaAtual - Math.floor(maxBotoesVisiveis / 2),
  );
  let paginaFinal = paginaInicial + maxBotoesVisiveis - 1;

  if (paginaFinal > totalPaginas) {
    paginaFinal = totalPaginas;
    paginaInicial = Math.max(1, paginaFinal - maxBotoesVisiveis + 1);
  }

  useEffect(() => {
    const carregarDados = async () => {
      try {
        if (pages === "home") {
          const dados = await listarAreaELocalizacao();

          const listaMesclada: ItemLista[] = dados.localizacoes.map(
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
        } else if (pages === "patrimonio") {
          const dados = await listarHistorico(id);
          setItems(Array.isArray(dados) ? dados : []);
        } else if (pages === "local") {
          const dados = await listarPatrimoniosPorLocal(id);
          const listaFormatada = dados.map((patrimonio: any) => ({
            patrimonioID:
              patrimonio.patrimonioID ||
              patrimonio.PatrimonioID ||
              patrimonio.id,
            nif: patrimonio.nif || patrimonio.NIF || patrimonio.Nif || "N/A",
            nomePatrimonio:
              patrimonio.nomePatrimonio ||
              patrimonio.NomePatrimonio ||
              patrimonio.Denominacao ||
              patrimonio.denominacao ||
              "Não informado",
            data:
              patrimonio.dataTransferencia ||
              patrimonio.DataTransferencia ||
              "Não informada",
          }));

          console.log("Lista formatada de patrimônios:", listaFormatada);
          setItems(listaFormatada);
        }
      } catch (error: any) {
        erro("Erro ao carregar dados: " + error.message);
        setItems([]);
      }
    };

    carregarDados();
    setPaginaAtual(1);
  }, [pages, id]);

  // ADDED RETURN STATEMENT HERE
  return (
    <div className="column full_width small_gap">
      {pages === "home" && (
        <>
          <div className="caixa cabeçalho">
            <span>Local</span>
            <span>Área</span>
            <span>Responsável</span>
          </div>
          <hr className="line" />
          {itensPaginados.map((item) => (
            <Link
              href={`/local/${item.localizacaoID}`}
              key={item.localizacaoID}
              className="caixa bgc"
            >
              <span>{item.nomeLocal || "Sem local"}</span>
              <span>{item.nomeArea || "Sem área"}</span>
              <span>{item.responsavel || "Sem responsável"}</span>
            </Link>
          ))}
        </>
      )}

      {pages === "patrimonio" && (
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
          {itensPaginados.length > 0 ? (
            itensPaginados.map((item, idx) => (
              <div className="caixa bgc" key={item.id || idx}>
                <span>{item.data}</span>
                <span className="color">{item.tipo}</span>
                <span>{item.origem}</span>
                <span>{item.destino}</span>
                <span>{item.responsavel}</span>
                <span className="column">
                  <Button className="link">Info</Button>
                </span>
              </div>
            ))
          ) : (
            <p>Nenhum histórico encontrado</p>
          )}
        </>
      )}

      {pages === "local" && (
        <>
          <ul className="caixa">
            <li>Patrimônio</li>
            <li>Denominação</li>
            <li>Data Transferência</li>
            <li>Ações</li>
          </ul>
          <hr className="line" />
          {itensPaginados.length > 0 ? (
            itensPaginados.map((item) => (
              <Link
                href={`/patrimonio/${item.patrimonioID}`}
                className="caixa bgc"
                key={item.nif}
              >
                <span>{item.nif}</span>
                <span>{item.nomePatrimonio}</span>
                <span>{item.data}</span>
                <span className="side center">
                  <Button className="link">Editar</Button>
                  <Button className="link">Info</Button>
                </span>
              </Link>
            ))
          ) : (
            <p>Nenhum patrimônio encontrado</p>
          )}
        </>
      )}

      {totalPaginas > 1 && (
        <nav className="side center" aria-label="Paginação">
          <Button
            type="button"
            className="btn"
            aria-label="Página anterior"
            onClick={paginaAnterior}
            disabled={paginaAtual === 1}
          >
            &lt;
          </Button>

          {Array.from(
            { length: paginaFinal - paginaInicial + 1 },
            (_, index) => {
              const numeroDaPagina = paginaInicial + index;
              return (
                <Button
                  key={numeroDaPagina}
                  type="button"
                  onClick={() => setPaginaAtual(numeroDaPagina)}
                  className={paginaAtual === numeroDaPagina ? "btn" : "link"}
                >
                  {numeroDaPagina}
                </Button>
              );
            },
          )}

          <Button
            type="button"
            className="btn"
            aria-label="Próxima página"
            onClick={proximaPagina}
            disabled={paginaAtual === totalPaginas}
          >
            &gt;
          </Button>
        </nav>
      )}
    </div>
  );
};

export default Lista;
