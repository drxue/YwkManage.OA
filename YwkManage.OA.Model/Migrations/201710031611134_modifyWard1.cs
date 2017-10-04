namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyWard1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ward", "WardBuilding", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ward", "WardBuilding");
        }
    }
}
