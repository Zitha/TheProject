namespace TheProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Section = c.String(),
                        Type = c.String(),
                        UserId = c.Int(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoundryPolygons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longitude = c.String(),
                        Latitude = c.String(),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(),
                        Suburb = c.String(),
                        Province = c.String(),
                        LocalMunicipality = c.String(),
                        Region = c.String(),
                        GPSCoordinates_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GPSCoordinates", t => t.GPSCoordinates_Id)
                .Index(t => t.GPSCoordinates_Id);
            
            CreateTable(
                "dbo.GPSCoordinates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longitude = c.String(),
                        Latitude = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingName = c.String(),
                        BuildingNumber = c.String(),
                        BuildingType = c.String(),
                        BuildingStandard = c.String(),
                        Status = c.String(),
                        NumberOfFloors = c.Int(nullable: false),
                        FootPrintArea = c.Double(nullable: false),
                        ImprovedArea = c.Double(nullable: false),
                        Heritage = c.Boolean(nullable: false),
                        OccupationYear = c.String(),
                        DisabledAccess = c.String(),
                        DisabledComment = c.String(),
                        ConstructionDescription = c.String(),
                        Photo = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(),
                        Facility_Id = c.Int(),
                        GPSCoordinates_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facilities", t => t.Facility_Id)
                .ForeignKey("dbo.GPSCoordinates", t => t.GPSCoordinates_Id)
                .Index(t => t.Facility_Id)
                .Index(t => t.GPSCoordinates_Id);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClientCode = c.String(),
                        SettlementType = c.String(),
                        Zoning = c.String(),
                        IDPicture = c.String(),
                        Status = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(),
                        DeedsInfo_Id = c.Int(),
                        Location_Id = c.Int(),
                        Portfolio_Id = c.Int(),
                        ResposiblePerson_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeedsInfoes", t => t.DeedsInfo_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id)
                .ForeignKey("dbo.People", t => t.ResposiblePerson_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.DeedsInfo_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Portfolio_Id)
                .Index(t => t.ResposiblePerson_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.DeedsInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ErFNumber = c.String(),
                        TitleDeedNumber = c.String(),
                        Extent = c.String(),
                        OwnerInfomation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        ClientId = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Designation = c.String(),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ErrorMessage = c.String(),
                        Source = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                        PasswordIsChanged = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedUserId = c.Int(nullable: false),
                        ModifiedUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facilities", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Buildings", "GPSCoordinates_Id", "dbo.GPSCoordinates");
            DropForeignKey("dbo.Facilities", "ResposiblePerson_Id", "dbo.People");
            DropForeignKey("dbo.Facilities", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.Portfolios", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Facilities", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Facilities", "DeedsInfo_Id", "dbo.DeedsInfoes");
            DropForeignKey("dbo.Buildings", "Facility_Id", "dbo.Facilities");
            DropForeignKey("dbo.Locations", "GPSCoordinates_Id", "dbo.GPSCoordinates");
            DropForeignKey("dbo.BoundryPolygons", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Portfolios", new[] { "Client_Id" });
            DropIndex("dbo.Facilities", new[] { "User_Id" });
            DropIndex("dbo.Facilities", new[] { "ResposiblePerson_Id" });
            DropIndex("dbo.Facilities", new[] { "Portfolio_Id" });
            DropIndex("dbo.Facilities", new[] { "Location_Id" });
            DropIndex("dbo.Facilities", new[] { "DeedsInfo_Id" });
            DropIndex("dbo.Buildings", new[] { "GPSCoordinates_Id" });
            DropIndex("dbo.Buildings", new[] { "Facility_Id" });
            DropIndex("dbo.Locations", new[] { "GPSCoordinates_Id" });
            DropIndex("dbo.BoundryPolygons", new[] { "Location_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.ErrorLogs");
            DropTable("dbo.People");
            DropTable("dbo.Clients");
            DropTable("dbo.Portfolios");
            DropTable("dbo.DeedsInfoes");
            DropTable("dbo.Facilities");
            DropTable("dbo.Buildings");
            DropTable("dbo.GPSCoordinates");
            DropTable("dbo.Locations");
            DropTable("dbo.BoundryPolygons");
            DropTable("dbo.Audits");
        }
    }
}
