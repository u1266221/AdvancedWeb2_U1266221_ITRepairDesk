namespace ITRepairDeskWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Job",
                c => new
                    {
                        JobID = c.Int(nullable: false),
                        Title = c.String(),
                        Detail = c.String(),
                        Status = c.Int(),
                        Priority = c.Int(),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Department = c.String(),
                        Email = c.String(),
                        ExtNo = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Technician",
                c => new
                    {
                        TechnicianID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        JobAssignDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        ContactNo = c.String(),
                        JobAssignment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TechnicianID);
            
            CreateTable(
                "dbo.ClientJob",
                c => new
                    {
                        Client_ClientID = c.Int(nullable: false),
                        Job_JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Client_ClientID, t.Job_JobID })
                .ForeignKey("dbo.Client", t => t.Client_ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.Job_JobID, cascadeDelete: true)
                .Index(t => t.Client_ClientID)
                .Index(t => t.Job_JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobAssignment", "TechnicianID", "dbo.Technician");
            DropForeignKey("dbo.JobAssignment", "JobID", "dbo.Job");
            DropForeignKey("dbo.ClientJob", "Job_JobID", "dbo.Job");
            DropForeignKey("dbo.ClientJob", "Client_ClientID", "dbo.Client");
            DropIndex("dbo.ClientJob", new[] { "Job_JobID" });
            DropIndex("dbo.ClientJob", new[] { "Client_ClientID" });
            DropIndex("dbo.JobAssignment", new[] { "TechnicianID" });
            DropIndex("dbo.JobAssignment", new[] { "JobID" });
            DropTable("dbo.ClientJob");
            DropTable("dbo.Technician");
            DropTable("dbo.Client");
            DropTable("dbo.Job");
            DropTable("dbo.JobAssignment");
        }
    }
}
