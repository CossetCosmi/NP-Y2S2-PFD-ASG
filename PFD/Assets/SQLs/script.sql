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
    [Id]        INTEGER IDENTITY (1, 1) NOT NULL,
    [Amount]    SMALLMONEY              NOT NULL,
    [CreateOn]  SMALLDATETIME           NOT NULL,
    [Type]      VARCHAR(10)             NOT NULL,
    [Status]    VARCHAR(10)             NOT NULL,
    [Recipient] CHAR(16) DEFAULT '0000000000000000',
    CONSTRAINT [PK_Transaction] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transaction_Recipient] FOREIGN KEY ([Recipient]) REFERENCES [Card] ([Id]),
    CONSTRAINT [CHK_Transaction_Type] CHECK ([Type] IN ('DEPOSIT', 'WITHDRAW', 'TRANSFER')),
    CONSTRAINT [CHK_Transaction_Status] CHECK ([Status] IN ('PENDING', 'REJECTED', 'COMPLETED')),
)

SET IDENTITY_INSERT [Account] ON;

INSERT INTO [Account]([Id], [Username], [Password], [Salt])
VALUES (0, 'DEFAULT', 'DEFAULT', 'DEFAULT');

SET IDENTITY_INSERT [Account] OFF;


INSERT INTO [Card] ([Id], [AccountId], [CVV], [IssueOn], [ExpireOn], [Balance], [Status], [PIN])
VALUES ('0000000000000000', 0, '000', '1900-01-01T00:00:00', '1900-01-01T00:00:00', 0, 'SUSPENDED', '000000');