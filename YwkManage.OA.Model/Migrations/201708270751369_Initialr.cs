namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Leave", "EmployeeID");
            CreateIndex("dbo.Leave", "DepartmentID");
            AddForeignKey("dbo.Leave", "DepartmentID", "dbo.Department", "DepartmenID", cascadeDelete: true);
            AddForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee", "EmployeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Leave", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Leave", new[] { "DepartmentID" });
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String());
        }
    }
}
