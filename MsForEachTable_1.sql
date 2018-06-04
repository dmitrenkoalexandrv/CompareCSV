USE TSQL2012
Go

USE TSQL2012
Go
IF OBJECT_ID('tempdb..#ResultTable') IS NOT NULL DROP TABLE #ResultTable
IF OBJECT_ID('tempdb..#TableNames') IS NOT NULL DROP TABLE #TableNames

CREATE TABLE #ResultTable
(
	Id	INT Identity (1,1),
	EntityId XML,
	ObjectName Varchar(250)
)

CREATE TABLE #TableNames
(
   TableName Varchar(50)
)
  
DECLARE @sql NVARCHAR(2000) = '	DECLARE @colname Varchar(100)

								SELECT @colname =	CASE	WHEN EXISTS (SELECT 1 FROM SYS.COLUMNS C WHERE c.OBJECT_ID = OBJECT_ID(''?'') AND C.NAME = ''empid'') 
															THEN ''empId''
															WHEN EXISTS (SELECT 1 FROM SYS.COLUMNS C WHERE c.OBJECT_ID = OBJECT_ID(''?'') AND C.NAME = ''custId'') 
															THEN ''custId''
															WHEN EXISTS (SELECT 1 FROM SYS.COLUMNS C WHERE c.OBJECT_ID = OBJECT_ID(''?'') AND C.NAME = ''shipperid'') 
															THEN ''shipperid''
													END

								INSERT INTO #ResultTable(EntityId,ObjectName)
								EXEC(''
									SElECT STUFF((SELECT '''','''' + convert(varchar(10),e.'' + @colname + '')
									FROM ? e 
									LEFT JOIN Sales.Orders orders 
										ON e.'' + @colname + '' = orders.'' + @colname + ''
									GROUP BY e.'' + @colname + ''
									ORDER BY e.'' + @colname + ''
									FOR XML PATH ('''''''')),1,1,''''''''),''''?''''
								'')'


INSERT INTO #TableNames
SELECT OBJECT_SCHEMA_NAME(o.object_id)  + '.' + [Name] FROM sys.Objects o WHERE o.[Name] IN ('Customers','Employees','Shippers')

EXEC dbo.sp_MSForEachTable @command1 = @sql, @whereand = 'AND o.Id IN (SELECT Object_id(TableName) FROM #TableNames)'
 
SELECT * FROM #ResultTable ORDER BY ObjectName

  

/*
IF OBJECT_ID('tempdb..#ResultTable') IS NOT NULL DROP TABLE #ResultTable
IF OBJECT_ID('tempdb..#TableNames') IS NOT NULL DROP TABLE #TableNames

CREATE TABLE #ResultTable
(
   EntityId XML,
   ObjectName Varchar(100)
)

CREATE TABLE #TableNames
(
   TableName Varchar(50)
)
  
DECLARE @sql NVARCHAR(2000) = '	DECLARE @colname Varchar(100)

								SELECT @colname =	CASE	WHEN EXISTS (SELECT 1 FROM SYS.COLUMNS C WHERE c.OBJECT_ID = OBJECT_ID(''?'') AND C.NAME = ''empid'') 
															THEN ''empId''
															WHEN EXISTS (SELECT 1 FROM SYS.COLUMNS C WHERE c.OBJECT_ID = OBJECT_ID(''?'') AND C.NAME = ''custId'') 
															THEN ''custId''
													END

								IF @colname = ''empId''
									INSERT INTO #ResultTable
										EXEC(''
											SElECT STUFF((SELECT '''','''' + convert(varchar(10),e.empid)
											FROM ? e 
											LEFT JOIN Sales.Orders orders 
												ON e.empid = orders.empid
											GROUP BY e.empid
											ORDER BY e.empid
											FOR XML PATH ('''''''')),1,1,''''''''),''''?''''
										'')
								ELSE IF @colname = ''custId''
									INSERT INTO #ResultTable
										EXEC(''	
											SElECT STUFF((SELECT '''','''' + convert(varchar(10),e.custid)
											FROM ? e 
											LEFT JOIN Sales.Orders orders 
												ON e.custid = orders.custid
											GROUP BY e.custid
											ORDER BY e.custid
											FOR XML PATH ('''''''')),1,1,''''''''),''''?''''
										'')'

INSERT INTO #TableNames
SELECT OBJECT_SCHEMA_NAME(o.object_id)  + '.' + [Name] FROM sys.Objects o WHERE o.[Name] IN ('Customers','Employees')

EXEC dbo.sp_MSForEachTable @command1 = @sql, @whereand = 'AND o.Id IN (SELECT Object_id(TableName) FROM #TableNames)'
 
SELECT * FROM #ResultTable ORDER BY ObjectName

 */ 