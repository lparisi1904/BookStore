CREATE DATABASE BookStore 


USE BookStore 


CREATE TABLE Book 
  ( 
     Id BIGINT   IDENTITY (1,1), 
     Title       NVARCHAR(100) NOT NULL, 
     Author      NVARCHAR(50) NOT NULL,
     Description NVARCHAR(100) NOT NULL, 
     CategoryId  BIGINT NOT NULL, 
     PublisherId BIGINT NOT NULL, 
     PRIMARY KEY (Id), 
     FOREIGN KEY (CategoryId)  REFERENCES Category(Id), 
  )

-- 1:N Book
CREATE TABLE Category 
  ( 
     Id   BIGINT IDENTITY(1, 1) NOT NULL, 
     NAME NVARCHAR(50) NOT NULL, 
     PRIMARY KEY (Id) 
  ) 

--------------- DA IMPLEMENTARE ----------------

-- BookAuthors : N:N
CREATE TABLE Author
  ( 
     Id   BIGINT IDENTITY(1, 1) NOT NULL, 
     NAME NVARCHAR(50) NOT NULL, 
     PRIMARY KEY (Id) 
  ) 

-- Book : N:1
CREATE TABLE Publisher 
  ( 
     Id   BIGINT IDENTITY(1, 1) NOT NULL, 
     NAME NVARCHAR(100) NOT NULL, 
     PRIMARY KEY (Id) 
  ) 

-- 1:1 Book
CREATE TABLE BookAuthors 
  ( 
     BookId   BIGINT NOT NULL, 
     AuthorId BIGINT NOT NULL 
     PRIMARY KEY (BookId, AuthorId), 
     FOREIGN KEY (BookId) REFERENCES Book(Id), 
     FOREIGN KEY (AuthorId) REFERENCES Author(Id) 
  )


CREATE TABLE AuthorContact 
  ( 
     AuthorId      BIGINT NOT NULL, 
     ContactNumber NVARCHAR(15) NULL, 
     Address       NVARCHAR(100) NULL, 
     PRIMARY KEY (AuthorId), 
     FOREIGN KEY (AuthorId) REFERENCES Author(Id) 
  ) 