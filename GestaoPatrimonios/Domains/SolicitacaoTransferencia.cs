using System;
using System.Collections.Generic;

namespace GestaoPatrimonios.Domains;

public partial class SolicitacaoTransferencia
{
    public Guid TransferenciaID { get; set; }

    public DateTime DataCriacaoSolicitacao { get; set; }

    public DateTime? DataResposta { get; set; }

    public string Justificativa { get; set; } = null!;

    public Guid LocalID { get; set; }

    public Guid PatrimonioID { get; set; }

    public Guid UsuarioIDAprovacao { get; set; }

    public Guid UsuarioIDSolicitado { get; set; }

    public Guid StatusTransferenciaID { get; set; }

    public virtual Localizacao Local { get; set; } = null!;

    public virtual Patrimonio Patrimonio { get; set; } = null!;

    public virtual StatusTransferencia StatusTransferencia { get; set; } = null!;

    public virtual Usuario UsuarioIDAprovacaoNavigation { get; set; } = null!;

    public virtual Usuario UsuarioIDSolicitadoNavigation { get; set; } = null!;
}
