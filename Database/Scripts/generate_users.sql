USE [Top2000]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleKey], [RoleName]) VALUES (1, N'user', N'Gebruiker')
INSERT [dbo].[Role] ([RoleID], [RoleKey], [RoleName]) VALUES (2, N'admin', N'Administrator')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Email], [FirstName], [LastName], [PasswordHash], [RoleID]) VALUES (6, N'admin@example.com', N'John', N'Winkles', N'9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 2)
INSERT [dbo].[User] ([UserID], [Email], [FirstName], [LastName], [PasswordHash], [RoleID]) VALUES (7, N'user@example.com', N'Cold', N'Ice', N'9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 1)
INSERT [dbo].[User] ([UserID], [Email], [FirstName], [LastName], [PasswordHash], [RoleID]) VALUES (8, N'spam@account.nl', N'Spam', N'Account', N'54e8c0ceac3b1b56d2c45fa8e738eb9441d884baa934058d6e69e221459ebb55', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
