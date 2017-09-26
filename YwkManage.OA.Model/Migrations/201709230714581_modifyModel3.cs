namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Department", name: "HeadNurseID", newName: "DepartmentHeadNurseID");
            RenameIndex(table: "dbo.Department", name: "IX_HeadNurseID", newName: "IX_DepartmentHeadNurseID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Department", name: "IX_DepartmentHeadNurseID", newName: "IX_HeadNurseID");
            RenameColumn(table: "dbo.Department", name: "DepartmentHeadNurseID", newName: "HeadNurseID");
        }
    }
}
