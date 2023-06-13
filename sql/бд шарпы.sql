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
