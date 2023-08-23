create database Delivery_v3
go

Use Delivery_v3
go

create type names from nvarchar(30)
create type id from int
go

create table Roles(role_id id primary key not null Check(role_id > 0),
                   rolename names not null)
				   go

create table Users(users_id id primary key not null Check(users_id > 0),
                   u_firstname names not null,
				   u_lastname names not null,
				   email names not null,
				   user_password varchar(15) not null)
				   go

create table UserRole(users_id id not null Check(users_id > 0),
                      role_id id not null Check(role_id > 0),
					  Primary key (users_id, role_id),
					  Foreign key (users_id) references Users(users_id),
					  Foreign key (role_id) references Roles(role_id))
					  go

create table Customer(customer_id id primary key not null Check(customer_id > 0),
                      c_firstname names not null,
					  c_lastname names not null)
					  go

							

create table Adres(adress_id id primary key not null Check(adress_id > 0),
                    house smallint not null,
					street names not null,
					city names not null,
					postalcode varchar(10))
					go

create table CustomerAdress(customer_id id not null Check(customer_id > 0),
                            adress_id id not null Check(adress_id > 0),
							Primary Key (customer_id, adress_id),
							Foreign Key (customer_id) references Customer(customer_id),
							Foreign Key (adress_id) references Adres(adress_id))
							go

create table Order_status(status_id id primary key not null Check(status_id > 0),
                          order_status nvarchar(10) not null)
						  go

create table Restaurant(restaurant_id id primary key not null Check(restaurant_id > 0),
                        restaurant_name names not null,
						adress_id id foreign key references Adres(adress_id) not null)
						go

create table FoodCategory(category_id id primary key not null Check(category_id > 0),
						  food_name names not null,
						  restaurant_id id foreign key references Restaurant(restaurant_id) not null)
						  go

create table Fooditem(item_id id primary key not null Check(item_id > 0),
                      item_name names not null,
					  item_price Decimal(6, 2) not null Check(item_price > 0),
					  category_id id foreign key references FoodCategory(category_id) not null)
					  go

create table Restaurant_Item(restaurant_id id foreign key references Restaurant(restaurant_id) not null,
                             item_id id foreign key references Fooditem(item_id) not null)
							 go

create table Food_order(Food_order_id id primary key not null Check(Food_order_id > 0),
                        customer_id id foreign key references Customer(customer_id) not null,
						adress_id id foreign key references Adres(adress_id) not null,
						driver_id id not null,
						status_id id foreign key references Order_status(status_id),
						restaurant_id id foreign key references Restaurant(restaurant_id) not null,
						deliveryfee  smallint,
						totalamount Decimal(6, 2) not null Check(totalamount > 0),
						order_datetime datetime not null,
						req_datetime datetime not null,
						FOREIGN KEY (driver_id) REFERENCES Users(users_id))
						go


create table FoodOrderItem(Food_order_id id not null,
                           item_id id not null,
						   item_price Decimal(6, 2) not null Check(item_price > 0),
						   quanity smallint not null Check(quanity > 0),
						   Primary Key (Food_order_id, item_id),
						   Foreign Key (Food_order_id) references Food_order(Food_order_id),
						   Foreign Key (item_id) references Fooditem(item_id))
						   go



INSERT INTO Roles (role_id, rolename)
VALUES
(1, 'Admin'),
(2, 'User'),
(3, 'Manager'),
(4, 'Guest'),
(5, 'Driver');


INSERT INTO Users (users_id, u_firstname, u_lastname, email, user_password)
VALUES
(1, 'John', 'Doe', 'johndoe@example.com', 'password123'),
(2, 'Jane', 'Smith', 'janesmith@example.com', 'abc123'),
(3, 'Michael', 'Johnson', 'michaeljohnson@example.com', 'qwerty'),
(4, 'Emily', 'Brown', 'emilybrown@example.com', '123456'),
(5, 'David', 'Miller', 'davidmiller@example.com', 'pass123'),
(6, 'Olivia', 'Wilson', 'oliviawilson@example.com', 'password'),
(7, 'James', 'Taylor', 'jamestaylor@example.com', 'qwerty123');


INSERT INTO UserRole (users_id, role_id)
VALUES
(1, 1),
(2, 5),
(3, 5),
(4, 5),
(5, 2),
(6, 4),
(7, 2);


INSERT INTO Customer (customer_id, c_firstname, c_lastname)
VALUES
(1, 'Emma', 'Johnson'),
(2, 'Liam', 'Smith'),
(3, 'Olivia', 'Davis'),
(4, 'Noah', 'Anderson'),
(5, 'Ava', 'Martinez'),
(6, 'Sophia', 'Wilson'),
(7, 'William', 'Taylor');


