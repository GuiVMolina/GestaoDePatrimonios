CREATE DATABASE GestaoPatrimonios;
GO

USE GestaoPatrimonios;
GO

-- ================================================ --
--                  LOCAL - ÁREA                    --
-- ================================================ --

CREATE TABLE Area (
    AreaID   UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeArea VARCHAR(50) UNIQUE NOT NULL
);
GO

CREATE TABLE Localizacao (
    LocalizacaoID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    LocalSAP      INT NULL,
    NomeLocal     VARCHAR(100),
    DescricaoSAP  VARCHAR(100),
    Ativo         BIT DEFAULT 1,
    AreaID        UNIQUEIDENTIFIER NOT NULL,
    
    CONSTRAINT FK_Localizacao_Area FOREIGN KEY (AreaID)
        REFERENCES Area(AreaID)
);
GO

-- ================================================ --
--            ENDEREÇO - BAIRRO - CIDADE            --
-- ================================================ --

CREATE TABLE Cidade (
    CidadeID   UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeCidade VARCHAR(50) NOT NULL,
    Estado     VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Bairro (
    BairroID   UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeBairro VARCHAR(50) NOT NULL,
    CidadeID   UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_Bairro_Cidade FOREIGN KEY (CidadeID)
        REFERENCES Cidade(CidadeID)
        ON DELETE CASCADE
);
GO

CREATE TABLE Endereco (
    EnderecoID  UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Numero      INT,
    Complemento VARCHAR(20),
    CEP         VARCHAR(10),
    Logradouro  VARCHAR(100) NOT NULL,
    BairroID    UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_Endereco_Bairro FOREIGN KEY (BairroID)
        REFERENCES Bairro(BairroID)
);
GO

-- ================================================ --
--                      TIPOS                       --
-- ================================================ --

CREATE TABLE TipoAlteracao (
    TipoAlteracaoID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeTipo        VARCHAR(50) UNIQUE NOT NULL
);
GO

CREATE TABLE TipoPatrimonio (
    TipoPatrimonioID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeTipo          VARCHAR(100) UNIQUE NOT NULL
);
GO

CREATE TABLE TipoUsuario (
    TipoUsuarioID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeTipo      VARCHAR(50) UNIQUE NOT NULL
);
GO

-- ================================================ --
--                      CARGO                       --
-- ================================================ --

CREATE TABLE Cargo (
    CargoID   UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeCargo VARCHAR(50) UNIQUE NOT NULL
);
GO

-- ================================================ --
--                      STATUS                      --
-- ================================================ --

CREATE TABLE StatusPatrimonio (
    StatusPatrimonioID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeStatus         VARCHAR(50) UNIQUE NOT NULL
);
GO

CREATE TABLE StatusTransferencia (
    StatusTransferenciaID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NomeStatus            VARCHAR(50) UNIQUE NOT NULL
);
GO

-- ================================================ --
--                     USUÁRIO                      --
-- ================================================ --

CREATE TABLE Usuario (
    UsuarioID        UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NIF              VARCHAR(7) NOT NULL,
    CPF              VARCHAR(11) NOT NULL,
    CarteiraTrabalho VARCHAR(14) NOT NULL,
    RG               VARCHAR(15) NOT NULL,
    NomeUsuario      VARCHAR(50) NOT NULL,
    Email            VARCHAR(150) NOT NULL,
    Senha            VARBINARY(32) NOT NULL,
    Ativo            BIT DEFAULT 1,
    PrimeiroAcesso   BIT DEFAULT 1 NOT NULL,
    CargoID          UNIQUEIDENTIFIER NOT NULL,
    EnderecoID       UNIQUEIDENTIFIER NOT NULL,
    TipoUsuarioID    UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_Usuario_Cargo FOREIGN KEY (CargoID) 
        REFERENCES Cargo(CargoID),

    CONSTRAINT FK_Usuario_Endereco FOREIGN KEY (EnderecoID) 
        REFERENCES Endereco(EnderecoID),

    CONSTRAINT FK_Usuario_TipoUsuario FOREIGN KEY (TipoUsuarioID) 
        REFERENCES TipoUsuario(TipoUsuarioID)
);
GO

-- ================================================ --
--                 PATRIMÔNIO - LOG                  --
-- ================================================ --

CREATE TABLE Patrimonio (
    PatrimonioID       UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    NumeroSerie        INT NOT NULL,
    NumeroPatrimonio   VARCHAR(30) NOT NULL,
    Imagem             VARCHAR(MAX),
    Denominacao        VARCHAR(MAX) NOT NULL,
    Valor              DECIMAL(10,2),
    Ativo              BIT DEFAULT 1,
    LocalID            UNIQUEIDENTIFIER NOT NULL,
    TipoPatrimonioID   UNIQUEIDENTIFIER NOT NULL,
    StatusPatrimonioID UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_Patrimonio_Localizacao FOREIGN KEY (LocalID)
        REFERENCES Localizacao(LocalizacaoID),

    CONSTRAINT FK_Patrimonio_TipoPatrimonio FOREIGN KEY (TipoPatrimonioID)
        REFERENCES TipoPatrimonio(TipoPatrimonioID),

    CONSTRAINT FK_Patrimonio_StatusPatrimonio FOREIGN KEY (StatusPatrimonioID)
        REFERENCES StatusPatrimonio(StatusPatrimonioID)
);
GO
    
CREATE TABLE LogPatrimonio (
    LogPatrimonioID    UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    DataTransferencia  DATETIME2(0) NOT NULL,
    LocalID            UNIQUEIDENTIFIER NOT NULL,
    UsuarioID          UNIQUEIDENTIFIER NOT NULL,
    PatrimonioID       UNIQUEIDENTIFIER NOT NULL,
    TipoAlteracaoID    UNIQUEIDENTIFIER NOT NULL,
    StatusPatrimonioID UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_LogPatrimonio_Localizacao FOREIGN KEY (LocalID)
        REFERENCES Localizacao(LocalizacaoID),

    CONSTRAINT FK_LogPatrimonio_Usuario FOREIGN KEY (UsuarioID)
        REFERENCES Usuario(UsuarioID),

    CONSTRAINT FK_LogPatrimonio_Patrimonio FOREIGN KEY (PatrimonioID)
        REFERENCES Patrimonio(PatrimonioID),

    CONSTRAINT FK_LogPatrimonio_TipoAlteracao FOREIGN KEY (TipoAlteracaoID)
        REFERENCES TipoAlteracao(TipoAlteracaoID),

    CONSTRAINT FK_LogPatrimonio_StatusPatrimonio FOREIGN KEY (StatusPatrimonioID)
        REFERENCES StatusPatrimonio(StatusPatrimonioID)
);
GO

-- ================================================ --
--                  TRANSFERÊNCIA                   --
-- ================================================ --

CREATE TABLE SolicitacaoTransferencia (
    TransferenciaID        UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    DataCriacaoSolicitacao DATETIME NOT NULL,
    DataResposta           DATETIME,
    Justificativa          VARCHAR(MAX) NOT NULL,
    LocalID                UNIQUEIDENTIFIER NOT NULL,
    PatrimonioID           UNIQUEIDENTIFIER NOT NULL,
    UsuarioIDAprovacao     UNIQUEIDENTIFIER NOT NULL,
    UsuarioIDSolicitado    UNIQUEIDENTIFIER NOT NULL,
    StatusTransferenciaID  UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_Solicitacao_Localizacao FOREIGN KEY (LocalID)
        REFERENCES Localizacao(LocalizacaoID),

    CONSTRAINT FK_Solicitacao_Patrimonio FOREIGN KEY (PatrimonioID)
        REFERENCES Patrimonio(PatrimonioID),

    CONSTRAINT FK_Solicitacao_UsuarioAprovacao FOREIGN KEY (UsuarioIDAprovacao)
        REFERENCES Usuario(UsuarioID),

    CONSTRAINT FK_Solicitacao_UsuarioSolicitado FOREIGN KEY (UsuarioIDSolicitado)
        REFERENCES Usuario(UsuarioID),

    CONSTRAINT FK_Solicitacao_StatusTransferencia FOREIGN KEY (StatusTransferenciaID)
        REFERENCES StatusTransferencia(StatusTransferenciaID)
);
GO

-- ================================================ --
--        TABELA INTERMEDIÁRIA (LocalUsuário)        --
-- ================================================ --

CREATE TABLE LocalUsuario (
    LocalizacaoID UNIQUEIDENTIFIER NOT NULL,
    UsuarioID     UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT PK_LocalUsuario
        PRIMARY KEY (LocalizacaoID, UsuarioID),

    CONSTRAINT FK_LocalUsuario_Localizacao
        FOREIGN KEY (LocalizacaoID) REFERENCES Localizacao(LocalizacaoID)
        ON DELETE CASCADE,

    CONSTRAINT FK_LocalUsuario_Usuario
        FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID)
        ON DELETE CASCADE
);
GO

