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
begin transaction
--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	name varchar (50) not null
	CONSTRAINT PK_user PRIMARY KEY (user_id)
);

CREATE TABLE breweries (
	brewery_id int IDENTITY(1,1) NOT NULL,
	name varchar(50) NOT NULL,
	brewer_id int NOT NULL,
	street_address1 nvarchar(100)not null,
	street_address2 nvarchar(50) null,
	city varchar (50) not null,
	state varchar (2) not null,
	zip int not null,
	phone varchar (15) not null,
	history nvarchar(300) not null,
	hours_of_operation varchar(300) not null,
	website varchar(30) not null,
	brewery_status_id int not null	
	CONSTRAINT PK_brewery PRIMARY KEY (brewery_id)
);
CREATE TABLE brewery_images(
	brewery_img_id int IDENTITY(1,1),
	brewery_id int not null,
	brewery_img_path varchar(max),
	Constraint PK_brewery_images PRIMARY KEY (brewery_img_id)
);

Create Table brewery_status_id (
	brewery_status_id int IDENTITY(1,1),
	brewery_status_desc varchar(10) not null
	Constraint PK_brewery_status PRIMARY KEY (brewery_status_id)
);
CREATE TABLE users_favBreweries (
	user_id int NOT NULL,
	brewery_id int NOT NULL
	CONSTRAINT PK_users_breweries PRIMARY KEY (user_id, brewery_id)
);

CREATE TABLE beers (
	beer_id int IDENTITY(1,1) NOT NULL,
	brewery_id int NOT NULL,
	beer_type_id int NOT NULL,
	name varchar(50) not null,
	abv decimal(4,2) NOT NULL,
	description varchar(500) NOT NULL,
	ingredients varchar(200) NOT NULL,
	isActive bit not null
	CONSTRAINT PK_beer_id PRIMARY KEY (beer_id)
);

CREATE TABLE beer_images(
	beer_img_id int IDENTITY (1,1),
	beer_id int not null,
	beer_img_path varchar(50) not null
	CONSTRAINT PK_beer_images PRIMARY KEY (beer_img_id)
);

CREATE TABLE beer_types (
beer_type_id int IDENTITY(1,1) NOT NULL,
beer_type varchar(75) NOT NULL
CONSTRAINT PK_beer_type PRIMARY KEY (beer_type_id)
);

CREATE TABLE brewery_reviews (
brewery_review_id int IDENTITY(1,1) NOT NULL,
brewery_id int not null,
user_id int not null,
title varchar(100) NOT NULL,
review varchar(300) NOT NULL,
rating int NOT NULL,
is_private bit Not Null
CONSTRAINT PK_brewery_review_id PRIMARY KEY (brewery_review_id)
);

CREATE TABLE beer_reviews(
beerReview_id int IDENTITY (1,1) not null,
beer_id int not null,
user_id int not null,
beerRating int not null,
title varchar(100) NOT NULL,
beerReview varchar(300) not null,
is_private bit Not Null
CONSTRAINT PK_beerReview_id PRIMARY KEY (beerReview_id)
);
ALTER TABLE breweries ADD CONSTRAINT fk_brewer_id FOREIGN KEY (brewer_id) REFERENCES users(user_id);
ALTER TABLE brewery_images ADD CONSTRAINT fk_brewery_img_id FOREIGN KEY (brewery_id) REFERENCES breweries(brewery_id);

ALTER TABLE users_favBreweries ADD CONSTRAINT fk_users_favBreweries_users FOREIGN KEY (user_id) REFERENCES users(user_id);

ALTER TABLE users_favBreweries ADD CONSTRAINT fk_users_favBreweries_brewery FOREIGN KEY (brewery_id) REFERENCES breweries(brewery_id);

ALTER TABLE beers ADD CONSTRAINT fk_brewery_id FOREIGN KEY (brewery_id) REFERENCES breweries(brewery_id);
ALTER TABLE beer_images ADD CONSTRAINT fk_beer_id FOREIGN KEY (beer_id) REFERENCES beers(beer_id);

ALTER TABLE beers ADD CONSTRAINT fk_beer_type_id FOREIGN KEY (beer_type_id) REFERENCES beer_types(beer_type_id);
Alter Table beer_reviews ADD CONSTRAINT fk_beerReview_beer_id FOREIGN KEY (beer_id) REFERENCES beers(beer_id);
ALTER TABLE beer_reviews ADD CONSTRAINT fk_beerReview_user_id FOREIGN KEY (user_id) REFERENCES users(user_id);

