namespace ITRepairDeskWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Technician", "JobAssignment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Technician", "JobAssignment", c => c.DateTime(nullable: false));
        }
    }
}
