namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyModel5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DoctorRegister");
            AddPrimaryKey("dbo.DoctorRegister", "EmployeeID");
            DropColumn("dbo.DoctorRegister", "DoctorRegisterID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoctorRegister", "DoctorRegisterID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.DoctorRegister");
            AddPrimaryKey("dbo.DoctorRegister", "DoctorRegisterID");
        }
    }
}
