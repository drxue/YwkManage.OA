namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leave", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Leave", new[] { "DepartmentID" });
            AddColumn("dbo.Employee", "TitleLevelInfoID", c => c.Int());
            AddColumn("dbo.Leave", "DepartmentName", c => c.String());
            CreateIndex("dbo.Employee", "TitleLevelInfoID");
            AddForeignKey("dbo.Employee", "TitleLevelInfoID", "dbo.TitleLevelInfo", "TitleLevelInfoID");
            DropColumn("dbo.Employee", "Title_TitleID");
            DropColumn("dbo.Leave", "DepartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leave", "DepartmentID", c => c.Int(nullable: false));
            AddColumn("dbo.Employee", "Title_TitleID", c => c.Int());
            DropForeignKey("dbo.Employee", "TitleLevelInfoID", "dbo.TitleLevelInfo");
            DropIndex("dbo.Employee", new[] { "TitleLevelInfoID" });
            DropColumn("dbo.Leave", "DepartmentName");
            DropColumn("dbo.Employee", "TitleLevelInfoID");
            CreateIndex("dbo.Leave", "DepartmentID");
            AddForeignKey("dbo.Leave", "DepartmentID", "dbo.Department", "DepartmentID");
        }
    }
}
