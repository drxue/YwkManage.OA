namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            AddColumn("dbo.TitleAward", "TitleLevelInfoID", c => c.Int(nullable: false));
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String());
            CreateIndex("dbo.TitleAward", "TitleLevelInfoID");
            AddForeignKey("dbo.TitleAward", "TitleLevelInfoID", "dbo.TitleLevelInfo", "TitleLevelInfoID", cascadeDelete: true);
            DropColumn("dbo.TitleAward", "TitleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TitleAward", "TitleID", c => c.Int(nullable: false));
            DropForeignKey("dbo.TitleAward", "TitleLevelInfoID", "dbo.TitleLevelInfo");
            DropIndex("dbo.TitleAward", new[] { "TitleLevelInfoID" });
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String(maxLength: 128));
            DropColumn("dbo.TitleAward", "TitleLevelInfoID");
            CreateIndex("dbo.Leave", "EmployeeID");
            AddForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee", "EmployeeID");
        }
    }
}
