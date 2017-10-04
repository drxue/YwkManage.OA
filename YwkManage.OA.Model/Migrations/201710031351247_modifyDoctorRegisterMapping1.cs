namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyDoctorRegisterMapping1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DoctorRegister", new[] { "Employee_EmployeeID" });
            DropPrimaryKey("dbo.DoctorRegister");
            DropColumn("dbo.DoctorRegister", "EmployeeID");
            RenameColumn(table: "dbo.DoctorRegister", name: "Employee_EmployeeID", newName: "EmployeeID");
            
            AlterColumn("dbo.DoctorRegister", "EmployeeID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DoctorRegister", "EmployeeID");
            CreateIndex("dbo.DoctorRegister", "EmployeeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DoctorRegister", new[] { "EmployeeID" });
            DropPrimaryKey("dbo.DoctorRegister");
            AlterColumn("dbo.DoctorRegister", "EmployeeID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.DoctorRegister", "EmployeeID");
            RenameColumn(table: "dbo.DoctorRegister", name: "EmployeeID", newName: "Employee_EmployeeID");
            AddColumn("dbo.DoctorRegister", "EmployeeID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.DoctorRegister", "Employee_EmployeeID");
        }
    }
}
