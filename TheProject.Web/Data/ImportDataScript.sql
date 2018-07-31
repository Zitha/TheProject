SELECT *
INTO #Temp From (
SELECT  ROW_NUMBER() OVER(ORDER BY VENUS_CODE) AS num, PROPDESC As 'Name',VENUS_CODE AS 'ClientCode',[ZONING] as 'Zoning',
		[Street_No]+' '+[Physical_Addr] AS 'StreetAddress',[TOWNSHIP] as 'Suburb',
		[REGION] as 'Region',[1] as 'LocalMunicipality',[Owner_Name] as 'OwnerInfomation',
		[Register_No] as 'TitleDeedNumber',[LONGITUDE] as 'Longitude',[LATITUDE] as 'Latitude' --,[USER/ASSESOR] AS 'ASSESOR'
  FROM [TheProject].[dbo].[G$]) as X
  ALTER TABLE #TEMP ADD GPSCoordinatesId INT
  ALTER TABLE #TEMP ADD DeedsId INT
  ALTER TABLE #TEMP ADD LocationId INT
  ALTER TABLE #TEMP ADD UserId INT
  
  --Delete from Facilities
  --Delete from DeedsInfoes
  --Delete from Locations
  --Delete from GPSCoordinates
  --Delete from Buildings

  --select * FROM #Temp
  BEGIN
      declare @TotalCount int 
	  declare @Counter int
	  SET @Counter = 1
	  SET @TotalCount = (SELECT COUNT(*) FROM #Temp)

	  WHILE (@Counter <=@TotalCount)
		BEGIN
			Insert into GPSCoordinates(Longitude,Latitude)
			select Longitude,Latitude
			FROM #TEMP where num=@Counter
			
			UPDATE #TEMP
			SET GPSCoordinatesId = IDENT_CURRENT('GPSCoordinates')
			WHERE num=@Counter

			Insert into Locations(StreetAddress,Suburb,LocalMunicipality,Province,Region,BoundryPolygon_Id,GPSCoordinates_Id)
			select  StreetAddress,Suburb,LocalMunicipality,'Gauteng',Region,NUll,GPSCoordinatesId
			FROM #TEMP where num=@Counter
			
			UPDATE #TEMP
			SET LocationId = IDENT_CURRENT('Locations')
			WHERE num=@Counter
			-------------------------------------------------------------------------------------------------
			Insert into DeedsInfoes(ErFNumber,TitleDeedNumber,OwnerInfomation,Extent)
			select null,TitleDeedNumber,OwnerInfomation,0
			FROM #TEMP where num=@Counter

			UPDATE #TEMP
			SET DeedsId = IDENT_CURRENT('DeedsInfoes')
			WHERE num=@Counter

			--UPDATE #TEMP
			--SET UserId = (Select Id from Users where Username= #TEMP.Assesor)
			--WHERE num=@Counter
			

			Insert Into Facilities(Name,ClientCode,Zoning,CreatedDate,ModifiedDate,CreatedUserId,Location_Id,DeedsInfo_Id,[User_Id])
			SELECT  Name,ClientCode,Zoning,GETDATE(),GETDATE(),0,LocationId,DeedsId,NUll
			FROM #TEMP where num=@Counter

		  SET @Counter = @Counter + 1
		  CONTINUE;
		END
	End
	DROP TABLE #TEMP

------------------------------------------------------------

MERGE Facilities AS targetTB
USING B$ AS sourceTB
ON targetTB.ClientCode = sourceTB.VENUS_CODE
	WHEN MATCHED AND sourceTB.VENUS_CODE NOT IN ('C550000000006700175000R0000','C55000000000670028700000000',
					'C55000000000670041500000000','H700000000011500006000R0000','H70000000001150000600000000'
					,'C550000000006700286000R0000','X66000000000760004800000000','C03000000026600000200000000'
					,'C55000000000670000500000000','C55000000000670004800000000','C55000000000670023500000000'
					,'C550000000006700236000R0000','C550000000006700179000R0000','C03000000026490000000000000'
					,'V010000000002500000000R0000','C55000000000670041600000000','C290000000193300000000R0000'
					,'C21000000002560000000000000','C030000000753900000000R0000')  THEN
		UPDATE SET 
		 targetTB.User_Id=(Select distinct Id from USERS where Username= sourceTB.[USER/ASSESOR]);


select VENUS_CODE,COUNT(VENUS_CODE) AS 'Counter' from B$
group by VENUS_CODE
order by 'Counter' desc