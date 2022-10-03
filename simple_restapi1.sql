use aspnet_simple_restapi;

--create table Users(Id uniqueidentifier primary key, Email varchar(32) not null, Password varchar(32) not null, Address varchar(256), RegisterDate datetime not null, Gender int, Role int not null);
--create table Products(Id uniqueidentifier primary key, Name varchar(32) not null, Category varchar(32), Unit int not null, Quantity int not null, Price float not null)
--create table OrderDetail(Id uniqueidentifier primary key, UserId uniqueidentifier, OrderDate datetime, Total float, IsConfirmed bit, foreign key(UserId) references Users(Id));

--create table OrderProduct(OrderDetailId uniqueidentifier not null, ProductId uniqueidentifier not null, foreign key(OrderDetailId) references OrderDetail(Id), foreign key(ProductId) references Products(Id));
--create table Payment(Id uniqueidentifier primary key, UserId uniqueidentifier, PaymentType int not null, Balance float not null, foreign key (UserId) references Users(Id));

create table Albums(Id uniqueidentifier primary key, UserId uniqueidentifier not null, Name varchar(32) not null, foreign key(UserId) references Users(Id));
create table Photos(Id uniqueidentifier primary key, AlbumId uniqueidentifier not null, FileName varchar(32), Path varchar(256), foreign key(AlbumId) references Albums(Id));

alter table OrderDetail alter column IsConfirmed bit not null;
--alter table Users alter column Gender varchar(32) not null;
alter table OrderProduct add Id uniqueidentifier primary key;

select * from Users;
select * from Albums;
select * from Photos;

select * from OrderDetail;
select * from OrderProduct;
select * from Users;
select * from Products;
select * from Payment;

delete from Payment where Id = '3FA85F64-5717-4562-B3FC-2C963F66AFA6';