ALTER TABLE brewery_reviews ADD CONSTRAINT fk_breweryReview_brewery_id FOREIGN KEY (brewery_id) REFERENCES breweries(brewery_id);
ALTER TABLE brewery_reviews ADD CONSTRAINT fk_breweryReview_user_id FOREIGN KEY (user_id) REFERENCES users(user_id);
ALTER TABLE  breweries ADD CONSTRAINT fk_brewery_status_id FOREIGN KEY (brewery_status_id) REFERENCES brewery_status_id(brewery_status_id);
commit transaction
--populate default data
INSERT INTO users (username, password_hash, salt, user_role, name) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','brewer', 'user');
INSERT INTO users (username, password_hash, salt, user_role, name) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 'admin');


INSERT INTO brewery_status_id (brewery_status_desc) values ('pending');
INSERT INTO brewery_status_id (brewery_status_desc) values ('active');
INSERT INTO brewery_status_id (brewery_status_desc) values ('inactive');

INSERT INTO breweries (name, brewer_id, street_address1, city, state, zip, phone, history, hours_of_operation, website, brewery_status_id) 
values ('Penn Brewery', 1, '800 Vinial Street', 'Pittsburgh', 'PA', '15212', '412-237-9400', 'Began brewing craft beer since 1986. We started brewing classic lagers and
German beer styles.  Our Restaurant serves Pittsburgh Native foods.  See our site for more details.', 'Weds-Sat: Noon - 10pm Sun: Noon - 9pm Closed Mon-Tues', 'https://www.pennbrew.com', 1);

INSERT INTO breweries (name, brewer_id, street_address1, city, state, zip, phone, history, hours_of_operation, website, brewery_status_id) 
values ('Southern Tier Brewing Co', 1, '316 N. Shore Drive', 'Pittsburgh', 'PA', '15212', '412-301-2337', '"Sothern Tier Brewering Companys first satellite brewpub.  
Were known for brewing world-class hoppy ales and decadent dessert beers alike, but we are known for the experience customers have when they visit.', 
'Mon-Wed: 3 - 10pm, Thur: 11 am - 10pm, fri-sat: 11am - 12am, sun 11am - 10pm"', 'https://taprooms.stbcbeer.com', 1);

Insert Into beer_types (beer_type) values('Ale');
Insert Into beer_types (beer_type) values('Lager');
Insert Into beer_types (beer_type) values('IPA');
Insert Into beer_types (beer_type) values('Stout');
Insert Into beer_types (beer_type) values('Pilsner');
Insert Into beer_types (beer_type) values('Porter');
Insert Into beer_types (beer_type) values('Wheat');

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Penn Pilsner', 5.0, 1, 5, 
'Our flagship beer. Amber-colored with a malty nose and a touch of Nobel hops, Penn Pilsner has caramel and toffee notes as 
well as toasted, nutty hints.  Penn Pilsner is a very well-rounded, balanced, and flavorful lager beer.', 'Hops: Hallertau Perle, Hallertau Tradition
Malt: Two-row, caramel', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Penn Dark', 5.0, 1, 2, 
'European-style Dark/Munchener Dunkel. Deep reddish-mahogany in color with sweet caramel malt, nutty and toffee notes, and roasted hints.
Penn Dark has a moderate hopping rate and a crisp, clean lager beer finish. A surprisingly smooth dark beer.', 'Hops: Perle, Malt: Two-row, Munich, Carafa', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Across the Spectrum IPA', 6.5, 2, 3, 
'Hazy & Juicy IPA','hops', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Pink Guava Milkshake IPA', 6.5, 2, 3, 
'IPA sweetened with lactose milk sugar and pink guava puree','hops, lactose milk sugar, pink guava puree', 1);

insert into brewery_images(brewery_id,brewery_img_path) values (1,'temp')
insert into brewery_images(brewery_id,brewery_img_path) values (2,'temp')

