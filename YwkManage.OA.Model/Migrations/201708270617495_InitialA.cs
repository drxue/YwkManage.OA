namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "DepartmentID", c => c.Int());
            CreateIndex("dbo.Contact", "DepartmentID");
            AddForeignKey("dbo.Contact", "DepartmentID", "dbo.Department", "DepartmenID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Contact", new[] { "DepartmentID" });
            DropColumn("dbo.Contact", "DepartmentID");
        }
    }
}
