namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentAttribute",
                c => new
                    {
                        DepartmentAttributeID = c.Int(nullable: false, identity: true),
                        DepartmentAttributeName = c.String(),
                        Comment = c.String(),
                        DeleteFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentAttributeID);
            
            AlterColumn("dbo.Department", "DepartmentAttributeID", c => c.Int());
            CreateIndex("dbo.Department", "DepartmentAttributeID");
            AddForeignKey("dbo.Department", "DepartmentAttributeID", "dbo.DepartmentAttribute", "DepartmentAttributeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Department", "DepartmentAttributeID", "dbo.DepartmentAttribute");
            DropIndex("dbo.Department", new[] { "DepartmentAttributeID" });
            AlterColumn("dbo.Department", "DepartmentAttributeID", c => c.Int(nullable: false));
            DropTable("dbo.DepartmentAttribute");
        }
    }
}
