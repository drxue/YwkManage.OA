namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projiectclassify : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Leave", "ProjectClassifyID");
            AddForeignKey("dbo.Leave", "ProjectClassifyID", "dbo.ProjectClassify", "ProjectClassifyID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leave", "ProjectClassifyID", "dbo.ProjectClassify");
            DropIndex("dbo.Leave", new[] { "ProjectClassifyID" });
        }
    }
}
