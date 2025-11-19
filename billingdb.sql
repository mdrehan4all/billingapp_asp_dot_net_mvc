create database billingdb;

use billingdb;

CREATE TABLE users(
	id int identity(0, 1) constraint pk_id primary key,
	email varchar(256) unique,
	password varchar(256),
	token varchar(256),
	name varchar(256)
);

CREATE TABLE products(
	id int identity(0, 1) constraint pk_pid primary key,
	name varchar(256),
	description varchar(1024),
	price DECIMAL(10, 2),
	stock int
);

INSERT INTO users (email, password, token, name)values('mdrehan4all@gmail.com', 'abcd', 'xxx', 'Md Rehan');
INSERT INTO users (email, password, token, name)values('tester@gmail.com', 'tester', 'xxx', 'Tester');

INSERT INTO products (name, description, price, stock)values('MI TV', '31 inch tv', 4000.00, 10);
INSERT INTO products (name, description, price, stock)values('Onida TV', '34 inch tv', 6000.00, 10);


SELECT * FROM users;
SELECT * FROM products;