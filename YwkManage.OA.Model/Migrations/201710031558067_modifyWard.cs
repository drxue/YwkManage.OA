namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyWard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ward", "WardFloor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ward", "WardFloor");
        }
    }
}
