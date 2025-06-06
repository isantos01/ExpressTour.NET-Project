USE [master]
GO
/****** Object:  Database [ExpressTour]    Script Date: 4/2/2025 11:40:50 ******/
CREATE DATABASE [ExpressTour]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ExpressTour', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ExpressTour.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ExpressTour_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ExpressTour_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ExpressTour] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExpressTour].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExpressTour] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ExpressTour] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ExpressTour] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ExpressTour] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ExpressTour] SET ARITHABORT OFF 
GO
ALTER DATABASE [ExpressTour] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ExpressTour] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ExpressTour] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ExpressTour] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ExpressTour] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ExpressTour] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ExpressTour] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ExpressTour] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ExpressTour] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ExpressTour] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ExpressTour] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ExpressTour] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ExpressTour] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ExpressTour] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ExpressTour] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ExpressTour] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ExpressTour] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ExpressTour] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ExpressTour] SET  MULTI_USER 
GO
ALTER DATABASE [ExpressTour] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ExpressTour] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ExpressTour] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ExpressTour] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ExpressTour] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ExpressTour] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ExpressTour] SET QUERY_STORE = OFF
GO
USE [ExpressTour]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[correo] [nvarchar](100) NOT NULL,
	[telefono] [nvarchar](15) NULL,
	[direccion] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[excursiones]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[excursiones](
	[id_excursion] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[descripcion] [nvarchar](max) NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[capacidad] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_excursion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[facturas]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[facturas](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[id_reserva] [int] NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[fecha_emision] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[opiniones_clientes]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[opiniones_clientes](
	[id_opinion] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_paquete] [int] NOT NULL,
	[comentario] [nvarchar](max) NULL,
	[calificacion] [int] NULL,
	[fecha_publicacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_opinion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paquetes]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paquetes](
	[id_paquete] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[descripcion] [nvarchar](max) NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[duracion_dias] [int] NOT NULL,
	[id_transporte] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_paquete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedores]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedores](
	[id_proveedor] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[contacto] [nvarchar](100) NULL,
	[telefono] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reservas]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reservas](
	[id_reserva] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_paquete] [int] NOT NULL,
	[fecha_reserva] [datetime] NULL,
	[estado] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transporte]    Script Date: 4/2/2025 11:40:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transporte](
	[id_transporte] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [nvarchar](50) NOT NULL,
	[capacidad] [int] NOT NULL,
	[id_proveedor] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_transporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[facturas] ADD  DEFAULT (getdate()) FOR [fecha_emision]
GO
ALTER TABLE [dbo].[opiniones_clientes] ADD  DEFAULT (getdate()) FOR [fecha_publicacion]
GO
ALTER TABLE [dbo].[reservas] ADD  DEFAULT (getdate()) FOR [fecha_reserva]
GO
ALTER TABLE [dbo].[facturas]  WITH CHECK ADD FOREIGN KEY([id_reserva])
REFERENCES [dbo].[reservas] ([id_reserva])
GO
ALTER TABLE [dbo].[opiniones_clientes]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[opiniones_clientes]  WITH CHECK ADD FOREIGN KEY([id_paquete])
REFERENCES [dbo].[paquetes] ([id_paquete])
GO
ALTER TABLE [dbo].[paquetes]  WITH CHECK ADD FOREIGN KEY([id_transporte])
REFERENCES [dbo].[transporte] ([id_transporte])
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD FOREIGN KEY([id_paquete])
REFERENCES [dbo].[paquetes] ([id_paquete])
GO
ALTER TABLE [dbo].[transporte]  WITH CHECK ADD FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedores] ([id_proveedor])
GO
ALTER TABLE [dbo].[opiniones_clientes]  WITH CHECK ADD CHECK  (([calificacion]>=(1) AND [calificacion]<=(5)))
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD CHECK  (([estado]='cancelada' OR [estado]='confirmada' OR [estado]='pendiente'))
GO
USE [master]
GO
ALTER DATABASE [ExpressTour] SET  READ_WRITE 
GO
