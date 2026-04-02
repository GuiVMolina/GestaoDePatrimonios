using System;
using System.Collections.Generic;
using GestaoPatrimonios.Domains;
using Microsoft.EntityFrameworkCore;

namespace GestaoPatrimonios.Contexts;

public partial class GestaoPatrimoniosContext : DbContext
{
    public GestaoPatrimoniosContext()
    {
    }

    public GestaoPatrimoniosContext(DbContextOptions<GestaoPatrimoniosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Area { get; set; }

    public virtual DbSet<Bairro> Bairro { get; set; }

    public virtual DbSet<Cargo> Cargo { get; set; }

    public virtual DbSet<Cidade> Cidade { get; set; }

    public virtual DbSet<Endereco> Endereco { get; set; }

    public virtual DbSet<Localizacao> Localizacao { get; set; }

    public virtual DbSet<LogPatrimonio> LogPatrimonio { get; set; }

    public virtual DbSet<Patrimonio> Patrimonio { get; set; }

    public virtual DbSet<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; }

    public virtual DbSet<StatusPatrimonio> StatusPatrimonio { get; set; }

    public virtual DbSet<StatusTransferencia> StatusTransferencia { get; set; }

    public virtual DbSet<TipoAlteracao> TipoAlteracao { get; set; }

    public virtual DbSet<TipoPatrimonio> TipoPatrimonio { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaID).HasName("PK__Area__70B8202811FC5D25");

            entity.HasIndex(e => e.NomeArea, "UQ__Area__9A7797605CD337B1").IsUnique();

            entity.Property(e => e.AreaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeArea)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Bairro>(entity =>
        {
            entity.HasKey(e => e.BairroID).HasName("PK__Bairro__4A093623D979B030");

            entity.Property(e => e.BairroID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeBairro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cidade).WithMany(p => p.Bairro)
                .HasForeignKey(d => d.CidadeID)
                .HasConstraintName("FK_Bairro_Cidade");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoID).HasName("PK__Cargo__B4E665ED62C070F4");

            entity.HasIndex(e => e.NomeCargo, "UQ__Cargo__4D9FD7DE31E020D5").IsUnique();

            entity.Property(e => e.CargoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.CidadeID).HasName("PK__Cidade__B6800959EA2292C4");

            entity.Property(e => e.CidadeID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeCidade)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.EnderecoID).HasName("PK__Endereco__B9D9462F1F749219");

