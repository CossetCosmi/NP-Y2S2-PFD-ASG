CREATE DATABASE [PFD];

USE [PFD];

CREATE TABLE [Account] (
    [Id]       INTEGER IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR(100)            NOT NULL,
    [Password] CHAR(44)                NOT NULL,
    [Salt]     CHAR(24)                NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Username] UNIQUE CLUSTERED ([Username] ASC),
)

CREATE TABLE [Card] (
    [Id]        CHAR(16)      NOT NULL,
    [AccountId] INTEGER       NOT NULL,
    [CVV]       CHAR(3)       NOT NULL,
    [IssueOn]   SMALLDATETIME NOT NULL,
    [ExpireOn]  SMALLDATETIME NOT NULL,
    [Balance]   SMALLMONEY    NOT NULL,
    [Status]    VARCHAR(10)   NOT NULL,
    [PIN]       CHAR(6)       NOT NULL,
    CONSTRAINT [PK_Card] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Card_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]),
    CONSTRAINT [CHK_Card_Status] CHECK ([Status] IN ('PENDING', 'ACTIVE', 'SUSPENDED', 'EXPIRED')),
)

CREATE TABLE [Transaction] (
    [Id]       INTEGER IDENTITY (1, 1) NOT NULL,
    [Card]     CHAR(16)                NOT NULL,
    [Amount]   SMALLMONEY              NOT NULL,
    [CreateOn] SMALLDATETIME           NOT NULL,
    [Type]     VARCHAR(15)             NOT NULL,
    [Status]   VARCHAR(10)             NOT NULL,
    [To]       CHAR(16)                NULL,
    [From]     CHAR(16)                NULL,
    [ATM]      INT                     NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transaction_Card] FOREIGN KEY ([Card]) REFERENCES [Card] ([Id]),
    CONSTRAINT [FK_Transaction_To] FOREIGN KEY ([To]) REFERENCES [Card] ([Id]),
    CONSTRAINT [FK_Transaction_From] FOREIGN KEY ([From]) REFERENCES [Card] ([Id]),
    CONSTRAINT [CHK_Transaction_Type] CHECK ([Type] IN ('DEPOSIT', 'WITHDRAW', 'TRANSFER (IN)', 'TRANSFER (OUT)')),
    CONSTRAINT [CHK_Transaction_Status] CHECK ([Status] IN ('PENDING', 'REJECTED', 'COMPLETED')),
)