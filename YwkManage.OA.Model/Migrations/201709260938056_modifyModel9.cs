namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "DepartmentID" });
            AlterColumn("dbo.Employee", "DepartmentID", c => c.Int());
            CreateIndex("dbo.Employee", "DepartmentID");
            AddForeignKey("dbo.Employee", "DepartmentID", "dbo.Department", "DepartmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "DepartmentID" });
            AlterColumn("dbo.Employee", "DepartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Employee", "DepartmentID");
            AddForeignKey("dbo.Employee", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
        }
    }
}
