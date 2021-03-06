
USE [OnlineStore]
GO
/****** Object:  Table [dbo].[ProductItem]    Script Date: 10/31/2018 5:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SKU] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductDescription] [nvarchar](500) NOT NULL,
	[ProductPrice] [money] NOT NULL,
 CONSTRAINT [PK_ProductItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesOrder]    Script Date: 10/31/2018 5:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[Total] [money] NOT NULL,
	[Tax] [money] NOT NULL,
	[Discount] [money] NOT NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesOrderItem]    Script Date: 10/31/2018 5:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesOrderId] [int] NOT NULL,
	[ProductItemId] [int] NOT NULL,
 CONSTRAINT [PK_SalesOrderItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ProductItem] ON 

GO
INSERT [dbo].[ProductItem] ([Id], [SKU], [ProductName], [ProductDescription], [ProductPrice]) VALUES (1, N'1234', N'Shoes', N'Shoes - Black', 34.0000)
GO
INSERT [dbo].[ProductItem] ([Id], [SKU], [ProductName], [ProductDescription], [ProductPrice]) VALUES (2, N'2345', N'Apparel', N'Skirt', 32.0000)
GO
INSERT [dbo].[ProductItem] ([Id], [SKU], [ProductName], [ProductDescription], [ProductPrice]) VALUES (3, N'3456', N'Leotard', N'L', 33.0000)
GO
SET IDENTITY_INSERT [dbo].[ProductItem] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesOrder] ON 

GO
INSERT [dbo].[SalesOrder] ([Id], [Code], [OrderDate], [Total], [Tax], [Discount]) VALUES (1, N'SO1234', CAST(N'2017-10-30 00:00:00.000' AS DateTime), 66.0000, 3.0000, 2.0000)
GO
INSERT [dbo].[SalesOrder] ([Id], [Code], [OrderDate], [Total], [Tax], [Discount]) VALUES (3, N'SO2345', CAST(N'2018-10-31 00:00:00.000' AS DateTime), 67.0000, 3.0000, 5.0000)
GO
SET IDENTITY_INSERT [dbo].[SalesOrder] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesOrderItem] ON 

GO
INSERT [dbo].[SalesOrderItem] ([Id], [SalesOrderId], [ProductItemId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[SalesOrderItem] ([Id], [SalesOrderId], [ProductItemId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[SalesOrderItem] ([Id], [SalesOrderId], [ProductItemId]) VALUES (3, 1, 3)
GO
INSERT [dbo].[SalesOrderItem] ([Id], [SalesOrderId], [ProductItemId]) VALUES (6, 3, 2)
GO
SET IDENTITY_INSERT [dbo].[SalesOrderItem] OFF
GO
ALTER TABLE [dbo].[SalesOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderItem_ProductItem] FOREIGN KEY([ProductItemId])
REFERENCES [dbo].[ProductItem] ([Id])
GO
ALTER TABLE [dbo].[SalesOrderItem] CHECK CONSTRAINT [FK_SalesOrderItem_ProductItem]
GO
ALTER TABLE [dbo].[SalesOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderItem_SalesOrder] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrder] ([Id])
GO
ALTER TABLE [dbo].[SalesOrderItem] CHECK CONSTRAINT [FK_SalesOrderItem_SalesOrder]
GO

