USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	email varchar(100) NOT NULL,
	organization_name varchar(200) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	has_one_time_password bit NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE proteins (
	protein_id int IDENTITY(1,1) NOT NULL,
	sequence_name varchar(500) NOT NULL,
	protein_sequence varchar(8000) NOT NULL,
	description varchar(8000) NOT NULL,
	format_type int NOT NULL,
	username varchar(200) NOT NULL,
	user_id int NOT NULL,
	sequence_1 varchar(8000),
	sequence_2 varchar(8000),
	sequence_3 varchar(8000),
	CONSTRAINT PK_protein PRIMARY KEY (protein_id),
	FOREIGN KEY (user_id) REFERENCES users(user_id)
)

CREATE TABLE homeview (
	view_id int IDENTITY(1,1) NOT NULL,
	name varchar(100) NOT NULL,
	header varchar(500) NOT NULL,
	body varchar(8000) NOT NULL, 
	active bit NOT NULL,
	image_source varchar(200) NOT NULL,
	CONSTRAINT view_id PRIMARY KEY(view_id)
) 

CREATE TABLE cells (
	cell_id int IDENTITY(1,1) NOT NULL,
	x_cord int NOT NULL,
	y_cord int NOT NULL,
	letter_x varchar(1) NOT NULL,
	letter_y varchar(1) NOT NULL,
	color varchar(10) NOT NULL,
	acid varchar(1) NOT NULL
)

--populate default data
INSERT INTO users (username, email, organization_name, password_hash, salt, user_role, has_one_time_password) VALUES ('user', 'dummy@email.net', 'Center for Charitable Contributions', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', 0);
INSERT INTO users (username, email, organization_name, password_hash, salt, user_role, has_one_time_password) VALUES ('admin','smart@email.com', 'Association For Protein Enrichment','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 0);
INSERT INTO users (username, email, organization_name, password_hash, salt, user_role, has_one_time_password) VALUES ('new user!', 'user@yahoo.com', 'new startup industries', 'SJwGoscVAqnsELPHPdMhq6tqYEM=', 'I7cqSJ3qfU4=', 'user', 0);
INSERT INTO users (username, email, organization_name, password_hash, salt, user_role, has_one_time_password) VALUES ('new admin', 'admin@gmail.com', 'administrative experts incorporated', '7aAwUxYHuUaz2iNz3SjHZHaZq88=', 'ydNeoqlGX9I=', 'admin', 0);


INSERT INTO homeview (header, body, active, image_source, name) VALUES ('Protein Capture Science', 'Amino Acid Sifter', 1, 'src/img/AminoAcid.jpg', 'Default')
INSERT INTO homeview (header, body, active, image_source, name) VALUES ('HO HO HO', 'Merry Christmas', 0, 'src/img/AminoAcid.jpg', 'Christmas')
INSERT INTO homeview (header, body, active, image_source, name) VALUES ('Software is a great combination between artistry and engineering', '-Bill Gates', 0, 'src/image/Empty', 'Quote')

INSERT INTO proteins (sequence_name, protein_sequence, description, format_type, username, user_id) 
VALUES ('Insulin', 'MALWMRLLPLLALLALWGPDPAAAFVNQHLCGSHLVEALYLVCGERGFFYTPKTRREAEDLQASALSLSSSTSTWPEGLDATARAPPALVVTANIGQAGGSSSRQFRQRALGTSDSPVLFIHCPGAAGTAQGLEYRGRRVTTELVWEEVDSSPQPQGSESLPAQPPAQPAPQPEPQQAREPSPEVSCCGLWPRRPQRSQN', 'This is an insulin protein!', 1, 'user', 1);

-- Note(anita): this is generated from table_gen.py found in the generator folder
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,0,'F','Y','blue','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,1,'F','F','blue','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,2,'F','H','blue','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,3,'F','L','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,4,'F','W','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,5,'F','I','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,6,'F','M','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,7,'F','Q','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,8,'F','C','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,9,'F','V','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,10,'F','T','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,11,'F','D','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,12,'F','E','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,13,'F','S','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,14,'F','R','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,15,'F','K','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,16,'F','A','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,17,'F','N','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,18,'F','G','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (1,19,'F','P','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (2,19,'H','P','red','J');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (5,6,'I','M','green','B');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (6,1,'M','F','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (6,6,'M','M','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (6,12,'M','E','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (6,15,'M','K','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (6,16,'M','A','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (6,19,'M','P','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (8,6,'C','M','green','C');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (9,6,'V','M','yellow','H');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (10,1,'T','F','green','A');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (10,2,'T','H','green','K');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (10,6,'T','M','green','A');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (11,6,'D','M','yellow','D');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (12,1,'E','F','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (12,6,'E','M','yellow','N');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (12,7,'E','Q','yellow','C');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (12,11,'E','D','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (12,15,'E','K','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (12,16,'E','A','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (12,19,'E','P','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (13,6,'S','M','yellow','L');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (15,1,'K','F','green','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (15,6,'K','M','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (15,11,'K','D','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (15,15,'K','K','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (15,16,'K','A','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (15,19,'K','P','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (16,1,'A','F','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (16,6,'A','M','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (16,11,'A','D','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (16,15,'A','K','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (16,16,'A','A','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (16,19,'A','P','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (18,6,'G','M','yellow','F');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (18,16,'G','A','red','F');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (19,1,'P','F','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (19,6,'P','M','yellow','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (19,11,'P','D','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (19,15,'P','K','red','X');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (19,16,'P','A','red','G');
INSERT INTO cells (x_cord, y_cord, letter_x, letter_y, color, acid) VALUES (19,19,'P','P','red','X');


GO