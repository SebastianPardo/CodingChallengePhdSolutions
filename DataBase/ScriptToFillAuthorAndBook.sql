USE [BookStore]
GO
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (1, N'Sebastian Pardo', CAST(N'1992-09-22T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Author] OFF
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([Id], [Tittle], [Description], [CoverImage], [ReleaseDate], [Price], [Quantity], [AuthorId], [TotalNumberOfPages]) VALUES (1, N'How to present a code challenge', N'new book', N'https://firebasestorage.googleapis.com/v0/b/phdsolutionscodechallenge.appspot.com/o/Book1.jpg?alt=media&token=000870c1-baee-4bfd-9fa3-9fe7aaa3495a', CAST(N'2020-09-10T00:00:00.0000000' AS DateTime2), CAST(50.00 AS Decimal(18, 2)), 83, 1, 300)
INSERT [dbo].[Book] ([Id], [Tittle], [Description], [CoverImage], [ReleaseDate], [Price], [Quantity], [AuthorId], [TotalNumberOfPages]) VALUES (3, N'Code Challnge To Dummies', N'Other book', N'https://firebasestorage.googleapis.com/v0/b/phdsolutionscodechallenge.appspot.com/o/Book2.jpg?alt=media&token=76fc0f65-2434-460d-902e-f11e26174287', CAST(N'2020-10-11T00:00:00.0000000' AS DateTime2), CAST(20.00 AS Decimal(18, 2)), 190, 1, 100)
INSERT [dbo].[Book] ([Id], [Tittle], [Description], [CoverImage], [ReleaseDate], [Price], [Quantity], [AuthorId], [TotalNumberOfPages]) VALUES (5, N'My dreams', N'Autobiography ', N'https://firebasestorage.googleapis.com/v0/b/phdsolutionscodechallenge.appspot.com/o/Book3.jpg?alt=media&token=0b255c10-bda5-4898-a3e1-cb925fdcbdbf', CAST(N'2021-01-01T00:00:00.0000000' AS DateTime2), CAST(70.00 AS Decimal(18, 2)), 10, 1, 50)
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
