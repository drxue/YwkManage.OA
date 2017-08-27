namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leave", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Leave", new[] { "DepartmentID" });
            AddColumn("dbo.Leave", "Department_DepartmenID", c => c.Int());
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Leave", "EmployeeID");
            CreateIndex("dbo.Leave", "Department_DepartmenID");
            AddForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee", "EmployeeID");
            AddForeignKey("dbo.Leave", "Department_DepartmenID", "dbo.Department", "DepartmenID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leave", "Department_DepartmenID", "dbo.Department");
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Leave", new[] { "Department_DepartmenID" });
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String());
            DropColumn("dbo.Leave", "Department_DepartmenID");
            CreateIndex("dbo.Leave", "DepartmentID");
            AddForeignKey("dbo.Leave", "DepartmentID", "dbo.Department", "DepartmenID", cascadeDelete: true);
        }
    }
}
