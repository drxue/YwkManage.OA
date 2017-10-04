namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyDoctorRegisterMapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DoctorRegister", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.DoctorRegister", new[] { "EmployeeID" });
            AddColumn("dbo.DoctorRegister", "Employee_EmployeeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.DoctorRegister", "Employee_EmployeeID");
            AddForeignKey("dbo.DoctorRegister", "Employee_EmployeeID", "dbo.Employee", "EmployeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorRegister", "Employee_EmployeeID", "dbo.Employee");
            DropIndex("dbo.DoctorRegister", new[] { "Employee_EmployeeID" });
            DropColumn("dbo.DoctorRegister", "Employee_EmployeeID");
            CreateIndex("dbo.DoctorRegister", "EmployeeID");
            AddForeignKey("dbo.DoctorRegister", "EmployeeID", "dbo.Employee", "EmployeeID");
        }
    }
}
