select F.ID,F.ClientCode,
	   L.LocalMunicipality,L.Province,L.Region,L.StreetAddress,
	   G.Latitude,G.Longitude,
	   P.FullName,P.EmailAddress,p.PhoneNumber
	   from Facilities F
INNER JOIN People P ON  F.ResposiblePerson_Id =P.Id
INNER JOIN Locations L ON  F.Location_Id =L.Id
INNER JOIN GPSCoordinates G ON  L.GPSCoordinates_Id = G.Id
where Status='Submitted' and Name not like'TestFacility%'


select * from Buildings
where Facility_Id=3260

Select * from Facilities
select * from GPSCoordinates