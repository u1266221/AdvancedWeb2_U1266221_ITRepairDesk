namespace ITRepairDeskWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyingDataModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "Email", c => c.String());
            AlterColumn("dbo.Client", "ExtNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "ExtNo", c => c.String(nullable: false));
            AlterColumn("dbo.Client", "Email", c => c.String(nullable: false));
        }
    }
}
