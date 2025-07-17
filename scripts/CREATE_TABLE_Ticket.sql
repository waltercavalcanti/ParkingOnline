USE [ParkingOnlineDB]
GO

ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Veiculo]
GO

ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Vaga]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataEntrada] [datetime] NOT NULL,
	[DataSaida] [datetime] NULL,
	[Valor] [decimal](18, 2) NULL,
	[VeiculoId] [int] NOT NULL,
	[VagaId] [int] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Vaga] FOREIGN KEY([VagaId])
REFERENCES [dbo].[Vaga] ([Id])
GO

ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Vaga]
GO

ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Veiculo] FOREIGN KEY([VeiculoId])
REFERENCES [dbo].[Veiculo] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Veiculo]
GO