INSERT INTO breweries (name, brewer_id, street_address1, city, state, zip, phone, history, hours_of_operation, website, brewery_status_id) 
values ('Hitchhiker', 2, '190 Castle Shannon Blvd', 'Pittsburgh', 'PA', '15228', '412-343-1950', 'Paste your text here :)hitchhiker brewing co. Is devoted to refining time-honored styles and discovering new ones. We are focused on the evolution of our craft, exploring the unknown, and the excitement of introducing people to our beer.', 
'Tuesday-Friday: 4-9, Saturday: 12-9, Sunday, 12-6, Monday: Closed', 'https://hitchhiker.beer/', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('2XIPA', 8.2, 2, 3, 
'With our 2XIPA, a massive dry hop brings a feverishly hoppy and well-balanced Double IPA. Citrusy hops tease the senses with big aromatics and certifiable bitterness. An enormous haul of hops, including Simcoe and Citra, deliver notes of grapefruit, lemon and doughy sweetness. Double your expectations because this is an ale that demands reverence.', 'Five Varieties of hops and three varieties of malts', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('NU JUICE IPA', 6.0, 2, 3, 
'Sometimes evolution is observed in real time. Case in point: Nu Juice IPA. Over the course of more than a year of development, our R&D team uncovered the ultimate balance of hops and malts. This process involves adding Mosaic, Ekuanot, and Simcoe hops over a multi-day period, creating a refreshingly juicy and approachable IPA. As an added bonus, bitterness has been reduced to a low 30 for what could be the smoothest IPA ever brewed.', 'Mosaic, Ekuanot, and Simcoe hops', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('LIVE SESSION', 5.5, 2, 3, 
'Live Session is bright, refreshingly loud, and cracked up to 11 by adding generous amounts of Citra hops to reflect the feeling of live music. Live Session pairs great with toe-tapping or head-banging to your favorite band on stage.0', 'Citra hops', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Penn Gold', 5.0, 1, 2, 
'Our Munich Helles.Malty, bready, and sweet flavors come together with just enough hops to round out this beer. Pale golden in color, this lager is delicate and delicious.', 'Hops: Perle', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Penn Kaiser Pils', 5.0, 1, 5, 
'A Northern German Pils. A clean, crisp, light-bodied lager featuring a very healthy dose of Noble hops. Kaiser Pils uses pale two-row barley malt as a backbone and has a fine level of carbonation that produces a lovely white foam head.', 'Two row barley malt', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('The Barbarian', 7.5, 3, 3, 
'Brewed with Oats and Spelt. Hopped with Mosaic and El Dorado.', 'Oats and Spelt', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Bane of Existence', 6.6, 3, 3, 
'Brewed with oats. Hopped with Citra, and Simcoe. Notes of passion fruit, melon, stone fruit, and mango.', 'Oats, Cirta, Simcoe', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Fruit Rush - Blackberry', 4.8, 3, 5, 
'Brewed with Oats and Wheat. Conditioned on Lemon, Blackberry', 'Oats, wheat', 1);

INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES ('Mystic Delusion', 7.2, 3, 3, 
'Brewed with Oats, Wheat and Electrolyte Salt. Conditioned on Tangerine and Blood Orange. Hopped with Amarillo, Citra, and Mosaic.', 'Oats, Wheat, Electrolyte salt', 1);

insert into brewery_images(brewery_id,brewery_img_path) values (3,'temp')

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (5, 1, 5, 'Amazing', 'My go to beer for sipping at home or drinking with friends. Goes good with a cigarette and a little late night DVR recordings of Let’s Make a Deal.', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (5, 2, 4, 'Dangerous', 'Great beer but it’ll sneak up on you like a pickpocket in the shady outskirts of Paris. Next thing you know you’re lost in the dark with no identity.', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (6, 1, 5, 'Fantastic', 'Can’t seem to get enough of this stuff! Tastes great from the tap at a bar or in a bottle on your portch. My wife loves it and she doesn’t even drink beer.', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (7, 2, 4, 'pretty good', 'Not Southern Tiers best drink but I still keep coming back to it. Reminds me of summers as a teen in New Jersey.', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (8, 1, 3, 'meh', 'Not great not horrible. Just kind of meh', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (9, 1, 4, 'Thumbs Up', 'Pretty good for a Pilsner', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (1, 2, 4, 'Love it', 'Two thumbs up. 4 stars. 6 pack.', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (2, 1, 5, 'More than decent', 'Best beer Ive had all morning. ', 0)

INSERT INTO beer_reviews (beer_id, user_id, beerRating, title, beerReview, is_private) VALUES (3, 1, 3, 'TASTE IS GOOD', 'Taste is good AFTER TASTE IS NOT ', 0)


select * from beer_reviews
select * from beers



GO
