namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "HisDepartmentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Department", "HisDepartmentName");
        }
    }
}
