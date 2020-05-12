CREATE TABLE vsisp38.apartment (
	id varchar NOT NULL,
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
    agentId varchar NULL,
	CONSTRAINT apartment_pk PRIMARY KEY (id)
);

CREATE TABLE vsisp38.estate_agent (
    id varchar NOT NULL,
	login varchar(255) NOT NULL,
	name varchar(255) NULL,
	address varchar(255) NULL,
	password varchar(255) NULL,
	CONSTRAINT estate_agent_pk PRIMARY KEY (id)
);

CREATE TABLE vsisp38.house (
	id varchar NOT NULL,
	city varchar NULL,
	postal_code varchar(5) NULL,
	street varchar NULL,
	street_no varchar NULL,
	square_area float8 NULL,
	floors int4 NULL,
	price float8 NULL,
	garden bool NULL,
	agentId varchar NULL
);

CREATE TABLE dis2020.person (
    id varchar NOT NULL,
	last_name varchar NULL,
	first_name varchar NULL,
	address varchar NULL
);