            entity.Property(e => e.EnderecoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CEP)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Complemento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Bairro).WithMany(p => p.Endereco)
                .HasForeignKey(d => d.BairroID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Endereco_Bairro");
        });

        modelBuilder.Entity<Localizacao>(entity =>
        {
            entity.HasKey(e => e.LocalizacaoID).HasName("PK__Localiza__83ABDECAE812FB43");

            entity.ToTable(tb => tb.HasTrigger("trg_Localizacao_SoftDelete"));

            entity.Property(e => e.LocalizacaoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.DescricaoSAP)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NomeLocal)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Area).WithMany(p => p.Localizacao)
                .HasForeignKey(d => d.AreaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Localizacao_Area");

            entity.HasMany(d => d.Usuario).WithMany(p => p.Localizacao)
                .UsingEntity<Dictionary<string, object>>(
                    "LocalUsuario",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioID")
                        .HasConstraintName("FK_LocalUsuario_Usuario"),
                    l => l.HasOne<Localizacao>().WithMany()
                        .HasForeignKey("LocalizacaoID")
                        .HasConstraintName("FK_LocalUsuario_Localizacao"),
                    j =>
                    {
                        j.HasKey("LocalizacaoID", "UsuarioID");
                    });
        });

        modelBuilder.Entity<LogPatrimonio>(entity =>
        {
            entity.HasKey(e => e.LogPatrimonioID).HasName("PK__LogPatri__E716D12B3F3A4B9D");

            entity.Property(e => e.LogPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataTransferencia).HasPrecision(0);

            entity.HasOne(d => d.Local).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.LocalID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_Localizacao");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.PatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_Patrimonio");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.StatusPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_StatusPatrimonio");

            entity.HasOne(d => d.TipoAlteracao).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.TipoAlteracaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_TipoAlteracao");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogPatrimonio_Usuario");
        });

        modelBuilder.Entity<Patrimonio>(entity =>
        {
            entity.HasKey(e => e.PatrimonioID).HasName("PK__Patrimon__C5A60BDED096D1EF");

            entity.ToTable(tb => tb.HasTrigger("trg_Patrimonio_SoftDelete"));

            entity.Property(e => e.PatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Denominacao).IsUnicode(false);
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.NumeroPatrimonio)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Local).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.LocalID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_Localizacao");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.StatusPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_StatusPatrimonio");

            entity.HasOne(d => d.TipoPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.TipoPatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_TipoPatrimonio");
        });

        modelBuilder.Entity<SolicitacaoTransferencia>(entity =>
        {
            entity.HasKey(e => e.TransferenciaID).HasName("PK__Solicita__E5B4F5F2C7F54CE5");

            entity.Property(e => e.TransferenciaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataCriacaoSolicitacao).HasColumnType("datetime");
            entity.Property(e => e.DataResposta).HasColumnType("datetime");
            entity.Property(e => e.Justificativa).IsUnicode(false);

            entity.HasOne(d => d.Local).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.LocalID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitacao_Localizacao");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.PatrimonioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitacao_Patrimonio");

            entity.HasOne(d => d.StatusTransferencia).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.StatusTransferenciaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitacao_StatusTransferencia");

            entity.HasOne(d => d.UsuarioIDAprovacaoNavigation).WithMany(p => p.SolicitacaoTransferenciaUsuarioIDAprovacaoNavigation)
                .HasForeignKey(d => d.UsuarioIDAprovacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitacao_UsuarioAprovacao");

            entity.HasOne(d => d.UsuarioIDSolicitadoNavigation).WithMany(p => p.SolicitacaoTransferenciaUsuarioIDSolicitadoNavigation)
                .HasForeignKey(d => d.UsuarioIDSolicitado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitacao_UsuarioSolicitado");
        });

        modelBuilder.Entity<StatusPatrimonio>(entity =>
        {
            entity.HasKey(e => e.StatusPatrimonioID).HasName("PK__StatusPa__B3F33609F4514A8E");

            entity.HasIndex(e => e.NomeStatus, "UQ__StatusPa__C5C60F1AB6AF0C4E").IsUnique();

            entity.Property(e => e.StatusPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusTransferencia>(entity =>
        {
            entity.HasKey(e => e.StatusTransferenciaID).HasName("PK__StatusTr__7AA828B9AF89121B");

            entity.HasIndex(e => e.NomeStatus, "UQ__StatusTr__C5C60F1AB8993559").IsUnique();

            entity.Property(e => e.StatusTransferenciaID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoAlteracao>(entity =>
        {
            entity.HasKey(e => e.TipoAlteracaoID).HasName("PK__TipoAlte__9BEF4F0D7A837C3E");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoAlte__7859A10A72A8344B").IsUnique();

            entity.Property(e => e.TipoAlteracaoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPatrimonio>(entity =>
        {
            entity.HasKey(e => e.TipoPatrimonioID).HasName("PK__TipoPatr__4DC9FF99937110D4");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoPatr__7859A10AC6D82B7B").IsUnique();

            entity.Property(e => e.TipoPatrimonioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioID).HasName("PK__TipoUsua__7F22C702614439EE");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoUsua__7859A10A8B1F2DB0").IsUnique();

            entity.Property(e => e.TipoUsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE7982311168D");

            entity.ToTable(tb => tb.HasTrigger("trg_Usuario_SoftDelete"));

            entity.Property(e => e.UsuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.CPF)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.CarteiraTrabalho)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NIF)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.NomeUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrimeiroAcesso).HasDefaultValue(true);
            entity.Property(e => e.RG)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.Cargo).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CargoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Cargo");

            entity.HasOne(d => d.Endereco).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.EnderecoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Endereco");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_TipoUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
