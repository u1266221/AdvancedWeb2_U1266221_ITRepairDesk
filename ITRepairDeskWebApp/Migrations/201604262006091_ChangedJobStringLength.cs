namespace ITRepairDeskWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedJobStringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Job", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.Job", "Detail", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Job", "Detail", c => c.String(maxLength: 50));
            AlterColumn("dbo.Job", "Title", c => c.String(maxLength: 50));
        }
    }
}
