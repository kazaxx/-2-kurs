CREATE DATABASE test
use test

create table register
(
id_user int identity(1,1) NOT NULL,
login_user varchar(50) NOT NULL,
password_user varchar(50) NOT NULL,
roll_user varchar(50)
);

Select * from register

create table tovar 
(
id_tovar int identity(1,1) NOT NULL,
brand_tovar varchar(50) NOT NULL,
model_tovar varchar(50) NOT NULL,
sum_tovar varchar(50) NOT NULL
);