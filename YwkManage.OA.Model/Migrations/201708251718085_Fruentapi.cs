namespace YwkManage.OA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fruentapi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leave", "Department_DepartmenID", "dbo.Department");
            DropForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Leave", "ProjectClassifyID", "dbo.ProjectClassify");
            DropForeignKey("dbo.TitleAward", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.TitleAward", "TitleID", "dbo.TitleLevelInfo");
            DropForeignKey("dbo.Department", "Ward_WardID", "dbo.Ward");
            DropForeignKey("dbo.Ward", "DeputyHeadNurse_EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Ward", "HeadNurse_EmployeeID", "dbo.Employee");
            DropIndex("dbo.Department", new[] { "Ward_WardID" });
            DropIndex("dbo.Leave", new[] { "EmployeeID" });
            DropIndex("dbo.Leave", new[] { "ProjectClassifyID" });
            DropIndex("dbo.Leave", new[] { "Department_DepartmenID" });
            DropIndex("dbo.TitleAward", new[] { "EmployeeID" });
            DropIndex("dbo.TitleAward", new[] { "TitleID" });
            DropIndex("dbo.Ward", new[] { "DeputyHeadNurse_EmployeeID" });
            DropIndex("dbo.Ward", new[] { "HeadNurse_EmployeeID" });
            AddColumn("dbo.Employee", "Department_DepartmenID", c => c.Int(nullable: false));
            AddColumn("dbo.Employee", "Department_DepartmenID1", c => c.Int(nullable: false));
            AddColumn("dbo.Employee", "Department_DepartmenID2", c => c.Int(nullable: false));
            AddColumn("dbo.Employee", "Department_DepartmenID3", c => c.Int(nullable: false));
            AlterColumn("dbo.DoctorRegister", "EmployeeID", c => c.String());
            AlterColumn("dbo.DoctorRegister", "CertifiedStatus", c => c.String());
            AlterColumn("dbo.DoctorRegister", "Name", c => c.String());
            AlterColumn("dbo.DoctorRegister", "Gender", c => c.String());
            AlterColumn("dbo.DoctorRegister", "IDCard", c => c.String());
            AlterColumn("dbo.DoctorRegister", "QualificationCertificateNo", c => c.String());
            AlterColumn("dbo.DoctorRegister", "LicensedCertificateNo", c => c.String());
            AlterColumn("dbo.DoctorRegister", "DoctorGrade", c => c.String());
            AlterColumn("dbo.DoctorRegister", "LicensedType", c => c.String());
            AlterColumn("dbo.DoctorRegister", "DoctorTitle", c => c.String());
            AlterColumn("dbo.DoctorRegister", "LicensedScope", c => c.String());
            AlterColumn("dbo.DoctorRegister", "ApprovalOrganization", c => c.String());
            AlterColumn("dbo.DoctorRegister", "MedicalInstitutions", c => c.String());
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String());
            AlterColumn("dbo.TitleAward", "EmployeeID", c => c.String());
            CreateIndex("dbo.Employee", "Department_DepartmenID");
            CreateIndex("dbo.Employee", "Department_DepartmenID1");
            CreateIndex("dbo.Employee", "Department_DepartmenID2");
            CreateIndex("dbo.Employee", "Department_DepartmenID3");
            AddForeignKey("dbo.Employee", "Department_DepartmenID", "dbo.Department", "DepartmenID", cascadeDelete: true);
            AddForeignKey("dbo.Employee", "Department_DepartmenID1", "dbo.Department", "DepartmenID");
            AddForeignKey("dbo.Employee", "Department_DepartmenID2", "dbo.Department", "DepartmenID");
            AddForeignKey("dbo.Employee", "Department_DepartmenID3", "dbo.Department", "DepartmenID");
            DropColumn("dbo.Department", "DepartmentDeputyDirectorID");
            DropColumn("dbo.Department", "DepartmentDirectorID");
            DropColumn("dbo.Department", "HeadNurseID");
            DropColumn("dbo.Department", "Ward_WardID");
            DropColumn("dbo.Employee", "Department");
            DropColumn("dbo.Leave", "Department_DepartmenID");
            DropColumn("dbo.Ward", "DeputyHeadNurse_EmployeeID");
            DropColumn("dbo.Ward", "HeadNurse_EmployeeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ward", "HeadNurse_EmployeeID", c => c.String(maxLength: 128));
            AddColumn("dbo.Ward", "DeputyHeadNurse_EmployeeID", c => c.String(maxLength: 128));
            AddColumn("dbo.Leave", "Department_DepartmenID", c => c.Int());
            AddColumn("dbo.Employee", "Department", c => c.String());
            AddColumn("dbo.Department", "Ward_WardID", c => c.Int());
            AddColumn("dbo.Department", "HeadNurseID", c => c.String());
            AddColumn("dbo.Department", "DepartmentDirectorID", c => c.String());
            AddColumn("dbo.Department", "DepartmentDeputyDirectorID", c => c.String());
            DropForeignKey("dbo.Employee", "Department_DepartmenID3", "dbo.Department");
            DropForeignKey("dbo.Employee", "Department_DepartmenID2", "dbo.Department");
            DropForeignKey("dbo.Employee", "Department_DepartmenID1", "dbo.Department");
            DropForeignKey("dbo.Employee", "Department_DepartmenID", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "Department_DepartmenID3" });
            DropIndex("dbo.Employee", new[] { "Department_DepartmenID2" });
            DropIndex("dbo.Employee", new[] { "Department_DepartmenID1" });
            DropIndex("dbo.Employee", new[] { "Department_DepartmenID" });
            AlterColumn("dbo.TitleAward", "EmployeeID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Leave", "EmployeeID", c => c.String(maxLength: 128));
            AlterColumn("dbo.DoctorRegister", "MedicalInstitutions", c => c.String(maxLength: 50));
            AlterColumn("dbo.DoctorRegister", "ApprovalOrganization", c => c.String(maxLength: 50));
            AlterColumn("dbo.DoctorRegister", "LicensedScope", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.DoctorRegister", "DoctorTitle", c => c.String(maxLength: 50));
            AlterColumn("dbo.DoctorRegister", "LicensedType", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.DoctorRegister", "DoctorGrade", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.DoctorRegister", "LicensedCertificateNo", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.DoctorRegister", "QualificationCertificateNo", c => c.String(nullable: false, maxLength: 27));
            AlterColumn("dbo.DoctorRegister", "IDCard", c => c.String(nullable: false, maxLength: 17));
            AlterColumn("dbo.DoctorRegister", "Gender", c => c.String(maxLength: 1));
            AlterColumn("dbo.DoctorRegister", "Name", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.DoctorRegister", "CertifiedStatus", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.DoctorRegister", "EmployeeID", c => c.String(maxLength: 10));
            DropColumn("dbo.Employee", "Department_DepartmenID3");
            DropColumn("dbo.Employee", "Department_DepartmenID2");
            DropColumn("dbo.Employee", "Department_DepartmenID1");
            DropColumn("dbo.Employee", "Department_DepartmenID");
            CreateIndex("dbo.Ward", "HeadNurse_EmployeeID");
            CreateIndex("dbo.Ward", "DeputyHeadNurse_EmployeeID");
            CreateIndex("dbo.TitleAward", "TitleID");
            CreateIndex("dbo.TitleAward", "EmployeeID");
            CreateIndex("dbo.Leave", "Department_DepartmenID");
            CreateIndex("dbo.Leave", "ProjectClassifyID");
            CreateIndex("dbo.Leave", "EmployeeID");
            CreateIndex("dbo.Department", "Ward_WardID");
            AddForeignKey("dbo.Ward", "HeadNurse_EmployeeID", "dbo.Employee", "EmployeeID");
            AddForeignKey("dbo.Ward", "DeputyHeadNurse_EmployeeID", "dbo.Employee", "EmployeeID");
            AddForeignKey("dbo.Department", "Ward_WardID", "dbo.Ward", "WardID");
            AddForeignKey("dbo.TitleAward", "TitleID", "dbo.TitleLevelInfo", "TitleID", cascadeDelete: true);
            AddForeignKey("dbo.TitleAward", "EmployeeID", "dbo.Employee", "EmployeeID");
            AddForeignKey("dbo.Leave", "ProjectClassifyID", "dbo.ProjectClassify", "ProjectClassifyID");
            AddForeignKey("dbo.Leave", "EmployeeID", "dbo.Employee", "EmployeeID");
            AddForeignKey("dbo.Leave", "Department_DepartmenID", "dbo.Department", "DepartmenID");
        }
    }
}
