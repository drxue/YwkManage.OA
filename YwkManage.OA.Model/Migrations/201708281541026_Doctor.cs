namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Doctor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorRegister", "EmployeeID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.DoctorRegister", "EmployeeID");
            AddForeignKey("dbo.DoctorRegister", "EmployeeID", "dbo.Employee", "EmployeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorRegister", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.DoctorRegister", new[] { "EmployeeID" });
            AlterColumn("dbo.DoctorRegister", "EmployeeID", c => c.String());
        }
    }
}
