namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contact", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Contact", new[] { "DepartmentID" });
            DropPrimaryKey("dbo.Contact");
            DropColumn("dbo.Contact", "ContactID");
            DropColumn("dbo.Contact", "DepartmentID");
            AlterColumn("dbo.Contact", "EmployeeID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Contact", "EmployeeID");
            CreateIndex("dbo.Contact", "EmployeeID");
            AddForeignKey("dbo.Contact", "EmployeeID", "dbo.Employee", "EmployeeID");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contact", "DepartmentID", c => c.Int());
            AddColumn("dbo.Contact", "ContactID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Contact", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Contact", new[] { "EmployeeID" });
            DropPrimaryKey("dbo.Contact");
            AlterColumn("dbo.Contact", "EmployeeID", c => c.String());
            AddPrimaryKey("dbo.Contact", "ContactID");
            CreateIndex("dbo.Contact", "DepartmentID");
            AddForeignKey("dbo.Contact", "DepartmentID", "dbo.Department", "DepartmentID");
        }
    }
}
