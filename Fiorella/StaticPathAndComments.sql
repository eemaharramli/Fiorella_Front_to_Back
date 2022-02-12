                        /*       Add Slider Images        */

select * from SliderImages;

insert into SliderImages values ('h3-slider-background.jpg'),
                                ('h3-slider-background-2.jpg'),
                                ('h3-slider-background-3.jpg');


                        /*       Add Slider Title Information        */

select * from Sliders;

insert into Sliders values ('<h1>Send <span>flowers</span> like</h1>
                            <h1>you mean it</h1>', 'Where flowers are our inspiration to create lasting memories. Whatever the occasion, our flowers will
                                make it special cursus a sit amet mauris.', 'h2-sign-img.png');


                        /*       Add Categories Information        */

select * from Categories;

insert into Categories values ('Popular'),
                              ('Winter'),
                              ('Various'),
                              ('Exotic'),
                              ('Greens'),
                              ('Cactuses');


                        /*       Add Slider Title Information        */


select * from Products;

insert into Products values ('MAJESTY PALM - 1', 259, 'shop-14-img.jpg', 1),
                            ('MAJESTY PALM - 2', 259, 'shop-13-img.jpg', 1),
                            ('MAJESTY PALM - 3', 259, 'shop-12-img.jpg', 2),
                            ('MAJESTY PALM - 4', 259, 'shop-11-img.jpg', 2),
                            ('MAJESTY PALM - 5', 259, 'shop-10-img.jpg', 2),
                            ('MAJESTY PALM - 6', 259, 'shop-9-img.jpg', 2),
                            ('MAJESTY PALM - 7', 259, 'shop-8-img.jpg', 2),
                            ('MAJESTY PALM - 8', 259, 'shop-7-img.jpg', 2);

update Products
set CategoryId = 3
where Image in('shop-10-img.jpg', 'shop-9-img.jpg');

update Products
set CategoryId = 4
where Image in('shop-8-img.jpg', 'shop-7-img.jpg');

update Products
set Name = 'MAJESTY PALM - 16'
where Id = 16

                        /*       Add Reasons Information        */

select * from Reasons;

insert into Reasons values ('Hand picked just for you.'),
                           ('Unique flower arrangements'),
                           ('Best way to say you care.');

                        /*       Add About Information        */

select * from Abouts;

insert into Abouts(Title, Subtitle, ReasonId) values ('Suprise Your <span>Valentine!</span> Let us arrange a smile.', 'Where flowers are our inspiration to create lasting memories. Whatever the occasion...', 1);


                        /*       Add Experts Jobs Information        */

select * from ExpertJobs;


insert into ExpertJobs values ('Florist'),
                              ('Manager');


                        /*       Add Experts Information        */

select * from Experts;

insert into Experts(Fullname, Image, JobId) values('CRYSTAL BROOKS', 'h3-team-img-1.png', 1, 1),
                                                   ('SHIRLEY HARRIS', 'h3-team-img-2.png', 2, 2),
                                                   ('BEVERLY CLARK', 'h3-team-img-3.png', 1, 1),
                                                   ('AMANDA WATKINS', 'h3-team-img-4.png', 1, 1);


                        /*       Add Blogs Information        */

select * from Blogs;

insert into Blogs values ('Flower Power', 'Class aptent taciti sociosqu ad litora torquent per conubia nostra, per', 'blog-feature-img-1.jpg', '12.29.2019'),
                         ('Local Florists', 'Class aptent taciti sociosqu ad litora torquent per conubia nostra, per', 'blog-feature-img-3.jpg', '12.29.2019'),
                         ('Flower Power', 'Class aptent taciti sociosqu ad litora torquent per conubia nostra, per', 'blog-feature-img-4.jpg', '12.29.2019');

                        /*       Add Blogs Information        */

select * from Things;

insert into Things values ('testimonial-img-1.png', 'Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus lingua.', 'Anna Barnes', 'Florist'),
                          ('testimonial-img-2.png', 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget.', 'Jasmine White', 'Florist');


                        /*       Add Instagram Images        */

select * from InstagramImages;

insert into InstagramImages values ('instagram1.jpg'),
                                   ('instagram2.jpg'),
                                   ('instagram3.jpg'),
                                   ('instagram4.jpg'),
                                   ('instagram5.jpg'),
                                   ('instagram6.jpg'),
                                   ('instagram7.jpg'),
                                   ('instagram8.jpg');


                        /*       Add Subscribe Image        */

select * from Subscribes;

insert into Subscribes values ('h3-background-img.jpg');


                        /*       Add more data to Products Table        */

select * from Products;

insert into Products values ('MAJESTY PALM', 259, 'shop-14-img.jpg', 1),
                            ('MAJESTY PALM', 259, 'shop-13-img.jpg', 1),
                            ('MAJESTY PALM', 259, 'shop-12-img.jpg', 2),
                            ('MAJESTY PALM', 259, 'shop-11-img.jpg', 2),
                            ('MAJESTY PALM', 259, 'shop-10-img.jpg', 3),
                            ('MAJESTY PALM', 259, 'shop-9-img.jpg', 3),
                            ('MAJESTY PALM', 259, 'shop-8-img.jpg', 4),
                            ('MAJESTY PALM', 259, 'shop-7-img.jpg', 4);


                        /*       Add Data to Bios Table        */

select * from Bios;

insert into Bios values ('logo.png', 'https://www.linkedin.com/', 'https://www.facebook.com/');


                        /*       Add Data to Description Column in Categories Table        */

select * from Categories;

update Categories
set
Description = 'Lorem ipsum';


select *
from Products;