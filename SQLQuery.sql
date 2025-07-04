USE ProyectoBackendUTN;
CREATE DATABASE ProyectoBackendUTN;
-- Se realizaron las tablas con una migracion
-- Las tablas estan realizadas en base a los Models
-- Porque el context esta en base a eso

CREATE TABLE Roles(
	RolID int IDENTITY,
	Nombre VARCHAR(100),
	PRIMARY KEY(RolId)
);

CREATE TABLE Domicilios(
	DomicilioID int IDENTITY,
	Nombre VARCHAR(100),
	Numero int,
	Piso int,
	PRIMARY KEY(DomicilioID)
);

CREATE TABLE Usuarios(
	UsuarioID int IDENTITY,
	Email VARCHAR(100),
	Password VARCHAR(100),
	Nombre VARCHAR(100),
	Apellido VARCHAR(100),
	FechaNacimiento date,
	eliminado bit,
	DomicilioId int,
	RolId int,
	PRIMARY KEY(UsuarioID),
	FOREIGN KEY (DomicilioId) REFERENCES Domicilios(DomicilioID),
	FOREIGN KEY (RolId) REFERENCES Roles(RolID)
);

CREATE TABLE CategoriaProductos(
	CategoriaProductoID int IDENTITY,
	Nombre VARCHAR(100),
	PRIMARY KEY(CategoriaProductoID)
);

CREATE TABLE Marcas(
	MarcaID int IDENTITY,
	Nombre VARCHAR(100),
	PRIMARY KEY(MarcaID)
);

CREATE TABLE Productos(
	ProductoID int IDENTITY,
	CodigoML VARCHAR(100),
	Titulo VARCHAR(100),
	Precio VARCHAR(100),
	Cantidad int,
	Imagen int,
	FechaCreacion date,
	Eliminado bit,
	MarcaId int,
	CategoriaProductoID int,
	PRIMARY KEY(ProductoID),
	FOREIGN KEY (MarcaId) REFERENCES Marcas(MarcaID),
	FOREIGN KEY (CategoriaProductoID) REFERENCES CategoriaProductos(CategoriaProductoID)
);


select * from Roles;
select * from Domicilios;
select * from Usuarios;

-- Inner Join con informacion general de Usuario y sus tablas foraneas
Select U.UsuarioID,U.Email,U.Password,U.Nombre,U.Apellido,U.FechaNacimiento,D.Nombre,D.Numero,D.Piso,D.Departamento,R.RolID,R.Nombre 
From Usuarios as U
Inner Join Roles as R
On U.RolID = R.RolID
Inner Join Domicilios as D
On D.DomicilioID = U.DomicilioID;

--Productos

select * from Marcas;
select * from Productos;
select * from CategoriaProductos;

-- Inner Join con informacion general de Productos y sus tablas foraneas
Select P.ProductoID,P.Titulo,P.Precio,P.Cantidad,P.FechaCreacion,M.MarcaID,M.Nombre,C.CategoriaProductoID,C.Nombre
From Productos as P
Inner Join Marcas as M
On P.MarcaID = M.MarcaID
Inner Join CategoriaProductos as C
On P.CategoriaProductoID = C.CategoriaProductoID;