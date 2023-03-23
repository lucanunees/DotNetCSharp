USE [Blog]
GO

--Usuario
CREATE TABLE [User](
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(80) NOT NULL, --Pode conter caracteres especiais por isso nvarchar
    [Email] VARCHAR(200) NOT NULL, -- não pode ter caracteres especiais por isso varchar
    [PassWordHash] VARCHAR(255) NOT NULL,
    [Bio] TEXT NOT NULL, -- O tipo text não tem limite de caracteres
    [Image] VARCHAR(2000) NOT NULL, -- vamos armazenar a url da imagem
    [Slug] VARCHAR(80) NOT NULL, -- url exemplo balta.io/user/

    CONSTRAINT [PK_User] PRIMARY KEY([Id]), -- renomeamos a primary key utilizando o constraint
    CONSTRAINT [UQ_User_Email] UNIQUE([Email]), -- as duas constraint abaixo define que os campos serão unicos do email e slug (salvar e atualizar).
    CONSTRAINT [UQ_User_Slug] UNIQUE([Slug])
)

-- Criamos dois index para otimizar a pesquisa utilizando o email e o slug da tabela user.
CREATE NONCLUSTERED INDEX [IX_User_Email] ON [User]([Email])
CREATE NONCLUSTERED INDEX [IX_User_slug] ON [User]([Slug])

/*
    indice clustered  ele realiza a pesquisa de uma forma mais ordenada.. por exemplo:
    Se eu tenho uma tabela com 10.000 linhas e quero pegar o cliente com o ID = 500, cujo o ID  é a PK e automaticamete é clustered
    existe uma paginação dentro do sql que ja sabe aonde esse cliente está então torna mais rapida a pesquisa.

    Quando colocamos como NONCLUSTERED, ele irá gerar um outro registro no banco aonde ele vai salvar os e-mails ordenados. 
*/
GO


CREATE TABLE [Role](
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] VARCHAR(80) NOT NULL,
    [Slug] VARCHAR(80) NOT NULL,

    CONSTRAINT [PK_Role] PRIMARY KEY ([Id]),
    CONSTRAINT [UQ_Role_Slug] UNIQUE([Slug])   
)
CREATE NONCLUSTERED INDEX [IX_Role_Slug] ON [Role]([Slug])
GO

--Perfil do usuario.
CREATE TABLE [UserRole](
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,

    CONSTRAINT [PK_UserRole] PRIMARY KEY([UserId], [RoleId]) -- aqui temos uma chave composta.
)

GO
CREATE TABLE [Category](
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] VARCHAR(80) NOT NULL,
    [Slug] VARCHAR(80) NOT NULL,

    CONSTRAINT [PK_Category] PRIMARY KEY([Id]),
    CONSTRAINT [UQ_Category_Slug] UNIQUE([slug])
)
CREATE NONCLUSTERED INDEX [IX_Category_Slug] ON [Category]([Slug])
GO

CREATE TABLE [Post](
    [Id] INT NOT NULL IDENTITY(1, 1),
    [CategoryId] INT NOT NULL,
    [AuthorID] INT NOT NULL, --usuario
    [Title] VARCHAR(160) NOT NULL,
    [Summary] VARCHAR(255) NOT NULL,
    [Body] TEXT NOT NULL,
    [Slug]VARCHAR(80) NOT NULL,
    [CreateDate] DATETIME NOT NULL DEFAULT(GETDATE()),
    [LastUpdateDate] DATETIME NOT NULL DEFAULT(GETDATE()),

    CONSTRAINT [PK_Post] PRIMARY KEY([Id]),
    CONSTRAINT [FK_Post_Category] FOREIGN KEY([CategoryId]) REFERENCES [Category]([Id]),
    CONSTRAINT [FK_Post_Author] FOREIGN KEY([AuthorID]) REFERENCES [User]([Id]),
    CONSTRAINT [UQ_Post_Slug] UNIQUE([Slug])
)
CREATE NONCLUSTERED INDEX [IX_Post_Slug] ON [Post]([Slug])
GO

CREATE TABLE [TAG] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Name] VARCHAR(80) NOT NULL,
    [Slug] VARCHAR(80) NOT NULL,

    CONSTRAINT [PK_Tag] PRIMARY KEY([Id]),
    CONSTRAINT [UQ_Tag_Slug] UNIQUE([slug])
)
CREATE NONCLUSTERED INDEX [IX_Tag_Slug] ON [TAG]([Slug])

CREATE TABLE [PostTag](
    [PostId] INT NOT NULL,
    [TagId] INT NOT NULL,

    CONSTRAINT [PK_Post_Tag] PRIMARY KEY([PostId], [TagId])
)