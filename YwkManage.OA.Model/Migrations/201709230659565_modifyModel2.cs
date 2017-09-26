namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ward", new[] { "HeadNurseID" });
            AddColumn("dbo.Ward", "WardDirectorID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Ward", "HeadNurseID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ward", "HeadNurseID");
            CreateIndex("dbo.Ward", "WardDirectorID");
            AddForeignKey("dbo.Ward", "WardDirectorID", "dbo.Employee", "EmployeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ward", "WardDirectorID", "dbo.Employee");
            DropIndex("dbo.Ward", new[] { "WardDirectorID" });
            DropIndex("dbo.Ward", new[] { "HeadNurseID" });
            AlterColumn("dbo.Ward", "HeadNurseID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Ward", "WardDirectorID");
            CreateIndex("dbo.Ward", "HeadNurseID");
        }
    }
}
