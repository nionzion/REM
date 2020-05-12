
CREATE TABLE vsisp38.estate_agent (
    id SERIAL PRIMARY KEY,
	login varchar(255) NOT NULL,
	name varchar(255) NULL,
	address varchar(255) NULL,
	password varchar(255) NULL
);

CREATE TABLE vsisp38.apartment (
	id SERIAL PRIMARY KEY,
	city varchar NULL,
	postal_code varchar(5) NULL,
	street varchar NULL,
	street_no varchar NULL,
	square_area float8 NULL,
	floor varchar NULL,
	rent float8 NULL,
	rooms float8 NULL,
	balcony bool NULL,
	kitchen bool NULL,
    agentId int4 NULL REFERENCES vsisp38.estate_agent (id)
);

CREATE TABLE vsisp38.house (
	id SERIAL PRIMARY KEY,
	city varchar NULL,
	postal_code varchar(5) NULL,
	street varchar NULL,
	street_no varchar NULL,
	square_area float8 NULL,
	floors int4 NULL,
	price float8 NULL,
	garden bool NULL,
    agentId int4 NULL REFERENCES vsisp38.estate_agent (id)
);

CREATE TABLE vsisp38.person (
    id SERIAL PRIMARY KEY,
	last_name varchar NULL,
	first_name varchar NULL,
	address varchar NULL
);


CREATE TABLE vsisp38.tenancycontract (
	id SERIAL PRIMARY KEY,
	placementdate date NULL,
	place varchar NULL,
	startdate date NULL,
	duration int4 NULL,
	additional_costs float8 NULL,
    personId int4 NULL REFERENCES vsisp38.person (id),
    apartmentId int4 UNIQUE NULL REFERENCES vsisp38.apartment (id)
);

CREATE TABLE vsisp38.purchasecontract (
	id SERIAL PRIMARY KEY,
	placementdate date NULL,
	place varchar NULL,
	installments int4 NULL,
	interest_rate float8 NULL,
    personId int4 NULL REFERENCES vsisp38.person (id),
    houseId int4 UNIQUE NULL REFERENCES vsisp38.house (id)
);

INSERT INTO vsisp38.estate_agent (login,"name",address,"password") VALUES 
('root','root','root','root');
INSERT INTO vsisp38.estate_agent (login,"name",address,"password") VALUES 
('admin','Administrator','root','1234');

INSERT INTO vsisp38.person (last_name,first_name,address) VALUES 
('Snow','Jon','Winterfell');
INSERT INTO vsisp38.person (last_name,first_name,address) VALUES 
('Lennister','Cersei','Kings Landing');

INSERT INTO vsisp38.apartment (city,postal_code,street,street_no,square_area,floor,rent,rooms,balcony,kitchen,agentid) VALUES 
('Beyond the Wall','93746','Crasters Keep','1',56,'2',420,2,false,true,1);

INSERT INTO vsisp38.house (city,postal_code,street,street_no,square_area,floors,price,garden,agentid) VALUES 
('Kings Landing','28821','Red Keep','4',500,4,600000,false,1)
;
INSERT INTO vsisp38.purchasecontract (placementdate,place,installments,interest_rate,personid,houseid) VALUES 
('2020-05-28','Westeros',2,3,2,1);

INSERT INTO vsisp38.tenancycontract (placementdate,place,startdate,duration,additional_costs,personid,apartmentid) VALUES 
('2020-05-13','Qarth','2020-05-13',6,50,1,1);