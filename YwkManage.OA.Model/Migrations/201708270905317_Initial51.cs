namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial51 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Leave", "EmployeeID");
            AddForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Leave", "EmployeeID");
            AddForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee", "EmployeeID");
        }
    }
}
