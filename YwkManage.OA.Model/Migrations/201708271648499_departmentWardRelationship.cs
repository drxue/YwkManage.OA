namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentWardRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentWardRelationship",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false),
                        WardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DepartmentID, t.WardID })
                .ForeignKey("dbo.Ward", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.WardID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.WardID);
            
            AlterColumn("dbo.Ward", "HeadNurseID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Ward", "DeputyHeadNurseID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ward", "DeputyHeadNurseID");
            CreateIndex("dbo.Ward", "HeadNurseID");
            AddForeignKey("dbo.Ward", "DeputyHeadNurseID", "dbo.Employee", "EmployeeID");
            AddForeignKey("dbo.Ward", "HeadNurseID", "dbo.Employee", "EmployeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ward", "HeadNurseID", "dbo.Employee");
            DropForeignKey("dbo.Ward", "DeputyHeadNurseID", "dbo.Employee");
            DropForeignKey("dbo.DepartmentWardRelationship", "WardID", "dbo.Department");
            DropForeignKey("dbo.DepartmentWardRelationship", "DepartmentID", "dbo.Ward");
            DropIndex("dbo.DepartmentWardRelationship", new[] { "WardID" });
            DropIndex("dbo.DepartmentWardRelationship", new[] { "DepartmentID" });
            DropIndex("dbo.Ward", new[] { "HeadNurseID" });
            DropIndex("dbo.Ward", new[] { "DeputyHeadNurseID" });
            AlterColumn("dbo.Ward", "DeputyHeadNurseID", c => c.String());
            AlterColumn("dbo.Ward", "HeadNurseID", c => c.String());
            DropTable("dbo.DepartmentWardRelationship");
        }
    }
}
