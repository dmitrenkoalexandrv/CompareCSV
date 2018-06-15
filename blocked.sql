declare @dat int
select @dat =57

--select loginame,blocked,spid from sysprocesses where blocked!=0 order by loginame

select * from master..sysprocesses where spid > 50 and kpid <> 0 and spid <> @@spid order by spid
select stmt_start , * from master..sysprocesses where spid > 50 and kpid <> 0 and program_name like 'Scy%'
select stmt_start , * from master..sysprocesses where spid > 50 and kpid <> 0 and program_name like 'Scrooge 3%'
select * from master..sysprocesses where spid > 50 and kpid <> 0 and spid <> @@spid order by spid
dbcc inputbuffer( 2072 )
SET NOCOUNT ON SET QUOTED_IDENTIFIER OFF IF @@TranCount <> 0 BEGIN Print( '* Ошибка обработки транзакции. Закрываю предыдущую транзакцию!') ROLLBACK END EXEC dbo.sc_synchronization @TBLName ='DOC_CNN',   @SyncBranchId = 1012405,  @SyncMode = 0x0,     @SyncDate = 40886,     @WorkStamp = 0x00000001CF6FCCFD 
select top 10 * from syncs where code = 'DOC_CNN'
sp_helptext SC_CBT_GETDOCCNNVALUE
sp_recompile SC_CBT_GETDOCCNNVALUE
update statistics ConnotationValues
dbcc showcontig ( 'ConnotationValues' )
dbcc opentran
dbcc inputbuffer( 932 )
select * from users where code = 'svs'
select top 200 * from scrooge..coreflags where Value = 275 and spid in ( select spid from master..sysprocesses )
select top 200 * from tempdb..coreflags where spid = @dat
select * from coreflags where value = 275
select * from users where id = 1799
select * from master..sysprocesses where spid = 1472
--select t2.name, t1.text  from syscomments as t1  inner join sysobjects as t2 on t1.id = t2.id  where t1.text like '%SELECT	TOP 1@EventsDate	= DayDate	,	@EventsAmount	= Amount FROM	#TreatyAmounts WHERE	Amount		!= 0 ORDER BY	DayDate	DESC%'
kill 1472


dbcc inputbuffer( @dat )

set nocount on
declare @spid int; set @spid = @dat
select cpu         , physical_io          , memusage    from master..sysprocesses where spid = @spid

DECLARE @sql_handle binary(20), @stmt_start int, @stmt_end int
SELECT	@sql_handle = sql_handle, @stmt_start = stmt_start/2, @stmt_end = CASE WHEN stmt_end = -1 THEN -1 ELSE stmt_end/2 END
FROM	master.dbo.sysprocesses
WHERE	spid = @spid
print 	'STMT: ' + str( @stmt_start ) + '/' + str( @stmt_end  )
SELECT 
	SUBSTRING(	text,
			COALESCE(NULLIF(@stmt_start, 0), 1),
			CASE @stmt_end 
				WHEN -1 THEN DATALENGTH(text) 
				ELSE 	(@stmt_end - @stmt_start) 
				END
		) 
	FROM ::fn_get_sql(@sql_handle)


select * from tempdb..coreflags where spid = @dat and status = 1 
select  (SELECT Code FROM Users WHERE Id = Value),(SELECT NAME FROM Users WHERE Id = Value),* from tempdb..coreflags where spid = @dat AND NAME= 'AppServer.UserId' and status = 1
SELECT * FROM AppUsers WHERE  id IN (select Value from tempdb..coreflags where spid = @dat and status = 1 AND NAME= 'AppServer.AppUserId' )

select * from users where id = 2386
select distinct object_name( id ) from syscomments where text like '%openquery%'

SELECT * FROM syncs WHERE flag & 2 <> 0
while 1=1 begin UPDATE top ( 1000 ) syncs SET flag = flag ^ 2 WHERE flag & 2 <> 0 if @@trancount = 0 break end
TRUNCATE TABLE syncevents

SELECT TOP 20 * FROM master..sysperfinfo WHERE counter_name LIKE '%life%' 


---------------------------------------------------------------------------------------

select 
a.spid,
a.blocked,
a.lastwaittype,
a.waittime,
a.hostname,
a.program_name,
(SELECT 
      SUBSTRING(  text,
                  COALESCE(NULLIF(stmt_start/2, 0), 1),
                  CASE a.stmt_end 
                        WHEN -1 THEN DATALENGTH(text) 
                        ELSE (stmt_end/2 - stmt_start/2) 
                        END
            ) 
      FROM ::fn_get_sql(a.sql_handle)
) as req,
a.status,
a.open_tran,
a.cpu,
a.physical_io,
a.cmd,
a.stmt_start
from master..sysprocesses a
where spid > 50 and kpid <> 0 
--AND spid = 114
--AND a.hostname IN ('diam-srv')
and spid <> @@spid 
order by a.spid

