## A test task from Fogsoft corp.
##### Description
This program reads vacancies from https://yaroslavl.hh.ru/catalog/Prodazhi/ and adds to the database those whose salaries do not differ by more than 10% from the average. 

##### Usage
In the application window, select from which page you want to collect vacancies and click "Start". To abort the operation, click "Abort".

##### Files
#
File name       | Contains
----------------|----------------------
DBdump.sql      | Dump file for Database in PostreSQL
Program.cs      | Contains Main() function
Vacancy.cs      | Class for vacancies
DBWorker.cs     | Contains methods for database
Form1.cs        | Windows Form
Parser          | Folder for parser classes

