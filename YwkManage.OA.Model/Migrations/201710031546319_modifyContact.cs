namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyContact : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contact", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Contact", new[] { "EmployeeID" });
            AddColumn("dbo.Contact", "Department", c => c.String());
            DropColumn("dbo.Contact", "Telephone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contact", "Telephone", c => c.String());
            DropColumn("dbo.Contact", "Department");
            CreateIndex("dbo.Contact", "EmployeeID");
            AddForeignKey("dbo.Contact", "EmployeeID", "dbo.Employee", "EmployeeID");
        }
    }
}
