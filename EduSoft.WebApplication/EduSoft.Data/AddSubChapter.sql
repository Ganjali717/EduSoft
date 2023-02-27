 
DECLARE @Counter INT 
SET @Counter=0
WHILE ( @Counter < 4)
BEGIN
  SET @Counter = @Counter + 1
  DECLARE @guid uniqueidentifier = NEWID() SELECT @guid as 'GUID'
  INSERT INTO Subchapters (Id,Title,Content,ChapterId,Created)
  VALUES 
  (@guid,'Introduction to Java FX','Some','42aaef9f-7f1b-4385-97bd-6206f84ccdea',GETDATE())
END
 
 





