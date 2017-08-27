namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "AdministractiveFlag", c => c.Boolean());
            AlterColumn("dbo.TitleAward", "EmployeeID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TitleAward", "EmployeeID");
            AddForeignKey("dbo.TitleAward", "EmployeeID", "dbo.Employee", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TitleAward", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.TitleAward", new[] { "EmployeeID" });
            AlterColumn("dbo.TitleAward", "EmployeeID", c => c.String());
            DropColumn("dbo.Department", "AdministractiveFlag");
        }
    }
}
