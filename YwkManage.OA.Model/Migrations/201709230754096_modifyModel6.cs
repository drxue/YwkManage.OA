namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "DeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employee", "DeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Department", "DeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ward", "DeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.DoctorRegister", "DeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProjectClassify", "DeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.TitleAward", "DeleteFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.TitleLevelInfo", "DeleteFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TitleLevelInfo", "DeleteFlag");
            DropColumn("dbo.TitleAward", "DeleteFlag");
            DropColumn("dbo.ProjectClassify", "DeleteFlag");
            DropColumn("dbo.DoctorRegister", "DeleteFlag");
            DropColumn("dbo.Ward", "DeleteFlag");
            DropColumn("dbo.Department", "DeleteFlag");
            DropColumn("dbo.Employee", "DeleteFlag");
            DropColumn("dbo.Contact", "DeleteFlag");
        }
    }
}
