USE ProyectoBackendUTN;
CREATE DATABASE ProyectoBackendUTN;
-- Se realizaron las tablas con una migracion
-- Las tablas estan realizadas en base a los Models
-- Porque el context esta en base a eso

CREATE TABLE CategoriaProductos(
	CategoriaProductoID int IDENTITY,
	Nombre VARCHAR(100),
	PRIMARY KEY(CategoriaProductoID)
);

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
	RolID int,
	DomicilioID int,
	eliminado bit,
	PRIMARY KEY(UsuarioID)
);

select * from Roles;
select * from Usuarios;

select U.UsuarioID,U.Email,U.Password,U.Nombre,U.Apellido,U.FechaNacimiento,D.Nombre,D.Numero,D.Piso,D.Departamento,R.RolID,R.Nombre 
from Usuarios as U
Inner Join Roles as R
on U.RolID = R.RolID
Inner Join Domicilios as D
on D.DomicilioID = U.DomicilioID;