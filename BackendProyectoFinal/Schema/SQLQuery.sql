USE ProyectoBackendUTN;
CREATE DATABASE ProyectoBackendUTN;
-- Se realizaron las tablas con una migracion
-- Las tablas estan realizadas en base a los Models
-- Porque el context esta en base a eso

CREATE TABLE Roles(
	RoleID int IDENTITY,
	Name VARCHAR(100),
	PRIMARY KEY(RoleID)
);

CREATE TABLE Addresses(
	AddressID int IDENTITY,
	Name VARCHAR(100),
	Number int,
	Floor int,
	ApartmentNumber VARCHAR(100)
	PRIMARY KEY(AddressID)
);

CREATE TABLE Users(
	UserID int IDENTITY,
	Email VARCHAR(100),
	Password VARCHAR(100),
	FirstName VARCHAR(100),
	SurName VARCHAR(100),
	CreationDate date,
	Eliminated bit,
	AddressID int,
	RoleID int,
	PRIMARY KEY(UserID),
	FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID),
	FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

CREATE TABLE Categories(
	CategoryID int IDENTITY,
	Name VARCHAR(100),
	PRIMARY KEY(CategoryID)
);

CREATE TABLE Brands(
	BrandID int IDENTITY,
	Name VARCHAR(100),
	PRIMARY KEY(BrandID)
);

CREATE TABLE Products(
	ProductID int IDENTITY,
	MLCode VARCHAR(100),
	Title VARCHAR(100),
	Price VARCHAR(100),
	Quantity int,
	Image int,
	CreationDate date,
	Eliminated bit,
	BrandID int,
	CategoryID int,
	PRIMARY KEY(ProductID),
	FOREIGN KEY (BrandID) REFERENCES Brands(BrandID),
	FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Carts(
	CartID int IDENTITY,
	UserID int NOT NULL,
	PRIMARY KEY(CartID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID),
);

CREATE TABLE ItemsCarts(
	ItemCartID int IDENTITY,
	Quantity int NOT NULL,
	ProductID int NOT NULL,
	CartID int NOT NULL,
	PRIMARY KEY(ItemCartID),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	FOREIGN KEY (CartID) REFERENCES Carts(CartID)
);

CREATE TABLE Orders(
	OrderID int IDENTITY,
	UserID int NOT NULL,
	OrderStatusID int NOT NULL,
	AdressID int NOT NULL,
	PRIMARY KEY(OrderID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID),
	FOREIGN KEY (OrderStatusID) REFERENCES OrderStatuses(OrderStatusID),
	FOREIGN KEY (AdressID) REFERENCES Adresses(AdressID)
);

CREATE TABLE ItemsOrders(
	ItemOrderID int IDENTITY,
	Quantity int NOT NULL,
	ProductID int NOT NULL,
	OrderID int NOT NULL,
	PRIMARY KEY(ItemOrderID),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);


select * from Roles;
select * from Addresses;
select * from Users;

--Delete From Users Where UserID = 2;

-- Inner Join con informacion general de Usuario y sus tablas foraneas
Select U.UserID,U.Email,U.Password,U.FirstName,U.SurName,U.DateOfBirth,D.Name,D.Number,D.Floor,D.ApartmentNumber,R.RoleID,R.Name 
From Users as U
Inner Join Roles as R
On U.RoleID = R.RoleID
Inner Join Addresses as D
On D.AddressID = U.AddressID;

--Productos

select * from Brands;
select * from Products;
select * from Categories;

-- Inner Join con informacion general de Productos y sus tablas foraneas
Select P.ProductID,P.Title,P.Price,P.Quantity,P.CreationDate,B.BrandID,B.Name,C.CategoryID,C.Name
From Products as P
Inner Join Brands as B
On P.BrandID = B.BrandID
Inner Join Categories as C
On P.CategoryID = C.CategoryID;

select * from OrderStatuses
Order by NextStatusOrderID;

--Order
Select * from Orders;
Select * from ItemsOrders;
Select * from OrderStatuses;

-- Inner Join que muestra todos los productos que estan dentro del Pedido y sus datos
Select O.OrderID,O.AddressID,IO.ItemOrderID,IO.ProductID,P.Title,P.Price,P.Quantity,P.CreationDate,B.BrandID,B.Name,IO.Quantity,OS.OrderStatusID,OS.Name
from Orders as O
Inner Join OrderStatuses as OS
On O.OrderStatusID = OS.OrderStatusID
Inner Join ItemsOrders as IO
On O.OrderID = IO.OrderID
Inner Join Products as P
On IO.ProductID = P.ProductID
Inner Join Brands as B
On P.BrandID = B.BrandID;

--Cart
Select * from Carts;
Select * from ItemsCarts;

-- Inner Join que muestra todos los productos que estan dentro de los Carts y sus datos
Select C.CartID,C.UserID,IC.ItemCartID,IC.ProductID,P.Title,P.Price,B.BrandID,B.Name,IC.Quantity
from Carts as C
Inner Join ItemsCarts as IC
On C.CartID = IC.CartID
Inner Join Products as P
On IC.ProductID = P.ProductID
Inner Join Brands as B
On P.BrandID = B.BrandID;