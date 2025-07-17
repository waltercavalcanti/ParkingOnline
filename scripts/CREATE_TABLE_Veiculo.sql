USE [ParkingOnlineDB]
GO

ALTER TABLE [dbo].[Veiculo] DROP CONSTRAINT [FK_Veiculo_Cliente]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Veiculo]') AND type in (N'U'))
DROP TABLE [dbo].[Veiculo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Veiculo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [varchar](100) NULL,
	[Modelo] [varchar](100) NULL,
	[Placa] [varchar](7) NOT NULL,
	[ClienteId] [int] NOT NULL,
 CONSTRAINT [PK_Veiculo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Veiculo]  WITH CHECK ADD  CONSTRAINT [FK_Veiculo_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Veiculo] CHECK CONSTRAINT [FK_Veiculo_Cliente]
GO