INSERT INTO Adres (adress_id, house, street, city, postalcode)
VALUES
(1, 10, 'Main Street', 'New York', '10001'),
(2, 5, 'First Avenue', 'Los Angeles', '90001'),
(3, 20, 'Park Road', 'Chicago', '60601'),
(4, 8, 'Oak Street', 'Houston', '77001'),
(5, 15, 'Elm Avenue', 'Dallas', '75201'),
(6, 12, 'Cedar Lane', 'Miami', '33101'),
(7, 25, 'Maple Drive', 'San Francisco', '94101');


INSERT INTO CustomerAdress (customer_id, adress_id)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4);


INSERT INTO Order_status (status_id, order_status)
VALUES
(1, 'Pending'),
(2, 'Processing'),
(3, 'Completed'),
(4, 'Cancelled'),
(5, 'On Hold'),
(6, 'Delivered'),
(7, 'Returned');


INSERT INTO Restaurant
VALUES
(1, 'Italian Delights', 5),
(2, 'Taste of India', 6),
(3, 'Mexican Fiesta', 7);


INSERT INTO FoodCategory (category_id, food_name, restaurant_id)
VALUES
(1, 'Pizza', 1),
(2, 'Pasta', 1),
(3, 'Curry', 2),
(4, 'Tacos', 3),
(5, 'Sushi', 3),
(6, 'Burgers', 2),
(7, 'Salads', 2);


INSERT INTO Fooditem (item_id, item_name, item_price, category_id)
VALUES
(1, 'Margherita', 9.99, 1),
(2, 'Pepperoni', 10.99, 1),
(3, 'Alfredo', 12.99, 2),
(4, 'Masala', 11.99, 3),
(5, 'Carnitas', 8.99, 4),
(6, 'California Roll', 14.99, 5),
(7, 'Cheeseburger', 9.99, 6);

Insert Into Restaurant_Item
Values (1, 1), (1, 2), (1, 3), (2, 1), (2, 2), (1, 4), (3, 2), (3, 1), (3, 3), (3, 7), (2, 6), (1, 5), (3, 5), (1, 7);

INSERT INTO Food_order (Food_order_id, customer_id, adress_id, driver_id, status_id, restaurant_id, deliveryfee, totalamount, order_datetime, req_datetime)
VALUES
(1, 1, 1, 4, 2, 1, 3, 25.99, '2023-06-28T18:30:00', '2023-06-28T18:00:00'),
(2, 2, 2, 3, 1, 2, 2, 18.50, '2023-06-29T12:15:00', '2023-06-29T11:30:00'),
(3, 3, 3, 2, 6, 3, 4, 35.75, '2023-06-29T20:45:00', '2023-06-29T20:00:00'),
(4, 4, 4, 2, 3, 2, 3, 28.99, '2023-06-29T17:30:00', '2023-06-29T17:00:00'),
(5, 5, 5, 3, 4, 3, 2, 15.99, '2023-06-29T19:00:00', '2023-06-29T18:30:00'),
(6, 6, 6, 4, 5, 1, 3, 29.50, '2023-06-29T14:20:00', '2023-06-29T13:45:00'),
(7, 7, 7, 5, 6, 1, 4, 38.25, '2023-06-29T21:30:00', '2023-06-29T21:00:00');



INSERT INTO FoodOrderItem (Food_order_id, item_id, item_price, quanity)
VALUES
(1, 1, 9.99, 2),
(1, 3, 12.99, 1),
(2, 6, 14.99, 1),
(3, 4, 11.99, 3),
(4, 7, 9.99, 2),
(5, 2, 10.99, 1),
(6, 5, 8.99, 2),
(7, 1, 9.99, 1)
go

Create view Menu AS
Select f.restaurant_id, fi.item_id, fi.item_name, fi.item_price
From Fooditem fi INNER JOIN Restaurant_Item f
ON f.restaurant_id = f.restaurant_id
go

use Delivery_v3

Create View User_role AS
Select u.users_id, u.email, u.u_firstname, u.u_lastname, u.user_password, ur.role_id 
From Users u INNER JOIN UserRole ur on u.users_id = ur.users_id
go

Select * From UsersData

Create View UsersData AS
Select u.users_id, u.email, u_firstname, u_lastname, user_password, r.role_id, r.rolename
From User_role u INNER JOIN Roles r ON u.role_id = r.role_id
go