-- ================================================ --
--                     TRIGGERS                     --
-- ================================================ --

-- Soft delete Usuário
CREATE TRIGGER trg_Usuario_SoftDelete
ON Usuario
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Usuario
        SET Ativo = 0
        WHERE UsuarioID IN (SELECT UsuarioID FROM deleted)
END
GO

-- Soft delete Localização
CREATE TRIGGER trg_Localizacao_SoftDelete
ON Localizacao
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Localizacao
        SET Ativo = 0
        WHERE LocalizacaoID IN (SELECT LocalizacaoID FROM deleted)
END
GO

-- Soft delete Patrimônio (CORRIGIDO)
CREATE TRIGGER trg_Patrimonio_SoftDelete
ON Patrimonio
INSTEAD OF DELETE
AS
BEGIN
    DECLARE @IDInativo UNIQUEIDENTIFIER = (SELECT StatusPatrimonioID FROM StatusPatrimonio WHERE NomeStatus = 'Inativo');
    
    UPDATE Patrimonio
        SET StatusPatrimonioID = @IDInativo,
            Ativo = 0
        WHERE PatrimonioID IN (SELECT PatrimonioID FROM deleted)
END
GO

-- ================================================ --
--                      INSERTS                     --
-- ================================================ --

-- Área
INSERT INTO Area (NomeArea) VALUES ('Bloco A - Térreo'), ('Bloco A - 1º Andar');

