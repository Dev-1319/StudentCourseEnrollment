SELECT TOP (1000) [Id]
      ,[Title]
      ,[Credits]
  FROM [StudentEnrollmentDb].[dbo].[Courses]
  ;

  SELECT *
  FROM [StudentEnrollmentDb].[dbo].[Students]
  ;


  SELECT *
  FROM [StudentEnrollmentDb].[dbo].Enrollments
  ;


  USE [StudentEnrollmentDb]
GO

INSERT INTO [dbo].[Students]
           ([Name])
     VALUES
           ('Test1')
GO

BEGIN TRANSACTION

commit;


  USE [StudentEnrollmentDb]
GO

INSERT INTO [dbo].[Courses]
		   (Title, Credits)
	 VALUES
		   ('Computer Science',5)
GO

BEGIN TRANSACTION

commit;
