namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Leave", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.DepartmentWardRelationship", "WardID", "dbo.Department");
            DropForeignKey("dbo.Contact", "DepartmentID", "dbo.Department");
            DropPrimaryKey("dbo.Department");
            DropColumn("dbo.Department", "DepartmenID");
            AddColumn("dbo.Department", "DepartmentID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Department", "DepartmentID");
            AddForeignKey("dbo.Employee", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.Leave", "DepartmentID", "dbo.Department", "DepartmentID");
            AddForeignKey("dbo.DepartmentWardRelationship", "WardID", "dbo.Department", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.Contact", "DepartmentID", "dbo.Department", "DepartmentID");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Department", "DepartmenID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Contact", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.DepartmentWardRelationship", "WardID", "dbo.Department");
            DropForeignKey("dbo.Leave", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropPrimaryKey("dbo.Department");
            DropColumn("dbo.Department", "DepartmentID");
            AddPrimaryKey("dbo.Department", "DepartmenID");
            AddForeignKey("dbo.Contact", "DepartmentID", "dbo.Department", "DepartmenID");
            AddForeignKey("dbo.DepartmentWardRelationship", "WardID", "dbo.Department", "DepartmenID", cascadeDelete: true);
            AddForeignKey("dbo.Leave", "DepartmentID", "dbo.Department", "DepartmenID");
            AddForeignKey("dbo.Employee", "DepartmentID", "dbo.Department", "DepartmenID", cascadeDelete: true);
        }
    }
}
