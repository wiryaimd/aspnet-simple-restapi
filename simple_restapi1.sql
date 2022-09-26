use aspnet_simple_restapi;

--create table Users(Id uniqueidentifier primary key, Email varchar(32) not null, Password varchar(32) not null, Address varchar(256), RegisterDate datetime not null, Gender int, Role int not null);
--create table Products(Id uniqueidentifier primary key, Name varchar(32) not null, Category varchar(32), Unit int not null, Quantity int not null, Price float not null)

create table OrderProduct(OrderId uniqueidentifier not null, ProductId uniqueidentifier not null, foreign key(ProductId) references Products(Id));

select * from Users;
select * from Products;