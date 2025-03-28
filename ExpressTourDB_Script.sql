USE [master]
GO
/****** Object:  Database [ExpressTour]    Script Date: 3/18/2025 12:54:41 ******/
CREATE DATABASE [ExpressTour]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DATABASE ExpressTour', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DATABASE ExpressTour.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DATABASE ExpressTour_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DATABASE ExpressTour_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[clientes]    Script Date: 3/18/2025 12:54:41 ******/
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
	[fecha_registro] [datetime] NULL,
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
/****** Object:  Table [dbo].[empleados]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empleados](
	[id_empleado] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[puesto] [nvarchar](50) NULL,
	[salario] [decimal](10, 2) NULL,
	[fecha_contratacion] [date] NULL,
	[correo] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[empleados_reservas]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empleados_reservas](
	[id_empleado_reserva] [int] IDENTITY(1,1) NOT NULL,
	[id_empleado] [int] NOT NULL,
	[id_reserva] [int] NOT NULL,
	[rol] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_empleado_reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[excursiones]    Script Date: 3/18/2025 12:54:41 ******/
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
/****** Object:  Table [dbo].[facturas]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[facturas](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[id_reserva] [int] NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[fecha_emision] [datetime] NULL,
	[metodo_pago] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[opiniones_clientes]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[opiniones_clientes](
	[id_opinion] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_paquete] [int] NOT NULL,
	[calificacion] [int] NULL,
	[comentario] [nvarchar](max) NULL,
	[fecha] [datetime] NULL,
	[respuesta_empresa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_opinion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pagos]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pagos](
	[id_pago] [int] IDENTITY(1,1) NOT NULL,
	[id_reserva] [int] NOT NULL,
	[monto] [decimal](10, 2) NOT NULL,
	[metodo_pago] [nvarchar](50) NULL,
	[fecha_pago] [datetime] NULL,
	[referencia_pago] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[referencia_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paquetes]    Script Date: 3/18/2025 12:54:41 ******/
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
	[categoria] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_paquete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedores]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedores](
	[id_proveedor] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[contacto] [nvarchar](100) NULL,
	[telefono] [nvarchar](15) NULL,
	[correo] [nvarchar](100) NULL,
	[direccion] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reservas]    Script Date: 3/18/2025 12:54:41 ******/
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
	[comentarios] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reservas_excursiones]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reservas_excursiones](
	[id_reserva_excursion] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_excursion] [int] NOT NULL,
	[fecha_reserva] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_reserva_excursion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transporte]    Script Date: 3/18/2025 12:54:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transporte](
	[id_transporte] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [nvarchar](50) NOT NULL,
	[capacidad] [int] NOT NULL,
	[placa] [nvarchar](20) NOT NULL,
	[estado] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_transporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[clientes] ADD  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[facturas] ADD  DEFAULT (getdate()) FOR [fecha_emision]
GO
ALTER TABLE [dbo].[opiniones_clientes] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[pagos] ADD  DEFAULT (getdate()) FOR [fecha_pago]
GO
ALTER TABLE [dbo].[reservas] ADD  DEFAULT (getdate()) FOR [fecha_reserva]
GO
ALTER TABLE [dbo].[reservas_excursiones] ADD  DEFAULT (getdate()) FOR [fecha_reserva]
GO
ALTER TABLE [dbo].[empleados_reservas]  WITH CHECK ADD  CONSTRAINT [FK_Empleado] FOREIGN KEY([id_empleado])
REFERENCES [dbo].[empleados] ([id_empleado])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[empleados_reservas] CHECK CONSTRAINT [FK_Empleado]
GO
ALTER TABLE [dbo].[empleados_reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reserva] FOREIGN KEY([id_reserva])
REFERENCES [dbo].[reservas] ([id_reserva])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[empleados_reservas] CHECK CONSTRAINT [FK_Reserva]
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
ALTER TABLE [dbo].[pagos]  WITH CHECK ADD FOREIGN KEY([id_reserva])
REFERENCES [dbo].[reservas] ([id_reserva])
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD FOREIGN KEY([id_paquete])
REFERENCES [dbo].[paquetes] ([id_paquete])
GO
ALTER TABLE [dbo].[reservas_excursiones]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[reservas_excursiones]  WITH CHECK ADD FOREIGN KEY([id_excursion])
REFERENCES [dbo].[excursiones] ([id_excursion])
GO
ALTER TABLE [dbo].[empleados_reservas]  WITH CHECK ADD CHECK  (([rol]='atención al cliente' OR [rol]='guía turístico' OR [rol]='chofer'))
GO
ALTER TABLE [dbo].[facturas]  WITH CHECK ADD CHECK  (([metodo_pago]='transferencia' OR [metodo_pago]='efectivo' OR [metodo_pago]='tarjeta'))
GO
ALTER TABLE [dbo].[opiniones_clientes]  WITH CHECK ADD CHECK  (([calificacion]>=(1) AND [calificacion]<=(5)))
GO
ALTER TABLE [dbo].[pagos]  WITH CHECK ADD CHECK  (([metodo_pago]='transferencia' OR [metodo_pago]='efectivo' OR [metodo_pago]='tarjeta'))
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD CHECK  (([estado]='cancelada' OR [estado]='confirmada' OR [estado]='pendiente'))
GO
ALTER TABLE [dbo].[transporte]  WITH CHECK ADD CHECK  (([estado]='ocupado' OR [estado]='mantenimiento' OR [estado]='disponible'))
GO
USE [master]
GO
ALTER DATABASE [ExpressTour] SET  READ_WRITE 
GO
