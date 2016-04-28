namespace ITRepairDeskWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientOffice",
                c => new
                    {
                        ClientID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ClientID)
                .ForeignKey("dbo.Client", t => t.ClientID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        ExtNo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobID = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Detail = c.String(maxLength: 50),
                        Status = c.Int(),
                        Priority = c.Int(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        ClientID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Client", t => t.ClientID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.JobAssignment",
                c => new
                    {
                        JobAssignmentID = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        TechnicianID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobAssignmentID)
                .ForeignKey("dbo.Job", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.Technician", t => t.TechnicianID, cascadeDelete: true)
                .Index(t => t.JobID)
                .Index(t => t.TechnicianID);
            
            CreateTable(
                "dbo.Technician",
                c => new
                    {
                        TechnicianID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        JobAssignDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        ContactNo = c.String(),
                    })
                .PrimaryKey(t => t.TechnicianID);
            
            CreateTable(
                "dbo.ClientJob",
                c => new
                    {
                        JobID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobID, t.ClientID })
                .ForeignKey("dbo.Job", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.JobID)
                .Index(t => t.ClientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientOffice", "ClientID", "dbo.Client");
            DropForeignKey("dbo.JobAssignment", "TechnicianID", "dbo.Technician");
            DropForeignKey("dbo.JobAssignment", "JobID", "dbo.Job");
            DropForeignKey("dbo.Job", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "ClientID", "dbo.Client");
            DropForeignKey("dbo.ClientJob", "ClientID", "dbo.Client");
            DropForeignKey("dbo.ClientJob", "JobID", "dbo.Job");
            DropIndex("dbo.ClientJob", new[] { "ClientID" });
            DropIndex("dbo.ClientJob", new[] { "JobID" });
            DropIndex("dbo.JobAssignment", new[] { "TechnicianID" });
            DropIndex("dbo.JobAssignment", new[] { "JobID" });
            DropIndex("dbo.Department", new[] { "ClientID" });
            DropIndex("dbo.Job", new[] { "DepartmentID" });
            DropIndex("dbo.ClientOffice", new[] { "ClientID" });
            DropTable("dbo.ClientJob");
            DropTable("dbo.Technician");
            DropTable("dbo.JobAssignment");
            DropTable("dbo.Department");
            DropTable("dbo.Job");
            DropTable("dbo.Client");
            DropTable("dbo.ClientOffice");
        }
    }
}