-- TipoUsuário
INSERT INTO TipoUsuario (NomeTipo) VALUES ('Responsável'), ('Coordenador');

-- Cargo
INSERT INTO Cargo (NomeCargo) VALUES ('Diretor'), ('Instrutor de Formação Profissional II');

-- TipoPatrimônio
INSERT INTO TipoPatrimonio (NomeTipo) VALUES ('Mesa'), ('Notebook');

-- StatusPatrimonio
INSERT INTO StatusPatrimonio (NomeStatus) VALUES ('Inativo'), ('Ativo'), ('Transferido'), ('Em manutenção');

-- StatusTransferencia
INSERT INTO StatusTransferencia (NomeStatus) VALUES ('Pendente de aprovação'), ('Aprovado'), ('Recusado');

-- TipoAlteração (Consolidado para evitar erro de UNIQUE)
INSERT INTO TipoAlteracao (NomeTipo) VALUES ('Modificação'), ('Transferência'), ('Atualização de dados');

-- Cidade
INSERT INTO Cidade (NomeCidade, Estado) VALUES ('São Caetano do Sul', 'São Paulo'), ('Diadema', 'São Paulo');

-- Localização (Usando subquery para pegar o ID da Área)
INSERT INTO Localizacao(LocalSAP, DescricaoSAP, NomeLocal, AreaID) VALUES
(NULL, 'Descricao Exemplo', 'Manutenção', (SELECT AreaID FROM Area WHERE NomeArea = 'Bloco A - Térreo'));

-- Bairro
INSERT INTO Bairro (NomeBairro, CidadeID) VALUES
('Centro', (SELECT CidadeID FROM Cidade WHERE NomeCidade = 'São Caetano do Sul'));
GO